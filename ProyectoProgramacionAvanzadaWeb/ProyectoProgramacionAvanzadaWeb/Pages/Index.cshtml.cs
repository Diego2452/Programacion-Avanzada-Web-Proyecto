using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProyectoProgramacionAvanzadaWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public bool EsUsuarioAutenticado { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");
            ViewData["EsUsuarioAutenticado"] = !string.IsNullOrEmpty(accessToken);
        }

    }
}