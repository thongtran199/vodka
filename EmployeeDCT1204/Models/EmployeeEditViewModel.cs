using EmployeeEntities;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDCT1204.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


    }
}
