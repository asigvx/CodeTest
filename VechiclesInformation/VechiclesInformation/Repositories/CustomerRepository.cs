using System.Collections.Generic;
using System.Linq;
using VechiclesInformation.Models;

namespace VechiclesInformation.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void AddCustomer(CustomerDetails Customerdata)
        {
            _context.CustomerDetails.Add(Customerdata);
            _context.SaveChanges();
        }

        public IEnumerable<CustomerDetails> GetCustomers()
        {
            var customers = _context.CustomerDetails.ToList();
            return customers;
        }
    }
}
