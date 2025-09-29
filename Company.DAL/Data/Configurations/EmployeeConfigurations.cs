using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=>E.Id).UseIdentityColumn(1,1);

            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.Department)              
                   .WithMany(d => d.Employees)            
                   .HasForeignKey(e => e.DepartmentId)    
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
