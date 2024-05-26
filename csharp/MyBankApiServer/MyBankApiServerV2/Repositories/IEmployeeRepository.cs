using BankApiServer.Models;

namespace MyBankApiServerV2.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        int Add(Employee employee);
        int Delete(int id);
        int Update(Employee employee);
        Employee Get(int id);
        IEnumerable<Employee> GetAll();
    }
}
