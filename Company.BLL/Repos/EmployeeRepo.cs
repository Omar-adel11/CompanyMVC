using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;

namespace Company.BLL.Repos
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
       
        public EmployeeRepo(CompanyDbContext context): base(context)
        {
            
        }

     
    }
}
