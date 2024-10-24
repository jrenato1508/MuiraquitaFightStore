using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Mvc.Extension.AspNetUser;
using WebApp.Mvc.Extension.CustomAuthorization;
using WebApp.Mvc.Models.UserManagementPanelModels;

namespace WebApp.Mvc.Controllers.UserManagementPanelController
{
    [Authorize]
    [ClaimsAuthorize("SuperAdmin")]
    [Route("Perfil-Usuario")]
    public class RoleManagerController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly 

        public RoleManagerController(RoleManager<IdentityRole> roleManager,
                                     UserManager<ApplicationUser> userManager,
                                     IUser appUser) : base( appUser)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("Lista-de-Pefil")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [Route("Adicionar-novo-Perfil")]
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName == null) return BadRequest();

            string roleFormatada = roleName.Trim().Replace(" ", "");

            var roleExite = await _roleManager.FindByNameAsync(roleFormatada);

            if (roleExite != null) return BadRequest();

            await _roleManager.CreateAsync(new IdentityRole(roleFormatada));

            return RedirectToAction("Index");
        }


        [Route("excluir-perfil")]
        public async Task<IActionResult> Delete(Guid id)
        {

            IdentityRole roleBanco = await _roleManager.FindByIdAsync(id.ToString());
            if (roleBanco == null) return NotFound();
            return View(roleBanco);
        }

        [Route("excluir-perfil")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            IdentityRole roleBanco = await _roleManager.FindByIdAsync(id.ToString());

            if (roleBanco == null) return NotFound();

            ChecarSeRoleUso(roleBanco.Name);

            var result = await _roleManager.DeleteAsync(roleBanco);

            if (!result.Succeeded) return BadRequest();

            //TempData["Sucesso"] = "Role Excluido com Sucesso";
            return RedirectToAction("Index");
        }

        #region MELHORIA
        /*
           Conferir se não existe nenhum usuário que esteja vinculado a Role que será excluida, caso esteja, ou a gente cancela
           a exclusão ou setamos todos os usuário para uma outra role desfault

         */
        #endregion
        private async void ChecarSeRoleUso(string RoleName)
        {
            var UsersInRole = await _userManager.GetUsersInRoleAsync(RoleName);

            if (UsersInRole.Any())
            {
                foreach (var users in UsersInRole)
                {

                    await _userManager.RemoveFromRoleAsync(users, RoleName);
                    await _userManager.AddToRoleAsync(users, Roles.Basic.ToString());

                }
            }
        }
    }
}
