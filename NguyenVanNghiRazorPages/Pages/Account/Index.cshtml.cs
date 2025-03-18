using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.Account
{
	[Authorize(Roles = "Admin")]
	public class IndexModel(IAccountService accountService) : PageModel
	{
		[BindProperty]
		public IEnumerable<SystemAccountDTO>? SystemAccounts { get; set; }

		[BindProperty]
		public CreateAccountDTO AccountDTO { get; set; }

		public SelectList? Roles { get; set; }

		public async Task OnGet(string? search = null, AccountRole? accountRole = null)
		{
			SystemAccounts = await accountService.GetAll(search, accountRole);
			var roles = new List<AccountRole>() { AccountRole.Admin, AccountRole.Staff, AccountRole.Lecturer };
			Roles = new SelectList(roles, accountRole);
		}

		public IActionResult OnPostCreate()
		{
			if (!ModelState.IsValid)
			{
				var roles = new List<AccountRole> { AccountRole.Admin, AccountRole.Staff, AccountRole.Lecturer };
				Roles = new SelectList(roles);
				return Page();
			}

			accountService.Create(AccountDTO);
			return RedirectToPage("/Account/Index");
		}
	}
}
