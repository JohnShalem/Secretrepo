using System.Reflection.Metadata;

namespace WhatsAppAPI.MessageObjects
{
    public class InteractiveListMessageObject
    {
        public InteractiveListMessageObject()
        {
            interactive = new InteractiveList();
        }

        public string messaging_product { get; set; }
        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public InteractiveList interactive { get; set; }
    }

    public class ListHeader
    {
        public ListHeader()
        {
            document = new Documents();
            image = new Image();
            video = new Video();
        }
        public string type { get; set; }
        public Documents document { get; set; }
        public Image image { get; set; }
        public Video video { get; set; }
        public string? text { get; set; }
    }

    public class Body
    {
        public string ?text { get; set; }
    }

    public class Footer
    {
        public string ?text { get; set; }
    }

    public class Row
    {
        public string? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

    public class Section
    {
        public string title { get; set; }
        public List<Row> rows { get; set; }
    }

    public class ListAction
    {
        public string? button { get; set; }
        public List<Section> sections { get; set; }
    }

    public class InteractiveList
    {
        public InteractiveList()
        {
            header = new ListHeader();
            body = new Body();
            footer = new Footer();
            action = new ListAction();
        }
        public string type { get; set; }
        public ListHeader header { get; set; }
        public Body body { get; set; }
        public Footer footer { get; set; }
        public ListAction action { get; set; }
    }
    public class Documents
    {
        public string id { get; set; }
        public string link { get; set; }
    }

    public class Image
    {
        public string id { get; set; }
        public string link { get; set; }
    }

    public class Video
    {
        public string id { get; set; }
        public string link { get; set; }
    }

}










