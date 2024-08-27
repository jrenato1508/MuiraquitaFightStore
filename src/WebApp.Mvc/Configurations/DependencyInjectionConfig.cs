using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApp.Mvc.Extension.AspNetUser;
using WebApp.Mvc.Models.Nofificacao;

namespace WebApp.Mvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUser,AspNetUser>();


            return services;
        }
    }
}
