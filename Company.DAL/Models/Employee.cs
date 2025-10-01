using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreateAt { get; set; }

        public int? DepartmentId { get; set; }
        //Navigation property
        public Department? Department { get; set; }

        public string? ImageName { get; set; } 
    }
}
