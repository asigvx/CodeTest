using System.Collections.Generic;

namespace MonitoringOfVehicles.Models
{
    public class CustomerDetails
    {
        
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public ICollection<VehicleDetails> Vehicles { get; set; }
    }
}