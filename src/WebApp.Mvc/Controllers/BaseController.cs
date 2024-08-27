using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Extension.AspNetUser;
using WebApp.Mvc.Models.Nofificacao;

namespace WebApp.Mvc.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificador _notificador;
        private readonly IUser _appUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        public BaseController(INotificador notificador,
                              IUser appUser)
        {
            _notificador = notificador;
            _appUser = appUser;

            if (_appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        
    }
}
