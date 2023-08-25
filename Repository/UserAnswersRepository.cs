using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class UserAnswersRepository : GenericRepository<UserAnswers>
    {
        private readonly RegistrationDbContext _context;

        public UserAnswersRepository(RegistrationDbContext registrationDbContext):base(registrationDbContext) 
        {
            _context = registrationDbContext;
        }

        public bool SaveUserAnswer(UserAnswers userAnswers)
        {
            Add(userAnswers);
            SaveChanges();
            return true;
        }

        public string? GetUserAnswerByCommunicationId(int? communicationId) 
        {
            var UserAnswer = (from userAnswer in _context.UserAnswers
                               where userAnswer.CommunicationId == communicationId
                               orderby userAnswer.CommunicationId
                              select userAnswer.UserAnswer).LastOrDefault();
            return UserAnswer;
        }
    }
}
