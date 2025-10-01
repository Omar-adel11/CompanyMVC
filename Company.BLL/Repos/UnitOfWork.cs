using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;

namespace Company.BLL.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentRepo DepartmentRepo { get; }

        public IEmployeeRepo EmployeeRepo { get;  }

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentRepo = new DepartmentRepo(_context);
            EmployeeRepo = new EmployeeRepo(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

       
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
