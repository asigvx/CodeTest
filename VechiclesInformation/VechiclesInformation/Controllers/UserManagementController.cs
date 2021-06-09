using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VechiclesInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        public readonly IUserRepository _userRepository;

        public UserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserManagementController>
        [HttpGet]
        public IEnumerable<AuthenticateModel> Get()
        {
            return _userRepository.GetUsers();
        }

        // POST api/<UserManagementController>
        [HttpPost]
        public void Post([FromBody] AuthenticateModel user)
        {
            _userRepository.AddUser(user);
        }
    }
}
