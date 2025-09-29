using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repos;
using Company.DAL.Models;
using Company.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IMapper _mapper;

        // ask CLR to create object from DepartmentRepo class

        public EmployeeController(IEmployeeRepo employeeRepo,IDepartmentRepo departmentRepo,IMapper Mapper)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _mapper = Mapper;
        }

        [HttpGet]
        public IActionResult Index(string? searchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(searchInput))
            {
                 employees = _employeeRepo.GetAll();
            }
            else
            {
                 employees = _employeeRepo.GetByName(searchInput);
            }



                ////Dictionary : Key : Value
                ////3 properties to acces data of controller in the view 
                ////             to transfer extra information from controller to view
                ////those properties are inherited from controller base class
                ////1.ViewData : ViewData["Key"] = value; //object

                //ViewData["Message01"] = "Hello from ViewData";

                ////2.ViewBag : ViewBag.Key = value; //dynamic
                ////doesnt not need casting
                //ViewBag.Message02 = "Hello from ViewBag";

                ////3.TempData : TempData["Key"] = value; //object : keep data between 2 requests
                //// to access data after redirection


                return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepo.GetAll();
            ViewData["Departments"] = departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDTOEmployee model)
        {
            if (ModelState.IsValid) //server-side validation
            {
                //Manual Mapping
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Email = model.Email,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    HireDate = model.HireDate,
                //    CreateAt = model.CreateAt,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DepartmentId = model.DepartmentId
                    

                //};

                var employee = _mapper.Map<Employee>(model);

                var count = _employeeRepo.Add(employee);

                if (count > 0)
                {
                    //TempData["Message03"] = "Employee Created successfuly";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest("Invalid Id");
            var model = _employeeRepo.Get(id.Value);

            if (model is null)
                return NotFound(new { StatusCode = 404, Message = $"Employee with Id {id} is Not Found" });
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var employee = _employeeRepo.Get(id.Value);

            if (employee is null)
                return NotFound();

            var depts = _departmentRepo.GetAll();
            ViewData["Departments"] = depts;

            //var dto = new CreateDTOEmployee
            //{
            //    Name = employee.Name,
            //    Email = employee.Email,
            //    Age = employee.Age,
            //    Address = employee.Address,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    HireDate = employee.HireDate,
            //    CreateAt = employee.CreateAt,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    DepartmentId = employee.DepartmentId
            //};

            var dto = _mapper.Map<CreateDTOEmployee>(employee);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // to prevent CSRF attacks OR ANY Request other than form submission (used with post)
        public IActionResult Edit([FromRoute] int id, CreateDTOEmployee _employeeDTO)
        {

            if (ModelState.IsValid) //server-side validation
            {
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = _employeeDTO.Name,
                //    Email = _employeeDTO.Email,
                //    Age = _employeeDTO.Age,
                //    Address = _employeeDTO.Address,
                //    Phone = _employeeDTO.Phone,
                //    Salary = _employeeDTO.Salary,
                //    HireDate = _employeeDTO.HireDate,
                //    CreateAt = _employeeDTO.CreateAt,
                //    IsActive = _employeeDTO.IsActive,
                //    IsDeleted = _employeeDTO.IsDeleted,
                //    DepartmentId = _employeeDTO.DepartmentId

                //};

                var employee = _mapper.Map<Employee>(_employeeDTO);
                employee.Id = id;   
                int count = _employeeRepo.Update(employee);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(_employeeDTO);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var model = _employeeRepo.Get(id.Value);
            if (model is null)
                return NotFound(new { StatusCode = 404, Message = $"Employee with Id {id} is Not Found" });
            int value = _employeeRepo.Delete(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
