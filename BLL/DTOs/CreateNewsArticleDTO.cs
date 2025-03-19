using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class CreateNewsArticleDTO
	{
		[Required]
		[StringLength(50, ErrorMessage = "News title cannot exceed 50 characters.")]
		public string NewsTitle { get; set; } = null!;

		[Required]
		[StringLength(50, ErrorMessage = "Headline cannot exceed 50 characters.")]
		public string Headline { get; set; } = null!;

		[Required]
		public DateTime CreatedDate { get; set; } = DateTime.Now;

		[Required]
		[StringLength(50, ErrorMessage = "News source cannot exceed 255 characters.")]
		public string NewsSource { get; set; } = null!;

		[Required]
		public int CategoryId { get; set; }

		[Required]
		public NewsStatus NewsStatus { get; set; }

		[Required]
		public int CreatedById { get; set; }

		[Required]
		public string NewsContent { get; set; } = null!;

		public IEnumerable<int> Tags { get; set; }
	}
}
