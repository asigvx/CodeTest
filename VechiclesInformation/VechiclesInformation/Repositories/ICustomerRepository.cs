using System.Collections.Generic;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerDetails> GetCustomers();

        void AddCustomer(CustomerDetails Customerdata);

    }
}
