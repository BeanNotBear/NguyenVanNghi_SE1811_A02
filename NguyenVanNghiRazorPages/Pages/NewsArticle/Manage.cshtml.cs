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

		[BindProperty]
		public EditNewsArticleDTO EditNewsArticleDTO { get; set; }

		public SelectList Tags { get; set; }

		public SelectList Categories { get; set; }

		public SelectList Status { get; set; }

		public async Task OnGetAsync(string? search = null, int? category = null)
		{
			var categories = await categoryService.GetAll();
			Categories = new SelectList(categories, "CategoryId", "CategoryName", category);
			var status = new List<NewsStatus>() { NewsStatus.Active, NewsStatus.Inactive };
			Status = new SelectList(status);
			NewsArticleDTOs = await newsArticleService.GetAll(search, category);
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

		#region edit news
		public async Task<IActionResult> OnGetEdit(int id)
		{
			EditNewsArticleDTO = await newsArticleService.GetById(id);
			return new JsonResult(EditNewsArticleDTO);
		}

		public async Task<IActionResult> OnPostEdit()
		{
			ModelState.Remove("Tags");
			ModelState.Remove("Headline");
			ModelState.Remove("NewsTitle");
			ModelState.Remove("NewsSource");
			ModelState.Remove("NewsContent");
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await newsArticleService.Update(EditNewsArticleDTO);
			return RedirectToPage("Manage");
		}
		#endregion
	}
}
