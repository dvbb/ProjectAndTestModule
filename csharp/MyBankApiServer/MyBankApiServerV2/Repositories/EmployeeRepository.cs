using BankApiServer.Models;
using Microsoft.EntityFrameworkCore;
using MyBankApiServerV2.Models;

namespace MyBankApiServerV2.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
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
            return this.FindByCondition(employee => employee.EId == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetAll()
        {
            return this.FindAll().OrderBy(p => p.EId).ToList();
        }

        public int Update(Employee employee)
        {
            var updated = _context.Employees.Attach(employee);
            updated.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
