using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IEvidence
    {
        Task<List<Evidence>> GetAllEvidenceAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Evidence> GetEvidenceByHashAsync(string hash);
        Task<Evidence> UploadEvidenceAsync(IFormFile file, Evidence model, string path);
        Task<IFormFile> DownloadEvidenceAsync(string path);
        Task<bool> DeleteEvidenceAsync(string hash);
        bool EvidenceExists(string hash);
    }
}
