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

		[BindProperty]
		public EditAccountDTO EditAccountDTO { get; set; }


		public SelectList? Roles { get; set; }

		public async Task OnGet(string? search = null, AccountRole? accountRole = null)
		{
			SystemAccounts = await accountService.GetAll(search, accountRole);
			var roles = new List<AccountRole>() { AccountRole.Admin, AccountRole.Staff, AccountRole.Lecturer };
			Roles = new SelectList(roles, accountRole);
		}

		#region create account
		public IActionResult OnPostCreate()
		{
			if (!ModelState.IsValid)
			{
				var roles = new List<AccountRole> { AccountRole.Admin, AccountRole.Staff, AccountRole.Lecturer };
				Roles = new SelectList(roles);
			}

			accountService.Create(AccountDTO);
			return RedirectToPage("/Account/Index");
		}
		#endregion

		#region edit account
		public async Task<JsonResult> OnGetEdit(int id)
		{
			var account = await accountService.GetByID(id);
			return new JsonResult(account);
		}

		public async Task<IActionResult> OnPostEdit()
		{
			ModelState.Remove("AccountName");
			ModelState.Remove("AccountEmail");
			ModelState.Remove("AccountPassword");
			ModelState.Remove("ConfirmPassword");
			if (!ModelState.IsValid)
			{
				var roles = new List<AccountRole> { AccountRole.Admin, AccountRole.Staff, AccountRole.Lecturer };
				Roles = new SelectList(roles);
				return Page();
			}
			await accountService.Update(EditAccountDTO);
			return RedirectToPage("/Account/Index");
		}
		#endregion

		#region delete account
		public async Task<IActionResult> OnPostDelete(int id)
		{
			await accountService.Delete(id);
			return RedirectToPage("/Account/Index");
		}
		#endregion
	}
}
