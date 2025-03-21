using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Entities;
using BLL.DTOs;
using BLL.Services.Implements;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;
using BLL.Services.Interfaces;

namespace NguyenVanNghiRazorPages.Pages.Report
{
    public class IndexModel(ICategoryService categoryService, INewsArticleService newsArticleService, ITagService tagService) : PageModel
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

		public async Task OnGetAsync(string? search = null, int? category = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			var categories = await categoryService.GetAll();
			Categories = new SelectList(categories, "CategoryId", "CategoryName", category);
			var status = new List<NewsStatus>() { NewsStatus.Active, NewsStatus.Inactive };
			Status = new SelectList(status);
			NewsArticleDTOs = await newsArticleService.GetList(search, category, startDate, endDate);
			Tags = new SelectList(await tagService.GetSelectTagList(), "TagId", "TagName");
		}

    }
}
