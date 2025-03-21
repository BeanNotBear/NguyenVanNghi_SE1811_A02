using System.Data;
using BLL.DTOs;
using BLL.Services.Implements;
using BLL.Services.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.Category
{
    public class IndexModel(ICategoryService categoryService) : PageModel
    {
        [BindProperty]
        public IEnumerable<CategoryDTO>? Categories { get; set; }

        [BindProperty]
        public CategoryDTO CategoryDTO { get; set; }

        public SelectList? Status { get; set; }
        public SelectList SelectCategory { get; set; }


        public async Task OnGet(string? search = null, CategoryStatus? categoryStatus = null)
        {
            Categories = await categoryService.GetAllCategory(search, categoryStatus, new string[] { "ParentCategory" });
            var status = new List<CategoryStatus> { CategoryStatus.Active, CategoryStatus.Inactive};
            Status = new SelectList(status, categoryStatus);
			var categories = await categoryService.GetAll();
			SelectCategory = new SelectList(categories, "CategoryId", "CategoryName");
		}

		#region create category
        public async Task<IActionResult> OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
				var statuss = new List<CategoryStatus> { CategoryStatus.Active, CategoryStatus.Inactive };
				Status = new SelectList(statuss);
				var categories = await categoryService.GetAll();
				SelectCategory = new SelectList(categories, "CategoryId", "CategoryName");
			}

            await categoryService.Create(CategoryDTO);
            return RedirectToPage("/Category/Index");
        }
		#endregion

		#region update category
		public async Task<JsonResult> OnGetEdit(int id)
		{
			var status = new List<CategoryStatus> { CategoryStatus.Active, CategoryStatus.Inactive };
			Status = new SelectList(status);
			var category = await categoryService.GetByID(id);
			return new JsonResult(new
			{
				success = true,
				categoryId = category.CategoryId,
				categoryName = category.CategoryName, // Make sure this matches the database field
				categoryDescription = category.CategoryDescription,
				parentCategoryId = category.ParentCategoryId,
				isActive = category.IsActive
			});
		}

		public async Task<IActionResult> OnPostEdit()
		{
			ModelState.Remove("CategoryName");
			ModelState.Remove("IsActive");
			ModelState.Remove("CategoryDTO.IsActive");
			if (!ModelState.IsValid)
			{
				var status = new List<CategoryStatus> { CategoryStatus.Active, CategoryStatus.Inactive };
				Status = new SelectList(status);
				var categories = await categoryService.GetAll();
				SelectCategory = new SelectList(categories, "CategoryId", "CategoryName");
				return Page();
			}
			await categoryService.Update(CategoryDTO);
			return RedirectToPage("/Category/Index");
		}
		#endregion

		#region delete category
		public async Task<IActionResult> OnPostDelete(int id)
		{
			await categoryService.Delete(id);
			return RedirectToPage("/Category/Index");
		}
		#endregion
	}
}
