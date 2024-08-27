using System.Security.Claims;
using WebApp.Mvc.Models;

namespace WebApp.Mvc.Extension.AspNetUser
{
    public interface IUser
    {
        string Name { get; } // Nome do usuário
        Guid GetUserId(); // Obter o ID usuário
        string GetUserEmail(); //Obter o Email do usuário
        bool IsAuthenticated(); //Verificar se ele está autenticado
        bool IsInRole(string role); //Verificar se ele está em alguma role 

        
    }
}
