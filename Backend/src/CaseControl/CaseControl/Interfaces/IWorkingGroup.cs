using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IWorkingGroup
    {
        Task<List<WorkingGroup>> GetAllWorkingGroupAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.WorkingGroup> GetWorkingGroupByIDAsync(int id);
        Task<WorkingGroup> AddWorkingGroupAsync(WorkingGroup model);
        Task<WorkingGroup> EditWorkingGroupAsync(WorkingGroup model);
        Task<bool> DeleteWorkingGroupAsync(int id);
        bool WorkingGroupExists(int id);
    }
}
