using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepo : IGenericRepo<Employee>
    {
        Task<List<Employee>> GetByNameAsync(string name);

    }
}
