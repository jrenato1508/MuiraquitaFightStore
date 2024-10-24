using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Extension.AspNetUser;
using WebApp.Mvc.Models.UserManagementPanelModels;

namespace WebApp.Mvc.Controllers.UserManagementPanelController
{
    // Excluir - não vamos mais usar. O mesmo vale pra Model UserClaimsViewModel e View da pasta UserClaim
    [Route("Claims")]
    public class UserClaimsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserClaimsController(UserManager<ApplicationUser> userManager,
                                    IUser appUser) : base( appUser)
        {
            _userManager = userManager;
        }

        // Artigo Macorati - https://www.macoratti.net/21/02/aspn_claims1.htm

        public ViewResult index() => View(User?.Claims);


        //public async Task<IActionResult> Index(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var claims = await _userManager.GetClaimsAsync(user);

        //    // Agrupa as claims por tipo
        //    var groupedClaims = claims
        //        .GroupBy(c => c.Type)
        //        .ToDictionary(g => g.Key, g => g.Select(c => c.Value).ToList());

        //    return View(groupedClaims);
        //}



        //public async Task<IActionResult> Index(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var claims = await _userManager.GetClaimsAsync(user);
        //    var model = claims.Select(c => new UserClaimsViewModel
        //    {
        //        UserId = userId,
        //        ClaimType = c.Type,
        //        ClaimValue = c.Value
        //    }).ToList();

        //    return View(model);
        //}
    }
}
