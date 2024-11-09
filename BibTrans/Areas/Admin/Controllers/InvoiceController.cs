using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BibTrans.Areas.Admin.Services;
using System.IO;

namespace BibTrans.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            var document = _invoiceService.GetInvoice();

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                byte[] bytes = stream.ToArray();

                return File(bytes, "application/pdf", "RaportBiblioteki.pdf");
            }
        }
    }
}
