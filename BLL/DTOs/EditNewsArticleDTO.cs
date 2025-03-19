using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class EditNewsArticleDTO
	{
		[Required]
		public int NewsArticleId { get; set; }

		[Required]
		public string NewsTitle { get; set; } = null!;

		[Required]
		public string Headline { get; set; } = null!;

		[Required]
		public string NewsContent { get; set; } = null!;

		[Required]
		public string NewsSource { get; set; } = null!;

		[Required]
		public int CategoryId { get; set; }

		[Required]
		public NewsStatus NewsStatus { get; set; }

		[Required]
		public int UpdatedById { get; set; }

		[Required]
		public DateTime ModifiedDate { get; set; } = DateTime.Now;

		[Required]
		public virtual ICollection<int> Tags { get; set; } = new List<int>();
	}
}
