using CATECEV.CORE.Framework;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CATECEV.Data.Configure
{
    public static class AppManager
    {
        public static void ConfigureContext(this IServiceCollection services)
        {
            var connectionString = Utility.GetAppsettingsValue("ConnectionStrings", "DefaultConnection");
            services.AddDbContext<AppDBContext>(options =>
                    options.UseSqlServer(connectionString));
        }

        public static void ConfigureApp(IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
            context.Database.Migrate();
        }
    }
}
