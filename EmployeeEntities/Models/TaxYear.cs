using System.ComponentModel.DataAnnotations;
namespace EmployeeEntities
{
    public class TaxYear
    {
        [Key]
        public int Id { get; set; }
        public string YearOfTax { get; set; }
    }
}
