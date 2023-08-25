using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsAppAPI.Models.Registration
{
    public class CustomerDocuments
    {
        [Key]
        public int Id { get; set; }
        public string? AadharDocumentPath{ get; set; }
        public string? PanDocumentPath { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }

}
