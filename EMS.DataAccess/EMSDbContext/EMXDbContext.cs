using EMS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.EMSDbContext
{
    public class EMXDbContext : DbContext
    {
        public EMXDbContext(DbContextOptions<EMXDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LogHistory> LogHistories { get; set; }
    }
}
