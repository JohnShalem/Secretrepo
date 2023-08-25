using WhatsAppAPI.MessageObjects;
using WhatsAppAPI.Models;
using WhatsAppAPI.ViewModels;

namespace WhatsAppAPI.IServices
{
    public interface IWhatsAppService
    {       
        Task<SentMessageResponseViewModel> SendTextMessage(TextMessageObject textMessageObject);
        Task<SentMessageResponseViewModel> SendListMessage(InteractiveListMessageObject interactiveListMessageObject);
        Task<SentMessageResponseViewModel> SendButtonMessage(InteractiveButtonMessageObject interactiveButtonMessageObject);
        Task<SentMessageResponseViewModel> SendRandomMessage(string mobile, string message);
        Task<string> GetMediaUrlById(string mediaId);        
        Task<string> GetMediaPathByUrl(string mediaUrl);
    }
}
