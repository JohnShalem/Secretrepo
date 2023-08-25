using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace WhatsAppAPI.ViewModels.WebHook
{
    public class NotificationPayLoadVM
    {

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; }

    }
    public class Metadata
    {
        [JsonProperty("display_phone_number")]
        public string DisplayPhoneNumber { get; set; }

        [JsonProperty("phone_number_id")]
        public string PhoneNumberId { get; set; }
    }

    public class Contact
    {
        [JsonProperty("wa_id")]
        public string WaId { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        // Other profile properties
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_data")]
        public Details Error_Data { get; set; }
    }

    public class Details
    {
        [JsonProperty("details")]
        public string Detail { get; set; }
    }

    public class Value
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("statuses")]
        public List<Status> Statuses { get; set; }
    }


    public class Status
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("recipient_id")]
        public string Recipient_id { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        [JsonProperty("pricing")]
        public object pricing { get; set; }

        [JsonProperty("conversation")]
        public Conversation Conversation { get; set; }

    }

    public class Origin
    {
        public string type { get; set; }
    }

    public class Conversation
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("origin")]
        public Origin origin { get; set; }
        [JsonProperty("expiration_timestamp")]
        public string expiration_timestamp { get; set; }
    }

    public class Message
    {
        public Message()
        {
            Interactive = new Type();
        }
        
        [JsonProperty("audio")]
        public Audio Audio { get; set; }

        [JsonProperty("button")]
        public Button? Button { get; set; }

        [JsonProperty("context")]
        public Context? Context { get; set; }

        [JsonProperty("document")]
        public Documents Document { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("identity")]
        public Identity Identity { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("interactive")]
        public Type? Interactive { get; set; }

        [JsonProperty("order")]
        public object Order { get; set; }

        [JsonProperty("referral")]
        public Referral Referral { get; set; }

        [JsonProperty("sticker")]
        public Sticker Sticker { get; set; }

        [JsonProperty("system")]
        public SystemUpdate System { get; set; }

        [JsonProperty("text")]
        public MessageText Text { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("video")]
        public Video Video { get; set; }

    }



    public class Audio
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("mime_type ")]
        public string Mime_type { get; set; }
    }

    public class Context
    {
        [JsonProperty("forwarded")]
        public bool Forwarded { get; set; }

        [JsonProperty("frequently_forwarded")]
        public bool FrequentlyForwarded { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("referred_product")]
        public ReferredProduct ReferredProduct { get; set; }

    }

    public class ReferredProduct
    {
        [JsonProperty("catalog_id")]
        public string Id { get; set; }

        [JsonProperty("product_retailer_id")]
        public string ProductRetailerId { get; set; }
    }



    public class Documents
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("mime_type")]
        public string Mime_type { get; set; }

        [JsonProperty("id")]
        public string? DocumentId { get; set; }
    }


    public class Identity
    {
        [JsonProperty("created_timestamp")]
        public string CreatedTimeStamp { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }
    }

    public class Image
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("mime_type")]
        public string Mime_type { get; set; }

        [JsonProperty("id")]
        public string ImageId { get; set; }

    }

    public class ButtonReply
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class ListReply
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Type
    {
        public Type()
        {
            ButtonReply = new ButtonReply();
            ListReply = new ListReply();
        }

        [JsonProperty("button_reply")]
        public ButtonReply? ButtonReply { get; set; }        

        [JsonProperty("list_reply")]
        public ListReply? ListReply { get; set; }
    }


    public class Video
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("mime_type")]
        public string Mime_type { get; set; }

        [JsonProperty("id")]
        public string VideoId { get; set; }

    }


    public class Change
    {
        [JsonProperty("value")]
        public Value Value { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }
    }

    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("changes")]
        public List<Change> Changes { get; set; }
    }



    public class Button
    {
        [JsonProperty("payload")]
        public string? Payload { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }
    }



    public class Referral
    {
        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }

        [JsonProperty("headline")]
        public string Headline { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
    }

    public class Sticker
    {
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("animated")]
        public bool Animated { get; set; }
    }

    public class SystemUpdate
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("identity")]
        public string Identity { get; set; }

        [JsonProperty("new_wa_id")]
        public string NewWhatsAppId { get; set; }

        [JsonProperty("wa_id")]
        public string WhatsAppId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class MessageText
    {
        [JsonProperty("body")]
        public string Body { get; set; }

    }



}

