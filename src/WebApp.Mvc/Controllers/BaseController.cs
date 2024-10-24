using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Extension.AspNetUser;

namespace WebApp.Mvc.Controllers
{
    public class BaseController : Controller
    {
        
        private readonly IUser _appUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        public BaseController(IUser appUser)
        {
            
            _appUser = appUser;

            if (_appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        
        
    }
}
