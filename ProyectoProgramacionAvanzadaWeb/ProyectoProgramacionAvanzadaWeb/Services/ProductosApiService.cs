using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class ProductosApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ProductosApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<Productos> Productos, string Message)> ObtenerProductosAsync()
        {
            string apiEndpoint = "Productos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<Productos> productos = JsonConvert.DeserializeObject<List<Productos>>(jsonContent);

                        return (productos, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron productos en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener productos desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearProductoAsync(Productos producto)
        {
            string apiEndpoint = "Productos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(producto);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El producto ha sido creado.");
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
                        return (false, $"Error al crear el producto. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarProductoAsync(int id)
        {
            string apiEndpoint = $"Productos/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Producto eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el producto. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el producto: {ex.Message}");
                }
            }
        }

        public async Task<(Productos Producto, string Message)> ObtenerDetallesProductoAsync(int id)
        {
            string apiEndpoint = $"Productos/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Productos producto = JsonConvert.DeserializeObject<Productos>(jsonContent);

                        if (producto == null)
                        {
                            return (null, "Producto no encontrado en la API.");
                        }
                        else
                        {
                            return (producto, "Producto cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el producto desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarProductoAsync(Productos producto)
        {
            if (producto.IdProducto == 0)
            {
                return (false, "El ID del producto no puede ser 0.");
            }

            string apiEndpoint = $"Productos/{producto.IdProducto}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(producto);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El producto ha sido modificado.");
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
                        return (false, $"Error al actualizar el producto. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el producto: {ex.Message}");
                }
            }
        }
    }

}
