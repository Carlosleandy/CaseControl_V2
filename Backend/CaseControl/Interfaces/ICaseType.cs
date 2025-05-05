using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface ICaseType
    {
        Task<List<CaseType>> GetAllCaseTypeAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.CaseType> GetCaseTypeByIDAsync(int id);
        Task<CaseType> AddCaseTypeAsync(CaseType model);
        Task<CaseType> EditCaseTypeAsync(CaseType model);
        Task<bool> DeleteCaseTypeAsync(int id);
        bool CaseTypeExists(int id);
    }
}
