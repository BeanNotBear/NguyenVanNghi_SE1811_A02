using System.Data;
using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.Profile
{
    public class IndexModel(IAccountService accountService) : PageModel
    {
        [BindProperty]
        public SystemAccountDetailDTO SystemAccountDetailDTO { get; set; }

        [BindProperty]
        public AccountPasswordDTO AccountPasswordDTO { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            SystemAccountDetailDTO = await accountService.GetByID(id);
            return Page();
        }

		public async Task<IActionResult> OnPostEdit()
		{
            EditAccountDTO editAccountDTO = new EditAccountDTO()
            {
                AccountID = SystemAccountDetailDTO.AccountID,
                AccountName = SystemAccountDetailDTO.AccountName,
                AccountEmail = SystemAccountDetailDTO.AccountEmail,
                AccountRole = SystemAccountDetailDTO.AccountRole
			};

			await accountService.Update(editAccountDTO);
            return Page();
		}

		#region change password
		public async Task<JsonResult> OnGetPassword(int id)
		{
			var account = await accountService.GetByID(id);
			return new JsonResult(account);
		}

        public async Task<IActionResult> OnPostPassword()
        {
            await accountService.UpdatePassword(AccountPasswordDTO);
            OnGet(AccountPasswordDTO.AccountId);
            return Page();
        }
		#endregion

	}
}
