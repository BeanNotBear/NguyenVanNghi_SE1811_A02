using DAL.Entities;
using Shared.Enums;

namespace BLL.DTOs
{
	public class NewsArticleDetailsDTO
	{
		public int NewsArticleId { get; set; }

		public string NewsTitle { get; set; } = null!;

		public string Headline { get; set; } = null!;

		public DateTime CreatedDate { get; set; }

		public string NewsContent { get; set; } = null!;

		public string NewsSource { get; set; } = null!;

		public NewsStatus NewsStatus { get; set; }

		public int? UpdatedById { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string Category { get; set; } = null!;

		public string CreatedBy { get; set; } = null!;

		public string? UpdatedBy { get; set; }

		public virtual IEnumerable<string> Tags { get; set; } = new List<string>();
	}
}
