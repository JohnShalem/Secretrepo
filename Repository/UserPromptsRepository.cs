using WhatsAppAPI.Data;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class UserPromptsRepository
    {
        private readonly RegistrationDbContext _context;
        public UserPromptsRepository(RegistrationDbContext context)
        {
            _context = context;
        }

        public string GetFieldNameByUserPromptId(int? userPromptId)
        {
            var FieldName = (from userPrompts in _context.UserPrompts                             
                             where userPrompts.UserPromptsId == userPromptId
                              select userPrompts.FieldName).FirstOrDefault();
            return FieldName;
        }


        public PromptText GetPromptTextByUserPromptId(int? Id,string? languageCode)
        {
            var PromptText = (from userPrompts in _context.UserPrompts
                              join promptText in _context.PromptText
                              on userPrompts.UserPromptsId equals promptText.UserPromptsId
                              where userPrompts.UserPromptsId == Id && promptText.LanguageCode == languageCode
                              select promptText).FirstOrDefault();
            return PromptText;
        }
        public UserPrompts? GetUserPromptByUserPromptId(int? Id)
        {
            var UserPrompts = (from userPrompts in _context.UserPrompts
                          where userPrompts.UserPromptsId == Id
                          select userPrompts).FirstOrDefault();
            return UserPrompts;
        }
       
        public string GetActionBasedOnButtonIdForPrompt(int? buttonId,UserPrompts userPrompt)
        {
            return "";
        }

        public int? GetParentPromptIdByChildPromptId(int? childPromptId)
        {
            return _context.UserPrompts.Where(x => x.ReConfirmationPromptId == childPromptId).FirstOrDefault().UserPromptsId;
        }

        public int? GetReconfirmationPromptIdById(int? userPromptId)
        {
            return _context.UserPrompts.Where(x => x.UserPromptsId == userPromptId).Select(x => x.ReConfirmationPromptId).FirstOrDefault();
        }

        public UserPrompts? GetChildPromptByParentPromptId(int? userPromptId)
        {
            int? reconfirmationid =  GetReconfirmationPromptIdById(userPromptId);

            return _context.UserPrompts.Where(x => x.UserPromptsId == reconfirmationid).FirstOrDefault();
        }

        public int? GetChildPromptIdByParentPromptId(int? userPromptId)
        {
            int? reconfirmationid = GetReconfirmationPromptIdById(userPromptId);

            return _context.UserPrompts.Where(x => x.UserPromptsId == reconfirmationid).FirstOrDefault().UserPromptsId;
        }



    }
}
