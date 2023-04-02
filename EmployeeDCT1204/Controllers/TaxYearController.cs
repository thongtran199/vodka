using EmployeeDataAccess;
using EmployeeDCT1204.Models.TaxYear;
using EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using EmployeeEntities;
namespace EmployeeDCT1204.Controllers
{
    public class TaxYearController:Controller
    {
        private ITaxYearService _taxYearService;
        public TaxYearController(ITaxYearService taxYearService)
        {
            _taxYearService = taxYearService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _taxYearService.GetAll().Select(taxYear => new TaxYearIndexViewModel
            {
                Id = taxYear.Id,
                YearOfTax = taxYear.YearOfTax,
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var taxYear = _taxYearService.GetById(id);
            var model = new TaxYearDetailViewModel
            {
                Id = taxYear.Id,
                YearOfTax = taxYear.YearOfTax
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var taxYear = _taxYearService.GetById(id);
            var model = new TaxYearEditViewModel
            {
                Id = taxYear.Id,
                YearOfTax = taxYear.YearOfTax
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var taxYear = _taxYearService.GetById(id);
            var model = new TaxYearDeleteViewModel
            {
                Id = taxYear.Id,
                YearOfTax = taxYear.YearOfTax
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new TaxYearCreateViewModel();  
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxYearCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taxYear = new TaxYear
                {
                    YearOfTax = model.YearOfTax
                }; 
                await _taxYearService.CreateAsSync(taxYear);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TaxYearDeleteViewModel model)
        {
            await _taxYearService.DeleteById(model.Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaxYearEditViewModel model)
        {
            var taxYear = _taxYearService.GetById(model.Id);
            taxYear.YearOfTax = model.YearOfTax;
            await _taxYearService.UpdateAsSync(taxYear);
            return RedirectToAction("Index");
        }
    }
}
