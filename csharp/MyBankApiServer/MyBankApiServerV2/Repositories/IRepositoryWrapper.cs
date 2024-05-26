namespace MyBankApiServerV2.Repositories
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }
        Task<int> Save();
    }
}
