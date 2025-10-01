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
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;
        public GenericRepo(CompanyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _context.Employees.Include("Department").ToList();
            }
            var result = _context.Set<T>().ToList();
            return result;
        }

        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _context.Employees.Include(E => E.Department).FirstOrDefault(e => e.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }

        public void Add(T model)
        {
            _context.Set<T>().Add(model);
            
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            
        }

        public void Update(T model)
        {
            _context.Set<T>().Update(model);
           
        }
    }
}
