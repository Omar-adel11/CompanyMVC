using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTO
{
    public class CreateDTODepartment
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Created At is required")]
        public DateTime CreateAt { get; set; }

        
    }
}
