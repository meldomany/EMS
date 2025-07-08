using EMS.DataAccess.EMSDbContext;
using EMS.DataAccess.Entities;
using EMS.DataAccess.Interfaces;
using EMS.DataAccess.Interfaces.IRepositories;
using EMS.DataAccess.Repositories.EntityRepositories;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EMXDbContext _context;

        public IEmployeeRepository Employees { get; }
        public IGenericRepository<Department> Departments { get; }
        public IGenericRepository<LogHistory> LogHistories { get; }

        public UnitOfWork(EMXDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Departments = new GenericRepository<Department>(_context);
            LogHistories = new GenericRepository<LogHistory>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var auditLogs = OnBeforeSaveChanges();

                foreach (var log in auditLogs)
                    _context.LogHistories.Add(log);

                var result = await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        private List<LogHistory> OnBeforeSaveChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var auditLogs = new List<LogHistory>();

            var entries = _context.ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity is LogHistory || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var audit = new LogHistory
                {
                    Table = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(), // Added, Modified, Deleted
                    Timestamp = DateTime.UtcNow
                };

                auditLogs.Add(audit);
            }

            return auditLogs;
        }

        public void Dispose() => _context.Dispose();
    }

}
