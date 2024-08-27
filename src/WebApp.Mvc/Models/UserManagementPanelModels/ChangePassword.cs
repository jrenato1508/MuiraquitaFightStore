using System.ComponentModel.DataAnnotations;

namespace WebApp.Mvc.Models.UserManagementPanelModels
{
    public class ChangePasswordUser
    {
        [Key]
        public Guid UsuarioID { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Senha Atual")]
        //public string OldPassword { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$", ErrorMessage = "O campo deve conter pelo menos uma letra maiúscula e pelo menos um caractere não alfanumérico.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme Nova Senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
