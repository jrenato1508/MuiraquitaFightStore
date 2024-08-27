using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Mvc.Data;
using WebApp.Mvc.Models.UserManagementPanelModels;

namespace WebApp.Mvc.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            #region OBSERVAÇÃO - IdentityUser/ApplicationUser
            /*
              - Mudamos de IdentityUser para ApplicationUser que foi a class que criamos com novos atributos que herda de indetityUser.

              - Próximo passo é alterar todas as outras paginas que adicionamos via scaffolding, devemos remover o IdentityUser e adicionar o ApplicationUser

              - Alterar também em nosso ApplicationDbContext(IMPORTANTE)

              - ConfirmEmailModel, ConfirmEmailChangeModel ....
             
              - IMPORTANTE:  Deve ser alterado também na _LoginPartial.cshtml

              HISTORICO

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
             */
            #endregion
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>() // Adicionei para funcionar as roles
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            


            return services;
        }
    }
}
