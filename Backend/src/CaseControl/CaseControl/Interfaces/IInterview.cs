using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IInterview
    {
        Task<List<Interview>> GetAllInterviewAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Interview> GetInterviewByIDAsync(int id);
        Task<Interview> AddInterviewAsync(Interview model);
        Task<Interview> EditInterviewAsync(Interview model);
        Task<bool> DeleteInterviewAsync(int id);
        bool InterviewExists(int id);
        Task<List<IntervieweeType>> GetAllIntervieweeTypesAsync();
    }
}
