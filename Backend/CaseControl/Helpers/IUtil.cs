using CaseControl.Domain.Entities;

namespace CaseControl.Api.Helpers
{
    public interface IUtil
    {
        Task<List<vwOficinas>> GetAllOfficesAsync();
        Task<List<vwCostCenter>> GetAllCostCentersAsync();
        Task<vwEmployee> GetEmployeeByCodeIdentAsync(string ident);
    }
}
