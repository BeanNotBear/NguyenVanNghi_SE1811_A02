using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace NguyenVanNghiRazorPages.Pages.Profile
{
	[Authorize(Roles = "Staff")]
	public class ManageModel(ICategoryService categoryService, INewsArticleService newsArticleService, ITagService tagService) : PageModel
	{
		[BindProperty]
		public CreateNewsArticleDTO CreateNewsArticleDTO { get; set; }

		[BindProperty]
		public IEnumerable<NewsArticleDTO> NewsArticleDTOs { get; set; }

		[BindProperty]
		public EditNewsArticleDTO EditNewsArticleDTO { get; set; }

		public SelectList Tags { get; set; }

		public SelectList Categories { get; set; }

		public SelectList Status { get; set; }

		public async Task OnGetAsync(string? search = null,int? id = null, int? category = null)
		{
			var categories = await categoryService.GetAll();
			Categories = new SelectList(categories, "CategoryId", "CategoryName", category);
			var status = new List<NewsStatus>() { NewsStatus.Active, NewsStatus.Inactive };
			Status = new SelectList(status);
			NewsArticleDTOs = await newsArticleService.GetListByID(search, category, null, id);
			Tags = new SelectList(await tagService.GetSelectTagList(), "TagId", "TagName");
		}
	}
}
