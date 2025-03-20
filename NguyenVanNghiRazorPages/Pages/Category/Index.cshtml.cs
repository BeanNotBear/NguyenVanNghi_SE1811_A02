using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.Category
{
    public class IndexModel(ICategoryService categoryService) : PageModel
    {
        [BindProperty]
        public IEnumerable<CategoryDTO>? Categories { get; set; }

        public SelectList? Status { get; set; }


        public async Task OnGet(string? search = null, CategoryStatus? categoryStatus = null)
        {
            Categories = await categoryService.GetAllCategory(search, categoryStatus, new string[] { "ParentCategory" });
            var statuss = new List<CategoryStatus> { CategoryStatus.Active, CategoryStatus.Inactive};
            Status = new SelectList(statuss, categoryStatus);
        }
    }
}
