using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        // Ignorar certificados SSL para desarrollo local
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        
        using var client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://localhost:7188/");
        
        // Crear el objeto de usuario
        var user = new
        {
            userName = "cmesa",
            password = "cmesa",
            isActive = true,
            isAdmin = false,
            workingGroupID = 1,
            userLevelID = 1
        };
        
        try
        {
            // Serializar el objeto a JSON
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json");
            
            // Enviar la solicitud POST
            var response = await client.PostAsync("api/User", content);
            
            // Leer y mostrar la respuesta
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Response: {responseContent}");
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Usuario 'cmesa' creado exitosamente con contraseña encriptada.");
            }
            else
            {
                Console.WriteLine($"Error al crear el usuario: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
