using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ask CLR to create object from DepartmentRepo class

        public DepartmentController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
           
            
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var departments = await _unitOfWork.DepartmentRepo.GetAllAsync();

            return View(departments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(CreateDTODepartment model)
        {
            if(ModelState.IsValid) //server-side validation
            {
                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //};
                var department = _mapper.Map<Department>(model);


                await _unitOfWork.DepartmentRepo.AddAsync(department);
                var count = await _unitOfWork.SaveAsync();

                if (count>0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest("Invalid Id");
            var model = await _unitOfWork.DepartmentRepo.GetAsync(id.Value);

            if (model is null)
                return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");
            var model = await _unitOfWork.DepartmentRepo.GetAsync(id.Value);

            if (model is null)
                return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            //var dto = new CreateDTODepartment()
            //{
            //    Code = model.Code,
            //    Name = model.Name,
            //    CreateAt = model.CreateAt
            //};

            var dto = _mapper.Map<CreateDTODepartment>(model);
            return View(dto);

            //return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // to prevent CSRF attacks OR ANY Request other than form submission (used with post)
        public async Task<IActionResult> Edit([FromRoute]int id,CreateDTODepartment _departmentDTO)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id = id,
                //    Code = _departmentDTO.Code,
                //    Name = _departmentDTO.Name,
                //    CreateAt = _departmentDTO.CreateAt
                //};
                var department = _mapper.Map<Department>(_departmentDTO);
                department.Id = id;
                _unitOfWork.DepartmentRepo.Update(department);
                var count = await _unitOfWork.SaveAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_departmentDTO);
        }

        [HttpGet]  
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null)
                return BadRequest("Invalid Id");
            
            var model = await _unitOfWork.DepartmentRepo.GetAsync(id.Value);
            if(model is null)
                return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            _unitOfWork.DepartmentRepo.Delete(model);
            var value = await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

    }


}
