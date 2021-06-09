using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<AuthenticateModel> GetUsers();

        void AddUser(AuthenticateModel user);
    }
}
