using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IFault
    {
        Task<List<Fault>> GetAllFaultsAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Fault> GetFaultByIDAsync(int id);
        Task<Fault> AddFaultAsync(Fault model);
        Task<Fault> EditFaultAsync(Fault model);
        Task<bool> DeleteFaultAsync(int id);
        bool FaultExists(int id);
        Task<List<FaultType>> GetAllFaultTypesAsync();
    }
}
