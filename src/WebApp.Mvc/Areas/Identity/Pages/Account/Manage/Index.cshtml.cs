// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Mvc.Models.UserManagementPanelModels;

namespace WebApp.Mvc.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        // TempData para alterar o limete de mudanças de usuário. By: Zé
        [TempData]
        public string UserNameChangeLimitMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //public class InputModel
        //{
        //    /// <summary>
        //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        //    ///     directly from your code. This API may change or be removed in future releases.
        //    /// </summary>
        //    [Phone]
        //    [Display(Name = "Phone number")]
        //    public string PhoneNumber { get; set; }
        //}

        // Nova class by:José
        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "Username")]
            public string Username { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var profilePicture = user.ProfilePicture;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Username = userName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = profilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            UserNameChangeLimitMessage = $"Você pode alterar seu nome de usuário {user.UsernameChangeLimit}  vezes.";
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userBanco = await _userManager.GetUserAsync(User);
            if (userBanco == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(userBanco);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(userBanco);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(userBanco, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var firstName = userBanco.FirstName;
            var lastName = userBanco.LastName;
            if (Input.FirstName != firstName)
            {
                userBanco.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(userBanco);
            }

            if (Input.LastName != lastName)
            {
                userBanco.LastName = Input.LastName;
                await _userManager.UpdateAsync(userBanco);
            }

            #region OBSERVAÇÃO
            /*
               Adicionando um limite de mudanças para alterar o nome de usuário
             */
            #endregion
            if (userBanco.UsernameChangeLimit > 0)
            {
                if (Input.Username != userBanco.UserName)
                {
                    var userNameExists = await _userManager.FindByNameAsync(Input.Username);
                    if (userNameExists != null)
                    {
                        StatusMessage = "Nome de usuário já utilizado. Selecione um nome de usuário diferente.";
                        return RedirectToPage();
                    }
                    var setUserName = await _userManager.SetUserNameAsync(userBanco, Input.Username);
                    if (!setUserName.Succeeded)
                    {
                        StatusMessage = "Erro inesperado ao tentar definir o nome de usuário.";
                        return RedirectToPage();
                    }
                    else
                    {
                        userBanco.UsernameChangeLimit -= 1;
                        await _userManager.UpdateAsync(userBanco);
                    }
                }
            }


            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    userBanco.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(userBanco);
            }

            await _signInManager.RefreshSignInAsync(userBanco);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
