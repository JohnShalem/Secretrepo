using System.ComponentModel.DataAnnotations;

namespace WhatsAppAPI.Models.Registration
{
    public class FlowDetails
    {
        [Key]
        public int FlowDetailsId { get; set; }        
        public int? FlowId { get; set; }
        public int? UserPromptsId { get; set; }
        public virtual Flow Flow { get; set; }
        public virtual UserPrompts UserPrompts { get; set; }

    }
}
