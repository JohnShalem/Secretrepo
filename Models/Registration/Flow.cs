using System.ComponentModel.DataAnnotations;

namespace WhatsAppAPI.Models.Registration
{
    public class Flow
    {
        [Key]
        public int FlowId { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Probability { get; set; }
    }
}
