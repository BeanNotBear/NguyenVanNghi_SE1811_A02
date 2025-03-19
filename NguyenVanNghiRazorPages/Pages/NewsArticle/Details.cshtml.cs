using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NguyenVanNghiRazorPages.Pages.NewsArticle
{
    public class DetailsModel(INewsArticleService newsArticleService) : PageModel
    {
        [BindProperty]
		public NewsArticleDetailsDTO NewsArticleDetailsDTO { get; set; }
		public async Task OnGet(int id)
        {
            NewsArticleDetailsDTO = await newsArticleService.GetDetails(id);
        }
    }
}
