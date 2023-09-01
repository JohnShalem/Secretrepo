using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Channels;
using System.Web;
using WhatsAppAPI.IServices;
using WhatsAppAPI.MessageObjects;
using WhatsAppAPI.Models.Registration;
using WhatsAppAPI.Repository;
using WhatsAppAPI.ViewModels;
using WhatsAppAPI.ViewModels.WebHook;
using static WhatsAppAPI.Enums.Enumerations;
using System.Resources;
using WhatsAppAPI.Models.ValidationMessages;

namespace WhatsAppAPI.Services
{
    public class CustomerResponses : ICustomerResponses
    {
        private CustomerRepository _customerRepository;
        private FlowRepository _flowRepository;
        private CustomerContactRepository _customerContactRepository;
        private UserAnswersRepository _userAnswersRepository;
        private IRegistrationService _registrationService;
        private UserPromptsRepository _userPromptsRepository;
        private CommunicationRepository _communicationRepository;
        private IWhatsAppIntegrator _whatsAppIntegrator;
        public CustomerResponses(CustomerRepository customerRepository, FlowRepository flowRepository, CustomerContactRepository customerContactRepository, UserAnswersRepository userAnswersRepository, UserPromptsRepository userPromptsRepository, IRegistrationService registrationService, CommunicationRepository communicationRepository, IWhatsAppIntegrator whatsAppIntegrator)
        {
            _flowRepository = flowRepository;
            _customerRepository = customerRepository;
            _customerContactRepository = customerContactRepository;
            _userAnswersRepository = userAnswersRepository;
            _userPromptsRepository = userPromptsRepository;
            _registrationService = registrationService;
            _communicationRepository = communicationRepository;
            _whatsAppIntegrator = whatsAppIntegrator;
        }
        public async Task ProcessCustomerResponse(Stream stream)
        {

            string response = await new System.IO.StreamReader(stream).ReadToEndAsync();
            try
            {
                NotificationPayLoadVM notificationPayLoadVM = await ConvertResponseToModel(response);
                if (!IsStatusResponse(notificationPayLoadVM))
                {
                    string phoneNumber = GetPhoneNumberFromResponse(notificationPayLoadVM);
                    int? customerId = _customerRepository.GetCustomerIdByPhoneNumber(phoneNumber);
                    if (customerId == null)
                    {
                        customerId = CreateNewCustomer(phoneNumber);
                    }
                    await SaveAnswerAndSendNextMessage(notificationPayLoadVM, customerId, phoneNumber);
                }

                //await new AzureStorageService().SaveResponseToAzureFile(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveAnswerAndSendNextMessage(NotificationPayLoadVM notificationPayLoadVM, int? customerId, string phoneNumber)
        {
            ReplyTypes replyType = GetCustomerReplyType(notificationPayLoadVM);
            var lastPromptId = _customerRepository.GetLastUserPromptIdByCustId(customerId);

            //User Already Answered For Question
            if (IsAlreadyAnsweredQuestion(notificationPayLoadVM))
            {
                string requireddocmessage = "\u274c You have Already Answered The Question";
                await new WhatsAppService().SendRandomMessage(phoneNumber, requireddocmessage);
                await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
            }
            else
            {
                //Handle customer answers
                var userPrompt = _userPromptsRepository.GetUserPromptByUserPromptId(lastPromptId);
                if (userPrompt == null)
                {
                    SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.TextReply);
                    SendNextMessage(customerId, phoneNumber, lastPromptId);
                }
                else if (userPrompt.AnswerShouldBeAttachment == true)
                {
                    if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Document?.DocumentId == null)
                    {
                        string requireddocmessage = "\u274c No Attachment Received";
                        await new WhatsAppService().SendRandomMessage(phoneNumber, requireddocmessage);
                        await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                    }
                    else
                    {
                        SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.AttachmentReply);
                        SendNextMessage(customerId, phoneNumber, lastPromptId);
                    }
                }
                else if (userPrompt.PromptType == "Message" && replyType == ReplyTypes.TextReply)
                {
                    if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Text?.Body == null)
                    {
                        string requireddocmessage = "\u274c  Expected Text Message";
                        await new WhatsAppService().SendRandomMessage(phoneNumber, requireddocmessage);
                        await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                    }
                    else
                    {

                        SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.TextReply);

                        var validationMessagesObject = ValidateAnswer(customerId, phoneNumber);

                        if (validationMessagesObject.IsSuccess)
                        {
                            SendNextMessage(customerId, phoneNumber, lastPromptId);
                        }
                        else
                        {
                            string validationMessage = ProcessValidationResult(validationMessagesObject);
                            //Save To Main Tables
                            //Get Last Prompt,communicationId from Communication Table. then userprompts table , fieldname
                            //Get UserAnswer by communicationId
                            await new WhatsAppService().SendRandomMessage(phoneNumber, validationMessage);
                            await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                        }
                    }
                }
                else if (userPrompt.PromptType == "Interactive" || replyType == ReplyTypes.ButtonReply && userPrompt.PromptType == "Message")
                {
                    if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive?.ButtonReply?.Id == null)
                    {
                        //other message received
                        await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                    }
                    else
                    {
                        string? actionBasedOnButtonIdForUserPrompt = "";
                        int? skipToPromptIdByButtonId = 0;
                        var buttonId = Convert.ToInt32(notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive?.ButtonReply?.Id);
                        //last
                        UserPrompts? buttonuserPrompt = new UserPrompts();
                        if (userPrompt.ReConfirmationPromptId != null)
                        {
                            buttonuserPrompt = _userPromptsRepository.GetChildPromptByParentPromptId(lastPromptId);
                            actionBasedOnButtonIdForUserPrompt = GetActionBasedOnButtonIdForPrompt(buttonId, buttonuserPrompt);
                            skipToPromptIdByButtonId = GetSkipToPromptIdByButtonId(buttonId, buttonuserPrompt);
                        }
                        else
                        {
                            buttonuserPrompt = _userPromptsRepository.GetUserPromptByUserPromptId(lastPromptId);
                            actionBasedOnButtonIdForUserPrompt = GetActionBasedOnButtonIdForPrompt(buttonId, buttonuserPrompt);
                            skipToPromptIdByButtonId = GetSkipToPromptIdByButtonId(buttonId, buttonuserPrompt);
                        }

                        //based on action do the necessary operation
                        if (actionBasedOnButtonIdForUserPrompt == "SaveParentAndProceed")
                        {


                            // var parentPromptId = _userPromptsRepository.GetParentPromptIdByChildPromptId(lastPromptId);
                            var communicationid = _communicationRepository.GetLastCommunicationIdByUserPromptId(lastPromptId);

                            // var parentPrompt = _userPromptsRepository.GetUserPromptByUserPromptId(parentPromptId);

                            var userAnswer = _userAnswersRepository.GetUserAnswerByCommunicationId(communicationid);

                            //lastuseranswer and parentPrompt save 
                            //send next message
                            SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.ButtonReply);

                            if (userPrompt.ReConfirmationPromptId != null)
                            {
                                lastPromptId = _registrationService.GetNextPromptIdInFlow(lastPromptId, customerId);
                                await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                            }
                            else
                            {
                                SendNextMessage(customerId, phoneNumber, lastPromptId);
                            }
                        }
                        else if (actionBasedOnButtonIdForUserPrompt == "SkipToNext")
                        {
                            SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.ButtonReply);
                            SendNextMessage(customerId, phoneNumber, lastPromptId);
                        }
                        else if (actionBasedOnButtonIdForUserPrompt == "RepromptParent")
                        {

                            //var parentPromptId = _userPromptsRepository.GetParentPromptIdByChildPromptId(lastPromptId);
                            SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.ButtonReply);
                            await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                        }
                        else if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive?.ButtonReply?.Id == "0")
                        {
                            SendNextMessage(customerId, phoneNumber, lastPromptId);
                        }
                        else if (actionBasedOnButtonIdForUserPrompt == "SkipToPrompt")
                        {
                            SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.ButtonReply);
                            await _registrationService.SendPromptByUserPromptId(customerId, skipToPromptIdByButtonId, phoneNumber, isReprompt: false);
                        }
                    }
                }
                else if (userPrompt.PromptType == "List")
                {
                    //if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive?.ListReply.Id == null )
                    //{
                    //    await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                    //}
                    //else if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive?.ListReply.Id == "0")
                    //{
                    //    SendNextMessage(customerId, phoneNumber, lastPromptId);
                    //}
                    //

                    if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply.Id == null)
                    {
                        //not received expected
                        await _registrationService.SendPromptByUserPromptId(customerId, lastPromptId, phoneNumber, isReprompt: false);
                    }
                    else
                    {
                        if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply.Id == "0")
                        {
                            SendNextMessage(customerId, phoneNumber, lastPromptId);
                        }
                        else
                        {

                            SaveUserAnswer(notificationPayLoadVM, customerId, ReplyTypes.ListReply);

                            if (userPrompt.FieldName == "Customer.LanguageCode")
                            {
                                string LangCode = "";

                                if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply.Title == "English")
                                {
                                    LangCode = "en";
                                }
                                else if (notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply.Title == "Telugu")
                                {
                                    LangCode = "te";
                                }

                                _whatsAppIntegrator.SaveCustomerData(userPrompt.FieldName, LangCode, customerId);
                                SendNextMessage(customerId, phoneNumber, lastPromptId);

                            }
                            else
                            {
                                //    _whatsAppIntegrator.SaveCustomerData(userPrompt.FieldName, notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply.Title, customerId);
                                //}

                                SendNextMessage(customerId, phoneNumber, lastPromptId);
                            }
                        }

                    }



                    switch (replyType)
                    {
                        case ReplyTypes.AudioReply:
                            // Perform operations for audio messages                           
                            break;

                        case ReplyTypes.AttachmentReply:
                            // Perform operations for document messages                                              
                            break;

                        case ReplyTypes.TextReply:

                            break;
                        case ReplyTypes.ImageReply:
                            // Perform operations for image messages                            
                            break;

                        case ReplyTypes.ButtonReply:

                            break;

                        case ReplyTypes.ListReply:

                            break;
                        default:
                            // Handle unknown message types or provide an error message                            
                            break;
                    }
                }

            }
        }

            public string GetPhoneNumberFromResponse(NotificationPayLoadVM notificationPayLoadVM)
            {
                string phoneNumber = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].From;
                return phoneNumber;
            }

            public int? CreateNewCustomer(string phoneNumber)
            {
                int FlowId = GetRandomFlowId();
                Customer customer = CreateCustomerObject(FlowId, phoneNumber);
                return _customerRepository.CreateCustomer(customer);
                //_customerRepository.CreateCustomer(CreateCustomerObject(GetRandomFlowId(), phoneNumber));
            }

            public Customer CreateCustomerObject(int flowId, string phoneNumber)
            {
                return new Customer()
                {
                    FlowId = flowId,
                    FirstName = "",
                    LastName = "",
                    AadharNumber = "",
                    DOB = "",
                    PANNumber = "",                    
                    CustomerContact = new CustomerContact()
                    {
                        PrimaryPhone = phoneNumber
                    }
                };
            }

            public int GetRandomFlowId()
            {
                //TODO: Write Logic for Getting Random ID
                return 1;
            }

            public bool IsStatusResponse(NotificationPayLoadVM notificationPayLoadVM)
            {
                return (notificationPayLoadVM.Entry[0].Changes[0].Value?.Statuses != null && notificationPayLoadVM.Entry[0].Changes[0].Value?.Statuses?.Count > 0) ? true : false;
            }

            public ReplyTypes GetCustomerReplyType(NotificationPayLoadVM notificationPayLoadVM)
            {
                string replyType = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Type;
                switch (replyType)
                {
                    case "audio":
                        return ReplyTypes.AudioReply;
                    case "document":
                        return ReplyTypes.AttachmentReply;
                    case "text":
                        return ReplyTypes.TextReply;
                    case "image":
                        return ReplyTypes.ImageReply;
                    case "interactive":
                        return notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply?.Id != null ? ReplyTypes.ListReply : ReplyTypes.ButtonReply;
                    case "video":
                        return ReplyTypes.VideoReply;
                    default:
                        return ReplyTypes.Unknown;
                }
            }

            //UserAnswerObjects
            public UserAnswers CreateUserAnswerObject(NotificationPayLoadVM notificationPayLoadVM, int? customerId, ReplyTypes? replyTypes)
            {
                UserAnswers userAnswer = new UserAnswers();

                if (replyTypes == ReplyTypes.ButtonReply)
                {
                    userAnswer.UserAnswer = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ButtonReply.Title;
                }
                else if (replyTypes == ReplyTypes.ListReply)
                {
                    userAnswer.UserAnswer = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Interactive.ListReply.Title;
                }
                else if (replyTypes == ReplyTypes.TextReply)
                {
                    userAnswer.UserAnswer = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Text.Body;
                }
                else if (replyTypes == ReplyTypes.AttachmentReply)
                {

                    string documentId = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Document.DocumentId;
                    string fileName = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Document.Filename;

                    string pathName = ProcessDocumentSaving(documentId, fileName).Result;

                    userAnswer.UserAnswer = pathName;

                    //the doc doesnt open in path
                    //hit getmethod using that
                    //get data save in local
                    //save local path
                    //save it as answer
                }
                userAnswer.WhatsAppId = notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Id;
                userAnswer.AnswerDate = DateTime.UtcNow.ToString();
                userAnswer.CommunicationId = _customerRepository.GetLastCommunicationIdByCustomerId(customerId);
                return userAnswer;
            }

            //SaveUserTypeAnswers
            public void SaveUserAnswer(NotificationPayLoadVM notificationPayLoadVM, int? customerId, ReplyTypes? replyTypes)
            {
                UserAnswers userAnswers = CreateUserAnswerObject(notificationPayLoadVM, customerId, replyTypes);
                _userAnswersRepository.SaveUserAnswer(userAnswers);
            }

            //Validate the current answer using reflection
            public ValidationMessagesObject ValidateAnswer(int? customerId, string phoneNumber)
            {
                var communicationId = _customerRepository.GetLastCommunicationIdByCustomerId(customerId);
                var userPromptsId = _customerRepository.GetUserPromptIdByCommunicationId(communicationId);
                ValidationMessagesObject validationMessagesObject = new ValidationMessagesObject();
                if (userPromptsId != null)
                {
                    string? fieldName = _userPromptsRepository.GetFieldNameByUserPromptId(userPromptsId);
                    string? userAnswer = _userAnswersRepository.GetUserAnswerByCommunicationId(communicationId);
                    string? langCode = _customerRepository.GetLanguageCodeById(customerId);
                    if (langCode == null)
                    {
                        langCode = "en";
                    }
                    validationMessagesObject.ValidationMessages = GetValidationMessagesForAnswer(fieldName, userAnswer, langCode);
                    if (validationMessagesObject.ValidationMessages.Count() > 0)
                    {
                        validationMessagesObject.IsSuccess = false;
                    }
                    else
                    {
                        validationMessagesObject.IsSuccess = true;
                    }
                }
                return validationMessagesObject;
            }

            public List<string> GetValidationMessagesForAnswer(string fieldName, string? userAnswer, string? langCode)
            {
                string[] fieldParts = fieldName.Split('.');

                string className = fieldParts[0];
                string propertyName = fieldParts[1];
                System.Type classType = System.Type.GetType("WhatsAppAPI.Models.Registration." + className);

                System.Type resourceFileTypeBasedOnLangCode = GetResourceFileBasedOnLanguageCode(langCode);

                List<string> validationMessages = new List<string>();
                if (classType != null)
                {
                    PropertyInfo property = classType.GetProperty(propertyName);
                    if (property != null)
                    {
                        object propertyValue = userAnswer;

                        ValidationAttribute[] validationAttributes = property.GetCustomAttributes<ValidationAttribute>(true).ToArray();
                        if (validationAttributes.Length > 0)
                        {
                            foreach (var attribute in validationAttributes)
                            {
                                bool isValid = attribute.IsValid(propertyValue);
                                if (!isValid)
                                {
                                    //attribute.ErrorMessageResourceType = System.Type.GetType("WhatsAppAPI.Models.ValidationMessages." + "ErrorMsgEn");
                                    string errorMessageName = attribute.ErrorMessageResourceName;
                                    ResourceManager resourceManager = new ResourceManager(resourceFileTypeBasedOnLangCode);

                                    string errorMessage = resourceManager.GetString(errorMessageName);

                                    validationMessages.Add(errorMessage);
                                }
                            }
                        }
                    }
                }
                return validationMessages;

            }

            public string? GetActionBasedOnButtonIdForPrompt(int? buttonId, UserPrompts? userPrompts)
            {
                if (userPrompts.Button1Id == buttonId)
                {
                    return userPrompts.Button1ActionType;
                }
                else if (userPrompts.Button2Id == buttonId)
                {
                    return userPrompts.Button2ActionType;
                }
                else if (userPrompts.Button3Id == buttonId)
                {
                    return userPrompts.Button3ActionType;
                }
                return null;
            }


            public int? GetSkipToPromptIdByButtonId(int? buttonId, UserPrompts? userPrompts)
            {
                if (userPrompts.Button1Id == buttonId)
                {
                    return userPrompts.Button1SkipToPromptId;
                }
                else if (userPrompts.Button2Id == buttonId)
                {
                    return userPrompts.Button2SkipToPromptId;
                }
                else if (userPrompts.Button3Id == buttonId)
                {
                    return userPrompts.Button3SkipToPromptId;
                }
                return null;
            }

            public string ProcessValidationResult(ValidationMessagesObject validationMessagesObject)
            {
                string validationMessages = "";
                if (!validationMessagesObject.IsSuccess)
                {
                    foreach (var validationMessage in validationMessagesObject.ValidationMessages)
                    {
                        validationMessages = string.Join(Environment.NewLine, "\u274c" + validationMessage);
                    }
                }
                return validationMessages;
            }

            public void SendNextMessage(int? customerId, string phoneNumber, int? lastPromptId)
            {
                //logic for getting next prompt
                //var lastPromptId = _customerRepository.GetLastUserPromptIdByCustId(customerId);
                UserPrompts? userPrompt = _userPromptsRepository.GetUserPromptByUserPromptId(lastPromptId);
                int? nextPromptId = 0;
                bool isReprompt = false;
                if (lastPromptId == null)
                {
                    nextPromptId = _registrationService.GetNextPromptIdInFlow(lastPromptId, customerId);
                }
                else if (userPrompt != null)
                {
                    if (userPrompt.ReConfirmationPromptId != null)
                    {
                        nextPromptId = _userPromptsRepository.GetReconfirmationPromptIdById(lastPromptId);
                        isReprompt = true;
                    }
                    else
                    {
                        nextPromptId = _registrationService.GetNextPromptIdInFlow(lastPromptId, customerId);
                    }
                }

                _registrationService.SendPromptByUserPromptId(customerId, nextPromptId, phoneNumber, isReprompt);
            }

            public async Task<NotificationPayLoadVM> ConvertResponseToModel(string response)
            {
                NotificationPayLoadVM notificationPayLoadVM1 = new NotificationPayLoadVM();
                try
                {

                    notificationPayLoadVM1 = JsonConvert.DeserializeObject<NotificationPayLoadVM>(response);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //await new AzureStorageService().SaveResponseToAzureFile(response);
                return notificationPayLoadVM1;
            }


            public async Task<string> ProcessDocumentSaving(string mediaID, string fileName)
            {
                try
                {
                    string url = await new WhatsAppService().GetMediaUrlById(mediaID);
                    var savedPath = await new WhatsAppService().GetMediaPathByUrl(url);
                    return savedPath;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public bool IsAlreadyAnsweredQuestion(NotificationPayLoadVM notificationPayLoadVM)
            {
                bool IsAlreadyAnswered = false;
                int? communicationId = _communicationRepository.GetCommunicationIdByWhatsAppId(notificationPayLoadVM.Entry[0].Changes[0].Value.Messages[0].Context?.Id);
                string? userAnswer = _userAnswersRepository.GetUserAnswerByCommunicationId(communicationId);
                if (userAnswer != null)
                {
                    IsAlreadyAnswered = true;
                }
                return IsAlreadyAnswered;

            }

            public System.Type GetResourceFileBasedOnLanguageCode(string? langCode)
            {
                System.Type resourceFile = null;
                if (langCode == "en")
                {
                    resourceFile = System.Type.GetType("WhatsAppAPI.Models.ValidationMessages." + "ErrorMsgEn");
                }
                else if (langCode == "te")
                {
                    resourceFile = System.Type.GetType("WhatsAppAPI.Models.ValidationMessages." + "ErrorMsgTe");
                }
                return resourceFile;
            }

        }
    }

