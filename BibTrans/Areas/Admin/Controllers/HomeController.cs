using Microsoft.AspNetCore.Mvc;

namespace BibTrans.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Books");
        }
    }
}
