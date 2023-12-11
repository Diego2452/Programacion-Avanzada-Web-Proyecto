using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            string userDataJson = HttpContext.Session.GetString("UserData");
            string accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Index");
            }

            if (!string.IsNullOrEmpty(userDataJson))
            {
                UserData userData = JsonConvert.DeserializeObject<UserData>(userDataJson);
                SharedUserData.UserName = userData.Nombre;
                SharedUserData.UserIdentificacion = userData.Identificacion;
                SharedUserData.UserData = userData;
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            // HttpContext.Session.Remove("UserData");
            // HttpContext.Session.Remove("AccessToken");
            // HttpContext.Session.Remove("UserId");
            // HttpContext.Session.Remove("UserName");

            return RedirectToPage("/Index");
        }
    }
}
