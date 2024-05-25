using BankApiServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBankApiServerV2.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(employee => employee.EId);
            builder.Property(employee => employee.EName);
            builder.Property(employee => employee.Pwd);

            builder.HasKey(employee => employee.EName);

        }
    }
}
