using EmployeeEntities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmployeeDCT1204.Models
{
    public class EmployeeCreateViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "First Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = " Employee Number")]
        public string EmployeeNo { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        [Display(Name = "Photo")]
        public IFormFile? ImageUrl { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }

        [Required, StringLength(50), Display(Name = "NI No.")]
        [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
        public string NationalInsuranceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan StudentLoan { get; set; }
        public UnionMember UnionMember { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        //public ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
