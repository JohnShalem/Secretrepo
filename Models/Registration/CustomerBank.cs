using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsAppAPI.Models.Registration
{
    public class CustomerBank
    {
        [Key]
        public int CustomerBankId { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public string? AccountNumber{ get; set; }
        public string? IFSC{ get; set; }        
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
