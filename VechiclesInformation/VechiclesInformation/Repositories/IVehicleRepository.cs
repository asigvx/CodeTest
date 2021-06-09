using System.Collections.Generic;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public interface IVehicleRepository
    {
        IEnumerable<CustomerDetails> GetVechiles();
        //VehicleDetails Get(int id);

        CustomerDetails GetVehiclesByCustomer(string CustomerName);

        IEnumerable<CustomerDetails> GetVehiclesByStatus(bool vehicleStatus);
        void AddVechile(VehicleDetails Vehicledata);
    }
}
