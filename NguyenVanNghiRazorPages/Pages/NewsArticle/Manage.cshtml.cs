using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.NewsArticle
{
	[Authorize(Roles = "Staff")]
	public class ManageModel(ICategoryService categoryService, INewsArticleService newsArticleService, ITagService tagService) : PageModel
	{
		[BindProperty]
		public CreateNewsArticleDTO CreateNewsArticleDTO { get; set; }

		[BindProperty]
		public IEnumerable<NewsArticleDTO> NewsArticleDTOs { get; set; }

		public SelectList Tags { get; set; }

		public SelectList Categories { get; set; }

		public SelectList Status { get; set; }

		public async Task OnGetAsync(string? search = null, int? categoryId = null)
		{
			var categories = await categoryService.GetAll();
			Categories = new SelectList(categories, "CategoryId", "CategoryName");
			var status = new List<NewsStatus>() { NewsStatus.Active, NewsStatus.Inactive };
			Status = new SelectList(status);
			NewsArticleDTOs = await newsArticleService.GetAll(search, categoryId);
			Tags = new SelectList(await tagService.GetSelectTagList(), "TagId", "TagName");
		}

		#region create news
		public async Task<IActionResult> OnPostCreate()
		{
			if (!ModelState.IsValid)
			{
				var categories = await categoryService.GetAll();
				Categories = new SelectList(categories, "CategoryId", "CategoryName");
				var status = new List<NewsStatus>() { NewsStatus.Active, NewsStatus.Inactive };
				Status = new SelectList(status);
				return Page();
			}
			await newsArticleService.Create(CreateNewsArticleDTO);
			return RedirectToPage("Manage");
		}
		#endregion
	}
}
