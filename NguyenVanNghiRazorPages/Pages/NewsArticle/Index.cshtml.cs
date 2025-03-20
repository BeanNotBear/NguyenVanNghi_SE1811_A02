using BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NguyenVanNghiRazorPages.Pages.NewsArticle
{
    [Authorize(Roles = "Lecturer")]
    public class IndexModel : PageModel
    {
		[BindProperty]
		public IEnumerable<NewsArticleDTO> NewsArticleDTOs { get; set; }
		public void OnGet()
        {
        }
    }
}
