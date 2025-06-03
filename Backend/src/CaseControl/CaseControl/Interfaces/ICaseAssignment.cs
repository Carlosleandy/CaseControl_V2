using CaseControl.Domain.Entities;
using CaseControl.Domain.DTOs;

namespace CaseControl.Api.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de asignaciones de casos.
    /// </summary>
    public interface ICaseAssignment
    {

        Task<List<CaseAssignment>> GetAllCaseAssignmentAsync(PaginationDTO pagination);
       
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);

        Task<CaseAssignment> GetCaseAssignmentByIDAsync(int id);

        Task<CaseAssignment> AddCaseAssignmentAsync(CaseAssignment model);

        Task<CaseAssignment> EditCaseAssignmentAsync(CaseAssignment model);

        Task<bool> DeleteCaseAssignmentAsync(int id);

        bool CaseAssignmentExists(int id);

    }
}
