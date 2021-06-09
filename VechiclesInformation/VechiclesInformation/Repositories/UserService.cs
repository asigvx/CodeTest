using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public interface IUserService
    {
        Task<AuthenticateModel> Authenticate(string username, string password);
    }

    public class UserService: IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticateModel> Authenticate(string username, string password)
        {
            List<AuthenticateModel> _users = _userRepository.GetUsers().ToList();
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return new AuthenticateModel
            {
                Username = username,
                Password = null
            };
        }
    }
}
