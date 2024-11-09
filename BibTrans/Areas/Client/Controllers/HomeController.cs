using Microsoft.AspNetCore.Mvc;

namespace BibTrans.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        [Area("Client")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Borrowings");
        }
    }
}
