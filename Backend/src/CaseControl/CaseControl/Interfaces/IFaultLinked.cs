using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IFaultLinked
    {
        Task<List<FaultLinked>> GetAllFaultLinkedAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.FaultLinked> GetFaultLinkedByLinkedAsync(int id);
        Task<List<FaultLinked>> GetFaultLinkedByCaseIDAsync(int caseid);
        Task<List<FaultLinked>> GetFaultLinkedsByLinkedCodeAsync(string linkedid);
        Task<FaultLinked> AddFaultLinkedAsync(FaultLinked model);
        Task<bool> DeleteFaultLinkedAsync(int id);
        bool FaultLinkedExists(int id);

        Task<byte[]> GeneratePDFFaultsByLinkedCodeAsync(string code);
    }
}
