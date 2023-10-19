using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoProgramaciónAvanzadaWeb.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardAdminController
        public ActionResult Index()
        {
            return View();
        }
    }
}
