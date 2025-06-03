using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using System.Threading.Tasks;

namespace CaseControl.Api.Interfaces
{
    public interface ICaseStatus
    {
        Task<List<CaseStatus>> GetAllCaseStatusAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.CaseStatus> GetCaseStatusByIDAsync(int id);
        Task<CaseStatus> AddCaseStatusAsync(CaseStatus model);
        Task<CaseStatus> EditCaseStatusAsync(CaseStatus model);
        Task<bool> DeleteCaseStatusAsync(int id);
        bool CaseStatusExists(int id);
    }
}
