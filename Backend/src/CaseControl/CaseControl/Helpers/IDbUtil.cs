using CaseControl.Domain.Entities;
// Verificado por Carlos Leandy pasante, el varon 
namespace CaseControl.Api.Helpers
{
    public interface IDbUtil
    {
        Task<List<vwOficinas>> GetAllOfficesAsync();
        Task<List<vwCostCenter>> GetAllCostCentersAsync();
        Task<vwEmployee> GetEmployeeByCodeIdentAsync(string ident);
    }
}
