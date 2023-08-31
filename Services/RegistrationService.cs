using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.Xml;
using WhatsAppAPI.IServices;
using WhatsAppAPI.MessageObjects;
using WhatsAppAPI.Models.Registration;
using WhatsAppAPI.Repository;
using WhatsAppAPI.ViewModels;

namespace WhatsAppAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly FlowRepository _flowRepository;
        private readonly CustomerContactRepository _customerContactRepository;
        private readonly FlowDetailsRepository _flowDetailsRepository;
        private readonly UserPromptsRepository _userPromptsRepository;
        private readonly CommunicationRepository _communicationRepository;
        private readonly UserAnswersRepository _userAnswersRepository;
        public RegistrationService(CustomerRepository customerRepository, FlowRepository flowRepository, CustomerContactRepository customerContactRepository, CommunicationRepository communicationRepository, FlowDetailsRepository flowDetailsRepository, UserPromptsRepository userPromptsRepository, UserAnswersRepository userAnswersRepository)
        {
            _flowRepository = flowRepository;
            _flowDetailsRepository = flowDetailsRepository;
            _customerRepository = customerRepository;
            _customerContactRepository = customerContactRepository;
            _communicationRepository = communicationRepository;
            _userPromptsRepository = userPromptsRepository;
            _userAnswersRepository = userAnswersRepository;
        }

        public int? GetNextPromptIdInFlow(int? lastPromptId, int? customerId)
        {
            int? FlowID = GetFlowId(customerId);
            var FlowDetails = _flowDetailsRepository.GetFlowDetailsListByFlowId(FlowID);
            int? NextPromptId = 0;
            if (lastPromptId != null)
            {
                NextPromptId = _customerRepository.GetNextPromptIdByCurrentPromptIdInFlow(FlowDetails, lastPromptId);
            }
            else
            {
                NextPromptId = GetFirstPromptId(FlowDetails);
            }
            return NextPromptId;
        }

        public int? GetFlowId(int? customerId)
        {
            return _customerRepository.GetFlowId(customerId);
        }

        public int? GetFirstPromptId(List<FlowDetails> flowDetails)
        {
            return _flowDetailsRepository.GetFirstPromptId(flowDetails);
        }

        public async Task SendPromptByUserPromptId(int? customerId, int? userPromptId, string phoneNumber, bool isReprompt)
        {
            var userPrompt = _userPromptsRepository.GetUserPromptByUserPromptId(userPromptId);

            string? languageCode =  _customerRepository.GetLanguageCodeById(customerId);
            if (languageCode == null) 
            {
                languageCode = "en";
            }
            var promptText = _userPromptsRepository.GetPromptTextByUserPromptId(userPromptId,languageCode);
            if (promptText == null) 
            {
                promptText = _userPromptsRepository.GetPromptTextByUserPromptId(userPromptId, "en");
            }


            var FieldName = _userPromptsRepository.GetFieldNameByUserPromptId(userPromptId);
            var questiontype = userPrompt.PromptType;

            bool isRequiredField = false;
            if (FieldName != null) 
            {
                isRequiredField = IsRequiredField(FieldName);
            }
            else
            {
                isRequiredField = true;
            }


            int communicationId = 0;

            SentMessageResponseViewModel? sentMessageResponse = new SentMessageResponseViewModel();
            FormattedResult? formattedResult = new FormattedResult();
            

            if (userPrompt.PromptNeedsReformatting == true)
            {
                formattedResult = ReformatPrompt(userPrompt, phoneNumber);
            }

            if (isReprompt == true)
            {
                var interactiveButtonObject = CreateInteractiveButtonMessageObject(userPrompt, promptText, phoneNumber, formattedResult);
                communicationId = SaveCommunication(customerId, userPromptId, isReprompt);
                sentMessageResponse = await new WhatsAppService().SendButtonMessage(interactiveButtonObject);
                UpdateCommunication(sentMessageResponse, communicationId);
            }
            else
            {
                switch (questiontype)
                {
                    case "Message":
                        //object create
                        //bind
                        //send
                        //addtocommunication
                        if (isRequiredField)
                        {
                            var textMessageObject = CreateTextMessageObject(userPrompt, promptText, phoneNumber);
                            communicationId = SaveCommunication(customerId, userPromptId, isReprompt);
                            sentMessageResponse = await new WhatsAppService().SendTextMessage(textMessageObject);
                        }
                        else
                        {
                            var optionalTextMessageObject = CreateOptionalTextMessageObject(promptText, phoneNumber);
                            communicationId = SaveCommunication(customerId, userPromptId, isReprompt);
                            sentMessageResponse = await new WhatsAppService().SendButtonMessage(optionalTextMessageObject);
                        }
                        if (sentMessageResponse!=null)
                        {
                        UpdateCommunication(sentMessageResponse, communicationId);
                        }
                        break;
                    case "List":
                        InteractiveListMessageObject interactiveListMessageObject = new InteractiveListMessageObject();
                        if (isRequiredField)
                        {
                            interactiveListMessageObject = CreateInteractiveListMessageObject(userPrompt, promptText, phoneNumber);
                        }
                        else
                        {
                            interactiveListMessageObject = CreateOptionalListMessageObject(userPrompt, promptText, phoneNumber);
                        }
                        communicationId = SaveCommunication(customerId, userPromptId, isReprompt);
                        sentMessageResponse = await new WhatsAppService().SendListMessage(interactiveListMessageObject);
                        if (sentMessageResponse!=null)
                        {
                        UpdateCommunication(sentMessageResponse, communicationId);
                        }
                        break;
                    case "Interactive":
                        InteractiveButtonMessageObject interactiveButtonMessageObject = new InteractiveButtonMessageObject();
                        if (isRequiredField)
                        {
                            interactiveButtonMessageObject = CreateInteractiveButtonMessageObject(userPrompt, promptText, phoneNumber, formattedResult);
                        }
                        else
                        {
                            interactiveButtonMessageObject = CreateOptionalButtonMessageObject(userPrompt, promptText, phoneNumber, formattedResult);
                        }
                        communicationId = SaveCommunication(customerId, userPromptId, isReprompt);
                        sentMessageResponse = await new WhatsAppService().SendButtonMessage(interactiveButtonMessageObject);
                        if (sentMessageResponse != null)
                        {
                        UpdateCommunication(sentMessageResponse, communicationId);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public int SaveCommunication(int? customerId, int? userPromptId, bool isReprompt)
        {
            Communication communication = CreateCommunicationObject(customerId, userPromptId, isReprompt);
            int communicationId = _communicationRepository.CreateCommunication(communication);
            return communicationId;
        }

        public Communication CreateCommunicationObject(int? customerId, int? userPromptId, bool isReprompt)
        {
            Communication communication = new Communication();
            communication.WhatsAppId = null;
            communication.SentDate = DateTime.UtcNow.ToString();
            communication.UserPromptsId = userPromptId;
            communication.CustomerId = customerId;
            communication.LanguageCode = _customerRepository.GetLanguageCodeById(customerId);
            communication.IsRePrompt = isReprompt;
            return communication;
        }

        //CreateObjects
        public TextMessageObject CreateTextMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber)
        {
            TextMessageObject textMessageObject = new TextMessageObject();
            textMessageObject.messaging_product = "whatsapp";
            textMessageObject.text.preview_url = false;
            textMessageObject.to = phoneNumber;
            textMessageObject.text.body = promptText.BodyText;
            textMessageObject.recipient_type = "individual";
            textMessageObject.type = "text";
            return textMessageObject;
        }

        public InteractiveListMessageObject CreateInteractiveListMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber)
        {
            InteractiveListMessageObject interactiveListMessageObject = new InteractiveListMessageObject();
            interactiveListMessageObject.messaging_product = "whatsapp";
            interactiveListMessageObject.recipient_type = "individual";
            interactiveListMessageObject.to = phoneNumber;
            interactiveListMessageObject.type = "interactive";
            interactiveListMessageObject.interactive.type = "list";

            

            interactiveListMessageObject.interactive.header.type = "text";
            interactiveListMessageObject.interactive.header.text = promptText.HeaderText??"";
            //Required field for Interactive
            interactiveListMessageObject.interactive.body.text = promptText.BodyText;

            interactiveListMessageObject.interactive.footer.text = promptText.FooterText;

            interactiveListMessageObject.interactive.action.button = promptText.Button1Text;

            List<string> listtexts = promptText.ListText.Split(',').ToList();
            List<string> listids = userPrompt.ListIds.Split(',').ToList();
            int count = listids.Count();

            List<Row> rowslist = new List<Row>();
            for (int i = 0; i < count; i++)
            {
                Row row = new Row();
                row.id = listids[i];
                row.title = listtexts[i];
                rowslist.Add(row);
            }
            Section section = new Section();
            section.rows = rowslist;
            section.title = promptText.BodyText;

            List<Section> sections = new List<Section>
            {
                section
            };
            interactiveListMessageObject.interactive.action.sections = sections;

            return interactiveListMessageObject;
        }

        public InteractiveButtonMessageObject CreateInteractiveButtonMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber, FormattedResult? formattedResult)
        {
            InteractiveButtonMessageObject interactiveButtonMessageObject = new InteractiveButtonMessageObject();
            interactiveButtonMessageObject.messaging_product = "whatsapp";
            interactiveButtonMessageObject.recipient_type = "individual";
            interactiveButtonMessageObject.to = phoneNumber;

            interactiveButtonMessageObject.type = "interactive";
            interactiveButtonMessageObject.interactive.type = "button";
            
            

            if (formattedResult.Body != null)
            {
                interactiveButtonMessageObject.interactive.header.text = formattedResult.Header??"";
                interactiveButtonMessageObject.interactive.header.type = "text";
                interactiveButtonMessageObject.interactive.body.text = formattedResult.Body;
                interactiveButtonMessageObject.interactive.footer.text = formattedResult.Footer;
            }
            else
            {
                string mediaType = promptText.MediaType??"text";
                if (mediaType == "text")
                {
                    interactiveButtonMessageObject.interactive.header.type = "text";
                    interactiveButtonMessageObject.interactive.header.text = "";
                }
                else if(mediaType == "Document")
                {
                    interactiveButtonMessageObject.interactive.header.type = "document";
                    interactiveButtonMessageObject.interactive.header.document.id = promptText.HeaderText;
                }
                else if(mediaType == "Image")
                {
                    interactiveButtonMessageObject.interactive.header.type = "image";
                    interactiveButtonMessageObject.interactive.header.image.id = promptText.HeaderText;
                }
                else if(mediaType == "Video")
                {
                    interactiveButtonMessageObject.interactive.header.type = "image";
                    interactiveButtonMessageObject.interactive.header.video.id = promptText.HeaderText;

                }                               
                interactiveButtonMessageObject.interactive.body.text = promptText.BodyText;
                interactiveButtonMessageObject.interactive.footer.text = promptText.FooterText;
            }

            List<Button> buttons = new List<Button>();
                                 
            Button button1 = new Button();
            button1.type = "reply";
            button1.reply = new Reply();
            button1.reply.id = userPrompt.Button1Id.ToString();
            button1.reply.title = promptText.Button1Text;
            buttons.Add(button1);

            if (userPrompt.Button2Id!=null)
            {
                Button button2 = new Button();
                button2.type = "reply";
                button2.reply = new Reply();
                button2.reply.id = userPrompt.Button2Id.ToString();
                button2.reply.title = promptText.Button2Text;
                buttons.Add(button2);
            }       
            interactiveButtonMessageObject.interactive.action.buttons = buttons;

            return interactiveButtonMessageObject;
        }

        public InteractiveButtonMessageObject CreateOptionalTextMessageObject(PromptText promptText, string phoneNumber)
        {
            InteractiveButtonMessageObject interactiveButtonMessageObject = new InteractiveButtonMessageObject();
            interactiveButtonMessageObject.messaging_product = "whatsapp";
            interactiveButtonMessageObject.recipient_type = "individual";
            interactiveButtonMessageObject.to = phoneNumber;
            interactiveButtonMessageObject.type = "interactive";
            interactiveButtonMessageObject.interactive.type = "button";

            interactiveButtonMessageObject.interactive.header.type = "text";
            interactiveButtonMessageObject.interactive.header.text = promptText.HeaderText??"";
            interactiveButtonMessageObject.interactive.body.text = promptText.BodyText;

            List<Button> buttons = new List<Button>();            
            Button button1 = new Button();
            button1.type = "reply";
            button1.reply.id = "0";
            button1.reply.title = "SKIP";
            buttons.Add(button1);

            interactiveButtonMessageObject.interactive.action.buttons = buttons;
            return interactiveButtonMessageObject;
        }

        public InteractiveListMessageObject CreateOptionalListMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber)
        {

            InteractiveListMessageObject interactiveListMessageObject = CreateInteractiveListMessageObject(userPrompt, promptText, phoneNumber);

            Row skiprow = new Row();
            skiprow.id = "0";
            skiprow.title = "SKIP";
            List<Row> skipRowlist = new List<Row>() { skiprow };

            Section skipSection = new Section() { title = "Click Skip To Skip", rows = skipRowlist };

            interactiveListMessageObject.interactive.action.sections.Add(skipSection);            
            return interactiveListMessageObject;
        }

        public InteractiveButtonMessageObject CreateOptionalButtonMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber, FormattedResult? formattedResult)
        {
            InteractiveButtonMessageObject interactiveButtonMessageObject = CreateInteractiveButtonMessageObject(userPrompt, promptText, phoneNumber, formattedResult);
            Button skipButton = new Button();
            skipButton.type = "reply";
            skipButton.reply.title = "SKIP";
            skipButton.reply.id = null;
            interactiveButtonMessageObject.interactive.action.buttons.Append(skipButton);

            return interactiveButtonMessageObject;
        }


        public bool IsRequiredField(string FieldName)
        {
            if (FieldName != null)
            {
                string[] fieldParts = FieldName.Split('.');
                string className = fieldParts[0];
                string propertyName = fieldParts[1];

                System.Type classType = System.Type.GetType("WhatsAppAPI.Models.Registration."+className);
                PropertyInfo propertyInfo = classType.GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    return false;
                }
                RequiredAttribute requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();
                return requiredAttribute != null ? true : false;
            }
            else
            {
                return false;
            }
        }


        public void UpdateCommunication(SentMessageResponseViewModel sentMessageResponseViewModel, int communicationId)
        {
            Communication communication = _communicationRepository.GetCommunicationById(communicationId);
            communication.WhatsAppId = sentMessageResponseViewModel.messages[0]?.id;
            _communicationRepository.Update(communication);
        }


       


        public FormattedResult ReformatPrompt(UserPrompts userPrompts, string phoneNumber)
        {
            FormattedResult formattedResult = new FormattedResult();

            int? userPromptId = userPrompts.UserPromptsId;
            var customerId = _customerRepository.GetCustomerIdByPhoneNumber(phoneNumber);
            var languageCode = _customerRepository.GetLanguageCodeById(customerId);
            if (languageCode == null)
            {
                languageCode = "en";
            }

            var currentPrompt = _userPromptsRepository.GetPromptTextByUserPromptId(userPromptId,languageCode);            

            var lastPromptId = _customerRepository.GetLastUserPromptIdByCustId(customerId);
            var communicationId = _communicationRepository.GetLastCommunicationIdByUserPromptId(lastPromptId);

            var userAnswer = _userAnswersRepository.GetUserAnswerByCommunicationId(communicationId);
            switch (userPromptId)
            {
                case 6:
                    try
                    {
                    DateTime dateTimeDOB = DateTime.ParseExact(userAnswer, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        

                        string formattedDateOfBirth = dateTimeDOB.ToString("dd MMMM yyyy");

                        var currentPromptText = currentPrompt.BodyText;
                        var formattedText = String.Format(currentPromptText, formattedDateOfBirth);

                        formattedResult.Body = formattedText;
                        formattedResult.Success = true;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                                    
                    break;
                default:
                    break;
            }
            return formattedResult;
        }
    }
}
