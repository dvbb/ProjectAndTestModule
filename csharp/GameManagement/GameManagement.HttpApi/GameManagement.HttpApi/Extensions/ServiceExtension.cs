using GameManagement.Contract.IRepository;
using GameManagement.EntityFramework;
using GameManagement.EntityFramework.Repository;
using GameManagement.HttpApi.Redis;
using Microsoft.EntityFrameworkCore;

namespace GameManagement.Extensions
{
    public static class ServiceExtension
    {
        /// <summary>
        /// config cross-domain
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("AnyPolicy",
                    builder => builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });
        }

        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            // Get connection string
            var connStr = config.GetConnectionString("GameDb");
            connStr = connStr.Replace("FOO", Environment.GetEnvironmentVariable("PWD"));

            // Use Sql server 
            services.AddDbContext<GameManagementDbContext>(builder => builder.UseSqlServer(connStr));
        }

        public static void ConfigureWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureRedis(this IServiceCollection services, IConfiguration config)
        {
            var section = config.GetSection("Redis:Default");
            //services.AddSingleton(new RedisHelper(section.Get<RedisOption>()));
        }
    }
}
