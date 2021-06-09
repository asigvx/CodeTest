using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VechiclesInformation.Models
{
    public class VehicleDetails
    {
        [ForeignKey("CustomerId")]
        
        [Key]
        [Required]
        public string VehicleId { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public bool VehicleStatus { get; set; }

        //public CustomerDetails Customer { get; set; }
    }
}
