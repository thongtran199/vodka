using EmployeeEntities;

namespace EmployeeServices
{
    public interface IEmployeeService
    {
        Task CreateAsSync(Employee newEmployee);
        Task UpdateById(int id);
        Task UpdateAsSync(Employee newEmployee);
        Task DeleteById(int id);
        Employee GetById(int id);
        IEnumerable<Employee> GetAll();

        //IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
    }
}
