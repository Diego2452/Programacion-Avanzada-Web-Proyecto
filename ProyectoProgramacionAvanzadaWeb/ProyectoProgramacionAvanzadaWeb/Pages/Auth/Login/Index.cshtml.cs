using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Auth.Login
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        public string Errors { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new LoginRequest();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "auth/login";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(LoginRequest);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeAnonymousType(responseData, new
                        {
                            tokenData = new
                            {
                                access_token = "",
                                token_type = "",
                                expires_in = 0,
                                exp = 0,
                                refresh_token = ""
                            },
                            userData = new
                            {
                                idUsuario = 0,
                                idTipoIdentificacion = 0,
                                idRol = 0,
                                identificacion = "",
                                nombre = "",
                                apellido_Materno = "",
                                apellido_Paterno = "",
                                email = "",
                                contrasenna = "",
                                idGenero = 0,
                                telefono = "",
                                direccion = "",
                                genero = "",
                                tipoIdentificacion = "",
                                rol = ""
                            }
                        });

                        string userDataJson = JsonConvert.SerializeObject(result.userData);

                        HttpContext.Session.SetString("UserData", userDataJson);
                        HttpContext.Session.SetString("AccessToken", result.tokenData.access_token);

                        TempData["SuccessMessage"] = "Operación exitosa: El usuario ha iniciado sesión correctamente.";
                        return RedirectToPage("/Admin/Index");
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        if (responseContent.Contains("El usuario no existe en la base de datos"))
                        {
                            Message = "El usuario no existe en la base de datos";
                        }
                        else if (responseContent.Contains("Credenciales incorrectas"))
                        {
                            Message = "Credenciales incorrectas";
                        }
                        else
                        {
                            Message = "Error al iniciar sesión. Código de estado: " + (int)response.StatusCode;
                        }
                    }
                    else
                    {
                        Message = "Error al iniciar sesión. Código de estado: " + (int)response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error interno del servidor: " + ex.Message;
                }
            }

            return Page();
        }
    }
}
