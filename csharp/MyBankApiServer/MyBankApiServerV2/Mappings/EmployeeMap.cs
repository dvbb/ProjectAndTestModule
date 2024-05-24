using BankApiServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBankApiServerV2.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(employee => employee.EId).HasMaxLength(50);
            builder.Property(employee => employee.EName).HasMaxLength(50);
            builder.Property(employee => employee.Pwd).HasMaxLength(50);

            builder.HasKey(employee => employee.EName);

        }
    }
}
