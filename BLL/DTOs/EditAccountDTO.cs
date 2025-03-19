using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class EditAccountDTO
	{
		public int AccountID { get; set; }

		[Required(ErrorMessage = "Account name is required.")]
		[StringLength(100, ErrorMessage = "Account name must be between 3 and 100 characters.", MinimumLength = 3)]
		public string AccountName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string AccountEmail { get; set; }

		[Required(ErrorMessage = "Role is required.")]
		public AccountRole AccountRole { get; set; }

	}
}
