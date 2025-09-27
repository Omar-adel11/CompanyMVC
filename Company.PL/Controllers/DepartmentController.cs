using Company.BLL.Interfaces;
using Company.BLL.Repos;
using Company.DAL.Models;
using Company.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _departmentRepo;
        // ask CLR to create object from DepartmentRepo class
       
        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            _departmentRepo =  departmentRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            var departments = _departmentRepo.GetAll();

            return View(departments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Create(CreateDTODepartment model)
        {
            if(ModelState.IsValid) //server-side validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };

                var count = _departmentRepo.Add(department);

                if(count>0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest("Invalid Id");
            var model = _departmentRepo.Get(id.Value);

            if (model is null)
                return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");
            var model = _departmentRepo.Get(id.Value);

            if (model is null)
                return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            var dto = new CreateDTODepartment()
            {
                Code = model.Code,
                Name = model.Name,
                CreateAt = model.CreateAt
            };
            return View(dto);

            //return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // to prevent CSRF attacks OR ANY Request other than form submission (used with post)
        public IActionResult Edit([FromRoute]int id,CreateDTODepartment _departmentDTO)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = id,
                    Code = _departmentDTO.Code,
                    Name = _departmentDTO.Name,
                    CreateAt = _departmentDTO.CreateAt
                };

                int count = _departmentRepo.Update(department);

                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_departmentDTO);
        }

        [HttpGet]  
        public IActionResult Delete(int? id)
        {
            if(id is null)
                return BadRequest("Invalid Id");
            
            var model = _departmentRepo.Get(id.Value);
            if(model is null)
                return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            int value =  _departmentRepo.Delete(model);

            return RedirectToAction(nameof(Index));
        }

    }


}
