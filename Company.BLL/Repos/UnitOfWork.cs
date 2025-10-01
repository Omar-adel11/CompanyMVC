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

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
