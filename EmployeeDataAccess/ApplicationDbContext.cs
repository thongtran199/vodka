using EmployeeEntities;
using Microsoft.EntityFrameworkCore;
namespace EmployeeDataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<PaymentRecord> PaymentRecord { get; set; }
        public DbSet<TaxYear> TaxYear { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }
    }
}
