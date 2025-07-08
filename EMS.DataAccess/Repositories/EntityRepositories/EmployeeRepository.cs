using EMS.DataAccess.EMSDbContext;
using EMS.DataAccess.Entities;
using EMS.DataAccess.Interfaces.IRepositories;
using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Employee;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.Repositories.EntityRepositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly EMXDbContext _context;

        public EmployeeRepository(EMXDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResult<Employee>> GetFilteredEmployeesAsync(EmployeeFilterDto filter)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(e => e.Name.Contains(filter.Name));

            if (filter.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == filter.DepartmentId);

            if (filter.Status.HasValue)
                query = query.Where(e => e.Status == filter.Status);

            if (filter.HireDateFrom.HasValue)
                query = query.Where(e => e.HireDate >= filter.HireDateFrom.Value);

            if (filter.HireDateTo.HasValue)
                query = query.Where(e => e.HireDate <= filter.HireDateTo.Value);

            query = (filter.SortBy.ToLower(), filter.SortDirection.ToLower()) switch
            {
                ("name", "desc") => query.OrderByDescending(e => e.Name),
                ("name", "asc") => query.OrderBy(e => e.Name),

                ("hiredate", "desc") => query.OrderByDescending(e => e.HireDate),
                ("hiredate", "asc") => query.OrderBy(e => e.HireDate),

                _ => query.OrderBy(e => e.Name)
            };

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Employee>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }

}
