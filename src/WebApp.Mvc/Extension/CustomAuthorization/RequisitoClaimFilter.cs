using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Mvc.Extension.CustomAuthorization
{
    // Configuração do filtro
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly string _role;


        public RequisitoClaimFilter(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Se não tiver autenticado redirecionaremos para a pagina responsavél por fazer a autenticação
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }

            // Caso já esteja autenticado, mas não possui a claim necessaria aqui verificaremos que ele possui a claim que esperamos, caso não enviaremos o CodeStatus 403(acesso negado)
            if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _role))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
