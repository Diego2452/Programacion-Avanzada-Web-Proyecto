using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class ModelosCarrosApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ModelosCarrosApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<ModelosCarros> ModelosCarros, string Message)> ObtenerModelosCarrosAsync()
        {
            string apiEndpoint = "ModelosCarros";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<ModelosCarros> modelosCarros = JsonConvert.DeserializeObject<List<ModelosCarros>>(jsonContent);

                        return (modelosCarros, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron modelos de carros en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener modelos de carros desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearModeloCarroAsync(ModelosCarros modeloCarro)
        {
            string apiEndpoint = "ModelosCarros";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(modeloCarro);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El modelo de carro ha sido creado.");
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
                        return (false, $"Error al crear el modelo de carro. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarModeloCarroAsync(int? id)
        {
            if (!id.HasValue)
            {
                return (false, "El ID del modelo de carro no puede ser nulo.");
            }

            string apiEndpoint = $"ModelosCarros/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Modelo de carro eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el modelo de carro. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el modelo de carro: {ex.Message}");
                }
            }
        }

        public async Task<(ModelosCarros ModeloCarro, string Message)> ObtenerDetallesModeloCarroAsync(int? id)
        {
            if (!id.HasValue)
            {
                return (null, "El ID del modelo de carro no puede ser nulo.");
            }

            string apiEndpoint = $"ModelosCarros/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        ModelosCarros modeloCarro = JsonConvert.DeserializeObject<ModelosCarros>(jsonContent);

                        if (modeloCarro == null)
                        {
                            return (null, "Modelo de carro no encontrado en la API.");
                        }
                        else
                        {
                            return (modeloCarro, "Modelo de carro cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el modelo de carro desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarModeloCarroAsync(ModelosCarros modeloCarro)
        {
            if (modeloCarro.IdModelo == null)
            {
                return (false, "El ID del modelo de carro no puede ser nulo.");
            }

            string apiEndpoint = $"ModelosCarros/{modeloCarro.IdModelo}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(modeloCarro);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El modelo de carro ha sido modificado.");
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
                        return (false, $"Error al actualizar el modelo de carro. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el modelo de carro: {ex.Message}");
                }
            }
        }
    }

}
