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
    public class DepartmentRepo :GenericRepo<Department>, IDepartmentRepo
    {

        public DepartmentRepo(CompanyDbContext context) : base(context)
        {

        }


    }
}
