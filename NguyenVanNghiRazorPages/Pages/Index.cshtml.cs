using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages
{
	[Authorize(Roles = "Lecturer")]
	[AllowAnonymous]
	public class IndexModel(INewsArticleService newsArticleService, ICategoryService categoryService) : PageModel
	{
		[BindProperty]
		public IEnumerable<NewsArticleDTO> NewsArticleDTOs { get; set; }

		[BindProperty]
		public SelectList Categories { get; set; }
		public async Task OnGet(string? search = null, int? category = null)
		{
			NewsArticleDTOs = await newsArticleService.GetAll(search, category, NewsStatus.Active);
			Categories = new SelectList(await categoryService.GetAll(), "CategoryId", "CategoryName", category);
		}
	}
}
