using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class CustomerContactRepository : GenericRepository<CustomerContact>
    {
        public CustomerContactRepository(RegistrationDbContext registrationDbContext) : base(registrationDbContext) { }
    
    }
}
