using AutoMapper;
using Company.DAL.Models;
using Company.PL.DTO;
namespace Company.PL.Mapper
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,CreateDTOEmployee>().ReverseMap();

            /*CreateMap<Employee, CreateDTOEmployee>().ForMember(d => d.Name, o => o.MapFrom(e => e.Name))
                                                    .ReverseMap();*/

        }
    }
}
