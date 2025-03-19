using Shared.Enums;

namespace BLL.DTOs
{
	public class SystemAccountDetailDTO
	{
		public required int AccountID { get; set; }
		public required string AccountName { get; set; }
		public required string AccountEmail { get; set; }
		public required AccountRole AccountRole { get; set; }
	}
}
