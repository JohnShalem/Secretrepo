using System.ComponentModel.DataAnnotations;
using WhatsAppAPI.GenericRepository;
using static WhatsAppAPI.Attributes.Attribute;

namespace WhatsAppAPI.Models.Registration
{
    public class Customer 
    {
        [Key]
        public int? CustomerId { get; set; }

        
        [Required(ErrorMessageResourceName = "RequiredAttribute")]
        [NoNumbers(ErrorMessageResourceName = "FirstNameNoNumbers")]
        public string? FirstName { get; set; }

        
        [NoNumbers(ErrorMessageResourceName = "LastNameNoNumbers")]
        public string? LastName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredAttribute")]
        [RegularExpression("(0[1-9]|[12][0-9]|3[01])[-]([0][1-9]|[1][0-2])[-]([0-9]{4})",ErrorMessageResourceName = "DateOfBirth")]
        public string? DOB { get; set; }

        [Required(ErrorMessageResourceName = "RequiredAttribute")]
        [RegularExpression("[1-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}",ErrorMessageResourceName = "AadharNumber")]
        public string? AadharNumber { get; set; }

        [Required(ErrorMessageResourceName = "RequiredAttribute")]
        [RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}",ErrorMessageResourceName = "PanNumber")]
        public string? PANNumber { get; set; }
        public string? LanguageCode { get; set; }
        public int? FlowId{ get; set; }
        public virtual Flow Flow { get; set; }
        public virtual CustomerContact CustomerContact { get; set; }    
        public virtual CustomerDocuments CustomerDocuments { get; set; }  
        public virtual CustomerBank CustomerBank { get; set; }  
    }
}
