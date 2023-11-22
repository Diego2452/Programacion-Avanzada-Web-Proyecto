using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class HistorialCompraCarroApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public HistorialCompraCarroApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<HistorialCompraCarro> Historiales, string Message)> ObtenerHistorialesAsync()
        {
            string apiEndpoint = "HistorialCompraCarro";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<HistorialCompraCarro> historiales = JsonConvert.DeserializeObject<List<HistorialCompraCarro>>(jsonContent);

                        return (historiales, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron historiales en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener historiales desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearHistorialAsync(HistorialCompraCarro historial)
        {
            string apiEndpoint = "HistorialCompraCarro";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(historial);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El historial ha sido creado.");
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
                        return (false, $"Error al crear el historial. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarHistorialAsync(int id)
        {
            string apiEndpoint = $"HistorialCompraCarro/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Historial eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el historial. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el historial: {ex.Message}");
                }
            }
        }

        public async Task<(HistorialCompraCarro Historial, string Message)> ObtenerDetallesHistorialAsync(int id)
        {
            string apiEndpoint = $"HistorialCompraCarro/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        HistorialCompraCarro historial = JsonConvert.DeserializeObject<HistorialCompraCarro>(jsonContent);

                        if (historial == null)
                        {
                            return (null, "Historial no encontrado en la API.");
                        }
                        else
                        {
                            return (historial, "Historial cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el historial desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarHistorialAsync(HistorialCompraCarro historial)
        {
            string apiEndpoint = $"HistorialCompraCarro/{historial.IdHistorialCompraCarro}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(historial);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El historial ha sido modificado.");
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
                        return (false, $"Error al actualizar el historial. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el historial: {ex.Message}");
                }
            }
        }
    }

}
