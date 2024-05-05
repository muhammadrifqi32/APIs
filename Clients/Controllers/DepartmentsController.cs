using Microsoft.AspNetCore.Mvc;

namespace Clients.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
