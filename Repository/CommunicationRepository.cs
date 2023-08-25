using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class CommunicationRepository : GenericRepository<Communication>
    {
        private readonly RegistrationDbContext _context;
        public CommunicationRepository(RegistrationDbContext registrationDbContext) : base(registrationDbContext)
        {
            this._context = registrationDbContext;
        }

        public int CreateCommunication(Communication communication)
        {
           int communicationId  = Add(communication).CommunicationId;
            SaveChanges();
           return communicationId;
        }

        public int? GetLastCommunicationIdByUserPromptId(int? userPromptId)
        {
            int? communicationId = (from communication in _context.Communication
                                    where communication.UserPromptsId == userPromptId
                                    orderby communication.CommunicationId
                                    select communication.CommunicationId
                                    ).LastOrDefault();
            return communicationId;
        }

        public Communication GetCommunicationById(int? communicationId)
        {
            return _context.Communication.Where(x => x.CommunicationId == communicationId)?.FirstOrDefault();
        }

        public int? GetCommunicationIdByWhatsAppId(string? whatsAppId)
        {
            return _context.Communication.Where(x => x.WhatsAppId == whatsAppId)?.Select(x => x.CommunicationId).FirstOrDefault();
        }


    }
}
