using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class AccountPasswordDTO
	{
		public int AccountId { get; set; }
		public string AccountPassword { get; set; } = null!;
	}
}
