using System.Collections.Generic;
using System.Linq;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationContext _context;

        public VehicleRepository (ApplicationContext context)
        {
            _context = context;
        }
        //public VehicleDetails Get(int id)
        //{
        //    return _context.VehiclesDetail.Find(id);
        //}

        public CustomerDetails GetVehiclesByCustomer(string CustomerName)
        {
            var customer = _context.CustomerDetails.FirstOrDefault(x => x.CustomerName == CustomerName); ;
            if (customer == null) return null;
            _context.Entry(customer)
                .Collection(v => v.Vehicles)
                .Load();
            return customer;
        }

        public IEnumerable<CustomerDetails> GetVehiclesByStatus(bool vehicleStatus)
        {
            var customers = _context.CustomerDetails;
            if (customers == null) return null;
            var custList = new List<CustomerDetails>();
            foreach (var customer in customers)
            {
                _context.Entry(customer)
                .Collection(v => v.Vehicles)
                .Load();
                foreach (var vehicle in customer.Vehicles)
                {
                    if (vehicle.VehicleStatus != vehicleStatus)
                        customer.Vehicles.Remove(vehicle);
                }
                if(customer.Vehicles.Count() > 0)
                {
                    custList.Add(customer);
                }
            }
            return custList;
        }
        public IEnumerable<CustomerDetails> GetVechiles()
        {
            var customers = _context.CustomerDetails;
            if (customers == null) return null;
            foreach (var customer in customers)
            {
                _context.Entry(customer)
                    .Collection(v => v.Vehicles)
                    .Load();
            }
            return customers;
        }

        public void AddVechile(VehicleDetails Vehicledata)
        {
            _context.VehiclesDetail.Add(Vehicledata);
            _context.SaveChanges();
        }
    }
}
