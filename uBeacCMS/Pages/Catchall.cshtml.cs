using Microsoft.AspNetCore.Mvc.RazorPages;
using uBeacCMS.Models;

namespace uBeacCMS.Pages;

public class CatchallModel : PageModel
{
    public CmsContext Cms { get; set; }

    public CatchallModel(CmsContext cms)
    {
        Cms = cms;
    }
    public void OnGet()
    {
        ViewData["Title"] = Cms.Page?.Title;
    }
}
