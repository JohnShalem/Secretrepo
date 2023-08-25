using System.ComponentModel.DataAnnotations;

namespace WhatsAppAPI.Models.Registration
{
    public class UserAnswers
    {
        [Key]
        public int Id { get; set; }
        public string? UserAnswer { get; set; }
        public string? AnswerDate { get; set; }
        public string? WhatsAppId { get; set; }
        public int? CommunicationId { get; set; }
        public virtual Communication Communication { get; set; }

    }
}
