using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WhatsAppAPI.MessageObjects
{

    public class TextMessageObject
    {
        public TextMessageObject()
        {
            text = new TextContent();
        }

        public string messaging_product { get;set; } 
        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public TextContent text { get; set; }
    }

    public class TextContent
    {
        public string? body { get; set; }
        public bool preview_url { get; set; }
    }

}
