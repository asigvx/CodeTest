
using System.Collections.Generic;


namespace MonitoringOfVehicles.Models
{
    public class VehicleDetails
    {
        //public int CustomerId { get; set; }
        ////public string CustomerName { get; set; }
        ////public string Address { get; set; }

        //public string VehicleId { get; set; }
        //public string RegistrationNumber { get; set; }

        //public bool VehicleStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicles> Vehicles { get; set; }
    }
}