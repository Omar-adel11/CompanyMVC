using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Repos
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        private readonly CompanyDbContext _context;
        public EmployeeRepo(CompanyDbContext context): base(context)
        {
            _context = context;
        }

        public CompanyDbContext Context { get; }

        public List<Employee> GetByName(string name)
        {
            return _context.Employees.Include(e=>e.Department).Where(e => e.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
