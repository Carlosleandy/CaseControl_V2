using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IEvidenceClassification
    {
        Task<List<EvidenceClassification>> GetAllEvidenceClassificationAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.EvidenceClassification> GetEvidenceClassificationByIDAsync(int id);
        Task<EvidenceClassification> AddEvidenceClassificationAsync(EvidenceClassification model);
        Task<EvidenceClassification> EditEvidenceClassificationAsync(EvidenceClassification model);
        Task<bool> DeleteEvidenceClassificationAsync(int id);
        bool EvidenceClassificationExists(int id);
    }
}
