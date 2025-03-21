using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class CreateCategoryDTO
	{
		public int CategoryId { get; set; }

		public string CategoryName { get; set; } = null!;

		public string CategoryDescription { get; set; } = null!;

		public int? ParentCategoryId { get; set; }

		public bool IsActive { get; set; }
	}
}
