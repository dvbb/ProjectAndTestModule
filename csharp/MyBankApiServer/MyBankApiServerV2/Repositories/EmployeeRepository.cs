using BankApiServer.Models;
using Microsoft.EntityFrameworkCore;
using MyBankApiServerV2.Models;

namespace MyBankApiServerV2.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public int Add(Employee employee)
        {
            _context.Employees.Add(employee);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            Employee? employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            // check
            employee = _context.Employees.Find(id);
            if (employee != null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public Employee Get(int id)
        {
            return _context.Employees.Find(id);

        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public int Update(Employee employee)
        {
            var updated = _context.Employees.Attach(employee);
            updated.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
