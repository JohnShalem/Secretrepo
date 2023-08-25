namespace WhatsAppAPI.ViewModels.Messages
{
    public class WhatsAppMessageRequest
    {
        public string messaging_product { get; set; }
        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public TextMessage text { get; set; }
    }

    public class TextMessage
    {
        public bool preview_url { get; set; }
        public string body { get; set; }
    }


}
