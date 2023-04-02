using EmployeeDCT1204.Models;
using EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using EmployeeEntities;
using Microsoft.EntityFrameworkCore;
using EmployeeDataAccess;

namespace EmployeeDCT1204.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private IWebHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var model = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.ID,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                City = employee.City
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            Console.WriteLine("Ngoai vong if");
            if (ModelState.IsValid)
            {
                Console.WriteLine("Trong vong if");
                var employee = new Employee
                {
                    //ID = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    EmployeeNo = model.EmployeeNo,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    Designation = model.Designation,
                    Phone = model.Phone,
                    Email = model.Email,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    Address = model.Address,
                    City = model.City,
                    Postcode = model.Postcode,
                    PaymentMethod = model.PaymentMethod,
                    UnionMember = model.UnionMember,
                    StudentLoan = model.StudentLoan,
                    PaymentRecord = new List<PaymentRecord>()
                };
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employees";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webrootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webrootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.CreateAsSync(employee);
                return RedirectToAction("Index");
            }
            foreach (var key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var employee = _employeeService.GetById(id);
            var model = new EmployeeDetailViewModel
            {
                Id = employee.ID,
                FullName = employee.FullName,
                EmployeeNo = employee.EmployeeNo,
                Gender = employee.Gender,
                City = employee.City,
                DateJoined = employee.DateJoined,
                ImageUrl = employee.ImageUrl,
                Designation = employee.Designation,
                PaymentMethod = employee.PaymentMethod
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var employee = _employeeService.GetById(id);
            var model = new EmployeeDeleteViewModel
            {
                Id = employee.ID,
                FullName = employee.FullName,
                EmployeeNo = employee.EmployeeNo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            await _employeeService.DeleteById(model.Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var employee = _employeeService.GetById(id);
            var model = new EmployeeEditViewModel
            {
                Id = employee.ID,
                DOB = employee.DOB,
                FullName = employee.FullName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            var em = _employeeService.GetById(model.Id);
            em.FullName = model.FullName;
            em.DOB = model.DOB;
            em.PaymentMethod = model.PaymentMethod;

            await _employeeService.UpdateAsSync(em);
            return RedirectToAction("Index");

        }
    }
}
