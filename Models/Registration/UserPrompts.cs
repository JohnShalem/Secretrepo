using System.ComponentModel.DataAnnotations;

namespace WhatsAppAPI.Models.Registration
{
    public class UserPrompts
    {
        [Key]
        public int UserPromptsId { get; set; }
        public string? FieldName { get; set; }
        public string? Description { get; set; }
        public string? PromptType{ get; set; }        
        public int? Button1Id{ get; set; }
        public string? Button1ActionType{ get; set; }
        public int? Button1SkipToPromptId{ get; set; }
        public int? Button2Id { get; set; }
        public string? Button2ActionType { get; set; }
        public int? Button2SkipToPromptId { get; set; }
        public int? Button3Id { get; set; }
        public string? Button3ActionType { get; set; }
        public int? Button3SkipToPromptId { get; set; }
        public string? ListIds { get; set; }
        public bool? IsEndOFFlow { get; set; }
        public bool? AnswerShouldBeAttachment { get; set; }
        public bool? PromptNeedsReformatting { get; set; }
        public bool? IsModifyable { get; set; }
        public int? ReConfirmationPromptId { get; set; }
    }
}
