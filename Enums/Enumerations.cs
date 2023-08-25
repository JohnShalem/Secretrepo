namespace WhatsAppAPI.Enums
{
    public class Enumerations
    {
        public enum ButtonActionTypes 
        {
            SaveParentAndProceed = 1,
            RepromptParent,
            SkipToNext,
            SkipToPrompt,
            Stop
        }

        public enum ReplyTypes
        {
            TextReply = 1,
            ButtonReply,
            ListReply,
            AttachmentReply,
            AudioReply,
            ImageReply,
            VideoReply,
            Unknown
        }
    }
}
