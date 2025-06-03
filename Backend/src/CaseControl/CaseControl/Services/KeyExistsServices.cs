using CaseControl.Api.Interfaces;

namespace CaseControl.Api.Services
{
    public class KeyExistsServices : IKeyExistsServices
    {
        // Ya no necesitamos el HttpClient
        public KeyExistsServices()
        {
            // Constructor vacío
        }

        public Task<bool> KeyExists(string key)
        {
            // Siempre devuelve true para mantener compatibilidad con el código existente
            // mientras se migra completamente al nuevo sistema de autenticación
            return Task.FromResult(true);
        }
    }
}
