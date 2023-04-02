using System.ComponentModel.DataAnnotations;

namespace EmployeeDCT1204.Models.TaxYear
{
    public class TaxYearCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Year Off Tax is required"), StringLength(50, MinimumLength = 2)]
        public string YearOfTax { get; set; }
    }
}
