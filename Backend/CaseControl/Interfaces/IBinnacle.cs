using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Interfaces
{
    public interface IBinnacle
    {
        Task<List<Binnacle>> GetAllBinnacleAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Binnacle> GetBinnacleByIDAsync(int id);
        Task<Binnacle> AddBinnacleAsync(Binnacle model);
        Task<Binnacle> EditBinnacleAsync(Binnacle model);
        Task<bool> DeleteBinnacleAsync(int id);
        bool BinnacleExists(int id);
    }
}
