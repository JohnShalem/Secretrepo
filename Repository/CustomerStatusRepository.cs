using WhatsAppAPI.Data;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class CustomerStatusRepository
    {
        private readonly RegistrationDbContext _context;
        public CustomerStatusRepository(RegistrationDbContext context)
        {
            _context = context;
        }
        public void UpdateCustomerStatus(CustomerStatus customerStatus)
        {
            _context.CustomerStatus.Update(customerStatus);
            _context.SaveChanges();
        }
    }
}
