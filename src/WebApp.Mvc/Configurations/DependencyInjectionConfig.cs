using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MuiraquitaFightStore.Catalogo.Application.Service;
using MuiraquitaFightStore.Catalogo.Data;
using MuiraquitaFightStore.Catalogo.Data.Repository;
using MuiraquitaFightStore.Catalogo.Domain.Events;
using MuiraquitaFightStore.Catalogo.Domain.Interfaces;
using MuiraquitaFightStore.Catalogo.Domain.Services;
using MuiraquitaFightStore.Core.Communication.Mediator;
using WebApp.Mvc.Extension.AspNetUser;


namespace WebApp.Mvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            
            
            services.AddScoped<IUser,AspNetUser>();
            services.AddScoped<IMediatorHandler, MediatrHandler>();


            #region CONTEXT DE CATALAGO
            services.AddScoped<IProdutoRepository, ProdutoRepositoy>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();


            #endregion

            return services;
        }
    }
}
