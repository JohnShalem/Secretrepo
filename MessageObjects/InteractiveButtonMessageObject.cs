namespace WhatsAppAPI.MessageObjects
{
    public class InteractiveButtonMessageObject
    {

        public InteractiveButtonMessageObject()
        {
            interactive = new Interactive();
            interactive.header = new ListHeader();
            interactive.body= new Body();
            interactive.footer = new Footer();
            interactive.action = new Action();
            
            interactive.action.buttons = new List<Button>();

        }
        public string? messaging_product { get; set; }
        public string? recipient_type { get; set; }
        public string? to { get; set; }
        public string? type { get; set; }
        public Interactive interactive { get; set; }
    }

    public class Reply
    {
        public string? id { get; set; }
        public string? title { get; set; }
    }

    public class Button
    {
        public Button()
        {
            reply = new Reply();
        }
        public string? type { get; set; }
        public Reply? reply { get; set; }
    }

    public class Action
    {
        public List<Button>? buttons { get; set; }
    }

    public class Interactive
    {
        public string? type { get; set; }
        public ListHeader? header { get; set; }
        public Body? body { get; set; }
        public Footer? footer { get; set; }
        public Action? action { get; set; }
    }

  
}



