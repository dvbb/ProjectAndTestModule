using Microsoft.Identity.Client;

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
    }
}
