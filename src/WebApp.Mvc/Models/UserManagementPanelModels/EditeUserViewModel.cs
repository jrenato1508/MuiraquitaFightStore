using System.ComponentModel.DataAnnotations;

namespace WebApp.Mvc.Models.UserManagementPanelModels
{
    public class EditeUserViewModel
    {
        public Guid UsuarioID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }


        public List<ManageUserRolesViewModel> Perfils { get; set; }
    }
}
