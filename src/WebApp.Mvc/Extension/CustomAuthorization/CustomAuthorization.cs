using System.Security.Claims;

namespace WebApp.Mvc.Extension.CustomAuthorization
{
    public class CustomAuthorization
    {
        
        public static bool ValidarClaimsUsuario(HttpContext context, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Value.Contains(claimValue));
        }

    }
}
