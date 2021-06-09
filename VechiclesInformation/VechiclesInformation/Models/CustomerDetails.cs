using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VechiclesInformation.Models
{
    public class CustomerDetails
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public ICollection<VehicleDetails> Vehicles { get; set; }
    }
}
