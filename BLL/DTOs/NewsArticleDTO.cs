using Shared.Enums;

namespace BLL.DTOs
{
	public class NewsArticleDTO
	{
		public int NewsArticleId { get; set; }

		public string NewsTitle { get; set; } = null!;

		public string Headline { get; set; } = null!;

		public DateTime CreatedDate { get; set; }

		public string NewsContent { get; set; } = null!;

		public string NewsSource { get; set; } = null!;

		public string Category { get; set; } = null!;

		public NewsStatus NewsStatus { get; set; }

		public string CreatedBy { get; set; } = null!;
	}
}
