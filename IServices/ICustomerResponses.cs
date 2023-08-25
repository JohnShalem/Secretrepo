using WhatsAppAPI.MessageObjects;
using WhatsAppAPI.Models.Registration;
using WhatsAppAPI.ViewModels;
using WhatsAppAPI.ViewModels.WebHook;
using static WhatsAppAPI.Enums.Enumerations;

namespace WhatsAppAPI.IServices
{
    public interface ICustomerResponses
    {        
        string GetPhoneNumberFromResponse(NotificationPayLoadVM notificationPayLoadVM);
        int? CreateNewCustomer(string phoneNumber);
        Customer CreateCustomerObject(int flowId, string phoneNumber);
        int GetRandomFlowId();       
        bool IsStatusResponse(NotificationPayLoadVM notificationPayLoadVM);
        ReplyTypes GetCustomerReplyType(NotificationPayLoadVM notificationPayLoadVM);
        ValidationMessagesObject ValidateAnswer(int? customerId, string? phoneNumber);                    
        List<string> GetValidationMessagesForAnswer(string fieldName, string? userAnswer);        
        string ProcessValidationResult(ValidationMessagesObject validationMessagesObject);        
        void SaveUserAnswer(NotificationPayLoadVM notificationPayLoadVM, int? customerId, ReplyTypes? replyTypes);
        UserAnswers CreateUserAnswerObject(NotificationPayLoadVM notificationPayLoadVM, int? customerId, ReplyTypes? replyTypes);
        void SendNextMessage(int? customerId, string phoneNumber, int? lastPromptId);
        //NotificationPayLoadVM ConvertResponseToModel(string response);        
        Task<NotificationPayLoadVM> ConvertResponseToModel(string response);
        Task<string> ProcessDocumentSaving(string mediaID, string fileName);
        int? GetSkipToPromptIdByButtonId(int? buttonId, UserPrompts? userPrompts);        
        Task ProcessCustomerResponse(Stream stream);
    }
}
