namespace WhatsAppAPI.MessageObjects
{
    public class ValidationMessagesObject
    {
        public bool IsSuccess { get; set; }
        public List<string> ValidationMessages { get; set; }
    }
}
