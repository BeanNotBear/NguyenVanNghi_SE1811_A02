using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Shared.Enums;

namespace BLL.DTOs
{
	public class CategoryDTO
	{
		public int CategoryId { get; set; }

		public string CategoryName { get; set; } = null!;

		public string CategoryDescription { get; set; } = null!;

		public int? ParentCategoryId { get; set; }

		public bool IsActive { get; set; }

		public CategoryStatus CategoryStatus { get; set; }

		public virtual Category? ParentCategory { get; set; }
	}
}
