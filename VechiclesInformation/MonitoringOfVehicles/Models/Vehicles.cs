using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoringOfVehicles.Models
{
    public class Vehicles
    {
        public string VehicleId { get; set; }
        public string RegistrationNumber { get; set; }

        public bool VehicleStatus { get; set; }
    }
}