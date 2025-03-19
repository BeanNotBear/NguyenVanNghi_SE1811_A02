using BLL.Services.Implements;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenVanNghiRazorPages.ViewModels;
using System.Security.Claims;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.Auth
{
	public class IndexModel(IAuthService authService) : PageModel
	{
		[BindProperty]
		public LoginViewModel LoginViewModel { get; set; }
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{

			if (!ModelState.IsValid)
			{
				return Page();
			}
			var user = await authService.Login(LoginViewModel.Email, LoginViewModel.Password);
			if (user == null)
			{
				TempData["ErrorMessage"] = "Invalid email or password, please try again!";
				return Page();
			}
			var claims = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()),
						new Claim(ClaimTypes.Role, ((AccountRole)user.AccountRole).ToString())
					};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = LoginViewModel.IsRemember, // "Remember Me" support
			};
			if (LoginViewModel.IsRemember)
			{
				authProperties.ExpiresUtc = DateTime.UtcNow.AddDays(30);
			}
			await HttpContext.SignInAsync(claimsPrincipal, authProperties);
			return RedirectToPage("/Index");
		}

	}
}
