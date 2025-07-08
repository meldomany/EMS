using EMS.DataAccess.Entities;
using EMS.DataAccess.Interfaces.IRepositories;

namespace EMS.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<LogHistory> LogHistories { get; }
        Task<int> SaveChangesAsync();
    }
}
