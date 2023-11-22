using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class CarrosApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public CarrosApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<Carros> Carros, string Message)> ObtenerCarrosAsync()
        {
            string apiEndpoint = "Carros";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<Carros> carros = JsonConvert.DeserializeObject<List<Carros>>(jsonContent);

                        return (carros, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron carros en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener carros desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearCarroAsync(Carros carro)
        {
            string apiEndpoint = "Carros";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(carro);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El carro ha sido creado.");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                        StringBuilder errorMessageBuilder = new StringBuilder();
                        foreach (var error in errorResponse.Errors)
                        {
                            errorMessageBuilder.AppendLine($"{error.Key}: {error.Value.Errors[0].ErrorMessage}");
                        }

                        return (false, errorMessageBuilder.ToString());
                    }
                    else
                    {
                        return (false, $"Error al crear el carro. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarCarroAsync(Carros carro)
        {
            string apiEndpoint = $"Carros/{carro.IdCarro}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(carro);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El carro ha sido editado.");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                        StringBuilder errorMessageBuilder = new StringBuilder();
                        foreach (var error in errorResponse.Errors)
                        {
                            errorMessageBuilder.AppendLine($"{error.Key}: {error.Value.Errors[0].ErrorMessage}");
                        }

                        return (false, errorMessageBuilder.ToString());
                    }
                    else
                    {
                        return (false, $"Error al editar el carro. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al editar el carro: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarCarroAsync(int id)
        {
            string apiEndpoint = $"Carros/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Carro eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el carro. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el carro: {ex.Message}");
                }
            }
        }

        public async Task<(Carros Carro, string Message)> ObtenerDetallesCarroAsync(int id)
        {
            string apiEndpoint = $"Carros/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Carros carro = JsonConvert.DeserializeObject<Carros>(jsonContent);

                        if (carro == null)
                        {
                            return (null, "Carro no encontrado en la API.");
                        }
                        else
                        {
                            return (carro, "Carro cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el carro desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

    }
}
