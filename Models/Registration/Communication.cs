using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsAppAPI.Models.Registration
{
    public class Communication
    {
        [Key]
        public int CommunicationId { get; set; }
        public string? SentDate { get; set; }
        public string? DeliveredDate { get; set; }
        public string? ReadDate { get; set; }
        public string? WhatsAppId { get; set; }
        public string? LanguageCode { get; set; }
        public bool? IsRePrompt { get; set; }
        public int? UserPromptsId { get; set; }
        public virtual UserPrompts UserPrompts { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
