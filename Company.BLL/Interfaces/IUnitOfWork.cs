using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IDepartmentRepo DepartmentRepo { get; }
        IEmployeeRepo EmployeeRepo { get; }

        Task<int> SaveAsync();
    }
}
