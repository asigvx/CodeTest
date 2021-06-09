using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VechiclesInformation.Integrations;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

namespace VechiclesInformation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public readonly IVehicleRepository _vehicleRepository;
        public readonly ISyncService _syncService;

        public VehicleController(IVehicleRepository vehicleRepository, ISyncService syncService)
        {
            _vehicleRepository = vehicleRepository;
            _syncService = syncService;
        }

        [HttpGet("sync")]
        public void UpdateVehicleStatus()
        {
            _syncService.SyncVehicleStatus();
        }

        [HttpGet]
        public IEnumerable<CustomerDetails> GetVehicles()
        {
            var vehicles = _vehicleRepository.GetVechiles();
            return vehicles;
        }

        [HttpGet("name/{customerName}")]
        public CustomerDetails GetVehiclesForCustomer(string customerName)
        {
            return _vehicleRepository.GetVehiclesByCustomer(customerName);
        }

        [HttpGet("status/{vehicleStatus}")]
        public IEnumerable<CustomerDetails> GetVehiclesByStatus(bool vehicleStatus)
        {
            return _vehicleRepository.GetVehiclesByStatus(vehicleStatus);
        }


        [HttpPost]
        public void AddVehicle([FromBody] VehicleDetails Vehicledata)
        {
            _vehicleRepository.AddVechile(Vehicledata);
        }

        

    }
}
