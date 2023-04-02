using EmployeeEntities;

namespace EmployeeDCT1204.Models
{
    public class EmployeeDetailViewModel
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DateJoined { get; set; }
        public string Designation { get; set; }
        public string City { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
