using Data.DbInitializer;

namespace Visitors.Utils
{
    public static class ComponentRegistry
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("Repository");

            //services.AddDbContext<Repository>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddHttpContextAccessor();
            services.AddSingleton<ILocalTimeHelper, LocalTimeHelper>();

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUserContext, UserContext>();

        }
    }
}
