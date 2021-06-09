using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VechiclesInformation.Models;

namespace VechiclesInformation.Integrations
{
    public class SyncService: ISyncService
    {
        private readonly ApplicationContext _context;

        public SyncService(ApplicationContext context)
        {
            _context = context;
        }

        public void SyncVehicleStatus()
        {
            foreach (var vehicle in GetVehicleDetails())
            {
                var v = _context.VehiclesDetail.Find(vehicle.VehicleId);
                v.VehicleStatus = GetStatus(v.VehicleId);
                _context.VehiclesDetail.Update(v);
                _context.SaveChanges();
            }
        }

        private IEnumerable<VehicleDetails> GetVehicleDetails()
        {
            var vehicles = _context.VehiclesDetail.ToList();
            return vehicles;
        }

        private bool GetStatus(string id)
        {
            var random = new Random();
            if (random.Next(100) < 50)
                return false;
            else
                return true;
        }


    }
}
