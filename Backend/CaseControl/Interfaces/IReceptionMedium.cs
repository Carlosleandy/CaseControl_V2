using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IReceptionMedium
    {
        Task<List<ReceptionMedium>> GetAllReceptionMediumAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.ReceptionMedium> GetReceptionMediumByIDAsync(int id);
        Task<ReceptionMedium> AddReceptionMediumAsync(ReceptionMedium model);
        Task<ReceptionMedium> EditReceptionMediumAsync(ReceptionMedium model);
        Task<bool> DeleteReceptionMediumAsync(int id);
        bool ReceptionMediumExists(int id);
    }
}
