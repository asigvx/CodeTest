using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

namespace VechiclesInformation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        public readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        [HttpPost]
        public void AddCustomer([FromBody] CustomerDetails Customerdata)
        {
            _customerRepository.AddCustomer(Customerdata);
        }

        [HttpGet]
        public IEnumerable<CustomerDetails> GetCustomers()
        {
            var abc = _customerRepository.GetCustomers();
            return abc;
        }
    }
}
