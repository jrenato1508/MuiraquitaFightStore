﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Models.Nofificacao;

namespace WebApp.Mvc.Extension
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;
        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());

            notificacoes.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x.Mensagem));

            return View();
        }
    }
}
