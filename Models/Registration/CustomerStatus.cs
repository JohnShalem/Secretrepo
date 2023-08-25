using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsAppAPI.Models.Registration
{
    public class CustomerStatus
    {
        [Key]
        public int CustomerStatusId { get; set; }       
        public CurrentStatus? Status { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }

    public enum CurrentStatus
    {
        RegistrationStarted,
        PersonalDetails,
        AddressDetails
    }
}
