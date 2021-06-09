using System.Collections.Generic;
using System.Linq;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddUser(AuthenticateModel user)
        {
            _context.UsersDetail.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<AuthenticateModel> GetUsers()
        {
            var users = _context.UsersDetail.ToList();
            return users;
        }
    }
}
