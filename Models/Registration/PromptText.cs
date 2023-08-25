using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsAppAPI.Models.Registration
{
    public class PromptText 
    {
        [Key]
        public int PromptTextId{ get; set; }
        public string? LanguageCode { get; set; }
        public string? HeaderText { get; set; }
        public string? MediaType { get; set; }
        public string? BodyText { get; set; }
        public string? FooterText { get; set; }    
        public string? Button1Text { get; set; }        
        public string? Button2Text { get; set; }
        public string? Button3Text { get; set; }        
        public string? ListText { get; set; }
        public int? UserPromptsId { get; set; }
        public virtual UserPrompts UserPrompts { get; set; }

    }
}
