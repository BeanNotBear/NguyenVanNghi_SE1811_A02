using Shared.Enums;

namespace BLL.DTOs
{
	public class SystemAccountDTO
	{
		public required int ID { get; set; }
		public required string AccountName { get; set; }
		public required string AccountEmail { get; set; }
		public required AccountRole AccountRole { get; set; }
	}
}
