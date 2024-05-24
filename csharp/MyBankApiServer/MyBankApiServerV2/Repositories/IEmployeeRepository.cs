using BankApiServer.Models;

namespace MyBankApiServerV2.Repositories
{
    public interface IEmployeeRepository
    {
        int Add(Employee employee);
        int Delete(int id);
        int Update(Employee employee);
        Employee Get();
        List<Employee> GetAll();
    }
}
