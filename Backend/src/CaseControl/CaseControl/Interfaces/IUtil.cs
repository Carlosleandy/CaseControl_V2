namespace CaseControl.Api.Interfaces
{  // Agregado por el Pasante Carlos Leandy Moreno Reyes (Alea: EL Varon)
    public interface IUtil
    {
        Task<string> encriptarSHA256(string texto);
        // Otros métodos si los hay
    }
}