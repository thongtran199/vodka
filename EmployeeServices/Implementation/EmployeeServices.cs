using EmployeeEntities;
using EmployeeDataAccess;

namespace EmployeeServices.Implementation
{
    public class EmployeeServices : IEmployeeService
    {
        private ApplicationDbContext _context;
        public EmployeeServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Employee newEmployee)
        {
            Console.WriteLine("Chuan bi luu !");
            await _context.Employee.AddAsync(newEmployee);
            //await _context.SaveChangesAsync();
            //_context.Employee.Update(newEmployee);
            Console.WriteLine("Da them vao DbSet !");
            await _context.SaveChangesAsync();
            Console.WriteLine("Da them vao Database !");
        }

        public async Task DeleteById(int id)
        {
            var employee = GetById(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<Employee> GetAll()
        {
            return _context.Employee.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employee.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Employee newEmployee)
        {
            _context.Employee.Update(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
