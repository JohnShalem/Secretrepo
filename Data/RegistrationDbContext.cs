using Microsoft.EntityFrameworkCore;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Data
{
    public class RegistrationDbContext : DbContext
    {        
        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options):base(options) 
        {
        
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerBank> CustomerBank { get; set; }
        public DbSet<CustomerContact> CustomerContact { get; set; }
        public DbSet<CustomerDocuments> CustomerDocuments { get; set; }
        public DbSet<CustomerStatus> CustomerStatus { get; set; }
        public DbSet<PromptText> PromptText { get; set; }
        public DbSet<UserPrompts> UserPrompts { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowDetails> FlowDetails { get; set; }

    }
}
