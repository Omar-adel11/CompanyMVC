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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include("Department").ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
            
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(e => e.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }

        public async Task AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            
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
