using Microsoft.EntityFrameworkCore;

namespace VechiclesInformation.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
        }

        public DbSet<VehicleDetails> VehiclesDetail { get; set; }

        public DbSet<AuthenticateModel> UsersDetail { get; set; }

        public DbSet<CustomerDetails> CustomerDetails { get; set; }

    }
}
