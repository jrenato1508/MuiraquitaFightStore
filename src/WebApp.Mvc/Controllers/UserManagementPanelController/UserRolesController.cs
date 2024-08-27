using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebApp.Mvc.Extension.AspNetUser;
using WebApp.Mvc.Extension.CustomAuthorization;
using WebApp.Mvc.Models.Nofificacao;
using WebApp.Mvc.Models.UserManagementPanelModels;
using WebApp.Mvc.Models.UserManagementPanelModels.Authentication;

namespace WebApp.Mvc.Controllers.UserManagementPanelController
{
    [Authorize]
    [ClaimsAuthorize("SuperAdmin")]
    [Route("AdministrarUsuario")]
    public class UserRolesController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly INotificador _notificador;
        private readonly INotyfService _notify;



        public UserRolesController(UserManager<ApplicationUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   INotificador notificador,
                                   IUser appUser,
                                   INotyfService notyf) : base(notificador, appUser)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _notificador = notificador;
            _notify = notyf;
        }

        [Route("Lista-de-Usuarios")]
        public async Task<IActionResult> Index()
        {

            var users = await _userManager.Users.Where(c => c.Id != UsuarioId.ToString()).ToListAsync();

            var userRolesViewModel = await CarregarUsuarios(users);

            //_notify.Success("Sucesso!");
            //_notify.Error("Erro!");
            //_notify.Warning("Warning!");
            //_notify.Information("Information!");
            ////_notify.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke");
            ////_notify.Custom("Custom Notification - closes in 5 seconds.", 10, "#135224");

            return View(userRolesViewModel);
        }



        [Route("novo-usuario")]
        public IActionResult NovoUsuario()
        {
            RegisterUserViewModel User = new RegisterUserViewModel();
            return View(User);
        }



        [Route("novo-usuario")]
        [HttpPost, ActionName("NovoUsuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterUserViewModel User)
        {
            if (!ModelState.IsValid) return View();
            ApplicationUser NovoUsuario = new ApplicationUser();

            NovoUsuario.FirstName = User.FirstName;
            NovoUsuario.LastName = User.LastName;
            NovoUsuario.Email = User.Email;
            NovoUsuario.UserName = User.FirstName;
            NovoUsuario.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(NovoUsuario, User.Password);

            if (!result.Succeeded)
            {
                _notify.Error("Errro ao criar novo usuáro");
                return View();
            }

            await _userManager.AddToRoleAsync(NovoUsuario, Roles.Basic.ToString());

            _notify.Success("Novo usuario criado com sucesso");
            return RedirectToAction("Index");
        }


        // ADICIONAR A OPÇÃO DE EDITAR EMAIL,Nome, Sobrenome e o que mais acharmos que seja valido.

        [Route("Alterar-Perfil-Usuario")]
        // Edita o Perfil do usuário
        public async Task<IActionResult> EditarUsuario(Guid userId)
        {
            var UsuarioBanco = await _userManager.FindByIdAsync(userId.ToString());
            if (UsuarioBanco == null)
            {
                _notify.Warning("Usuário Não Encontrado");
                return View();
            }

            var PerfisDoUsuario = new List<ManageUserRolesViewModel>();
            foreach (var perfil in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = perfil.Id,
                    RoleName = perfil.Name
                };
                if (await _userManager.IsInRoleAsync(UsuarioBanco, perfil.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                PerfisDoUsuario.Add(userRolesViewModel);
            }

            var Usuario = new EditeUserViewModel
            {
                UsuarioID = userId,
                FirstName = UsuarioBanco.FirstName,
                LastName = UsuarioBanco.LastName,
                Email = UsuarioBanco.Email,
                Perfils = PerfisDoUsuario
            };

            //ViewBag.userId = userId;
            //var user = await _userManager.FindByIdAsync(userId.ToString());
            //if (user == null)
            //{
            //    _notify.Warning("Usuário não encontrado");
            //    return RedirectToAction("Index");
            //}
            //ViewBag.UserName = user.UserName;
            //var model = new List<ManageUserRolesViewModel>();
            //foreach (var role in _roleManager.Roles)
            //{
            //    var userRolesViewModel = new ManageUserRolesViewModel
            //    {
            //        RoleId = role.Id,
            //        RoleName = role.Name
            //    };
            //    if (await _userManager.IsInRoleAsync(user, role.Name))
            //    {
            //        userRolesViewModel.Selected = true;
            //    }
            //    else
            //    {
            //        userRolesViewModel.Selected = false;
            //    }
            //    model.Add(userRolesViewModel);
            //}
            return View(Usuario);
        }




        [Route("Alterar-Perfil-Usuario")]
        [HttpPost, ActionName("EditarUsuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuarioConfirmed(EditeUserViewModel UsuarioFormulario, Guid UsuarioID)
        {
            if (!ModelState.IsValid) return View();
            var usuarioBanco = await _userManager.FindByIdAsync(UsuarioID.ToString());

            if (usuarioBanco == null)
            {
                _notify.Warning("Usuário Não encontrado");
                return View();
            }

            usuarioBanco.FirstName = UsuarioFormulario.FirstName;
            usuarioBanco.LastName = UsuarioFormulario.LastName;
            usuarioBanco.Email = UsuarioFormulario.Email;



            var roles = await _userManager.GetRolesAsync(usuarioBanco);

            var result = await _userManager.RemoveFromRolesAsync(usuarioBanco, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View();
            }

            result = await _userManager.AddToRolesAsync(usuarioBanco, UsuarioFormulario.Perfils.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View();
            }

            //_notify.Success($"Perfil do usuário {user.UserName} alterado!");
            return RedirectToAction("Index");
        }




        [Route("Excluir-Usuario")]
        public async Task<IActionResult> ExcluirUsuario(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _notify.Warning("Usuário não encontrado");
                return BadRequest();
            }

            return View(user);
        }


        [Route("Excluir-Usuario")]
        [HttpPost, ActionName("ExcluirUsuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusaoUsuario(Guid Id)
        {

            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null) return BadRequest();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                _notify.Error("Oppss...Erro ao exlcuir usuário");
                return BadRequest();
            }

            _notify.Success($"Usuário {user.UserName} Excluido com sucesso!");
            return RedirectToAction("index");
        }


        [Route("Alterar-Senha-Usuario")]
        public async Task<IActionResult> ChangePassword(Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null) return BadRequest();

            return View(new ChangePasswordUser { UsuarioID = UserId });

        }

        [Route("Alterar-Senha-Usuario")]
        [HttpPost, ActionName("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordConfirmed(Guid UserId, ChangePasswordUser userChanger)
        {

            if (UserId != userChanger.UsuarioID) return NotFound();
            if (!ModelState.IsValid) return BadRequest();

            var usuario = await _userManager.FindByIdAsync(UserId.ToString());

            if (usuario == null)
            {
                _notify.Warning("Usuário não encontrado");
                return NotFound();
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var result = await _userManager.ResetPasswordAsync(usuario, token, userChanger.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                _notify.Error("Ops... Algo deu errado");
                return View();
            }

            _notify.Success($"A Senha do usuário {usuario.FirstName} foi alterada com sucesso!");
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<UserRolesViewModel>> CarregarUsuarios(List<ApplicationUser> users)
        {
            var userRoles = new List<UserRolesViewModel>();

            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                thisViewModel.UserName = user.UserName;
                userRoles.Add(thisViewModel);
            }

            return userRoles;
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}
