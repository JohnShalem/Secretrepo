using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class FlowRepository : GenericRepository<Flow>
    {
        private readonly RegistrationDbContext _context;

        public FlowRepository(RegistrationDbContext registrationDbContext): base(registrationDbContext) 
        {
            _context = registrationDbContext;
        }        

    }
}
