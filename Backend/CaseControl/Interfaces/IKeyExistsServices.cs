namespace CaseControl.Api.Interfaces
{
    public interface IKeyExistsServices
    {
        Task<bool> KeyExists(string key);
    }
}
