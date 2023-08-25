using WhatsAppAPI.MessageObjects;
using WhatsAppAPI.Models.Registration;
using WhatsAppAPI.ViewModels;

namespace WhatsAppAPI.IServices
{
    public interface IRegistrationService
    {
        //void ContinueRegistration(string response);
        int? GetNextPromptIdInFlow(int? lastPromptId, int? customerId);
        int? GetFlowId(int? customerId);
        int? GetFirstPromptId(List<FlowDetails> flowDetails);        
        //void SendPromptByUserPromptId(int? customerId, int? userPromptId, string phoneNumber, bool isReprompt);        
       
        Communication CreateCommunicationObject(int? customerId, int? userPromptId, bool isReprompt);
        int SaveCommunication(int? customerId, int? userPromptId, bool isReprompt);
        void UpdateCommunication(SentMessageResponseViewModel sentMessageResponseViewModel, int communicationId);

        TextMessageObject CreateTextMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber);
        InteractiveListMessageObject CreateInteractiveListMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber);        
        InteractiveButtonMessageObject CreateOptionalTextMessageObject(PromptText promptText, string phoneNumber);
        InteractiveListMessageObject CreateOptionalListMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber);        
        InteractiveButtonMessageObject CreateInteractiveButtonMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber, FormattedResult? formattedResult);
        InteractiveButtonMessageObject CreateOptionalButtonMessageObject(UserPrompts userPrompt, PromptText promptText, string phoneNumber, FormattedResult? formattedResult);
        Task SendPromptByUserPromptId(int? customerId, int? userPromptId, string phoneNumber, bool isReprompt);
    }
}
