
using System.ComponentModel.DataAnnotations;

namespace EmployeeEntities
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string NationalInsuranceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan StudentLoan { get; set; }
        public UnionMember UnionMember { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        public ICollection<PaymentRecord>? PaymentRecord { get; set; }
    }
}
