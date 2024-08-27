using Microsoft.AspNetCore.Mvc;

namespace WebApp.Mvc.Extension.CustomAuthorization
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string RoleValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new string(RoleValue) };


        }
    }
}
