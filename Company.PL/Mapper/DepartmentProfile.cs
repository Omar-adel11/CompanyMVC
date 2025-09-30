using AutoMapper;
using Company.DAL.Models;
using Company.PL.DTO;

namespace Company.PL.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, CreateDTODepartment>().ReverseMap();
        }
    }
}
