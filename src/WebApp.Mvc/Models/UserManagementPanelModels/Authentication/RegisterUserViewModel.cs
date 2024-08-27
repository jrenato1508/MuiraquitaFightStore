using System.ComponentModel.DataAnnotations;

namespace WebApp.Mvc.Models.UserManagementPanelModels.Authentication
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$", ErrorMessage = "O campo deve conter pelo menos uma letra maiúscula e pelo menos um caractere não alfanumérico.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme Nova Senha")]
        [Compare("Password", ErrorMessage = "A nova senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
