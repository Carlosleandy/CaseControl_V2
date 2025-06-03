using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IGerencia
    {
        Task<List<Gerencia>> GetAllGerenciaAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Gerencia> GetGerenciaByIDAsync(int id);
        Task<Gerencia> AddGerenciaAsync(Gerencia model);
        Task<Gerencia> EditGerenciaAsync(Gerencia model);
        Task<bool> DeleteGerenciaAsync(int id);
        bool GerenciaExists(int id);
    }
}