using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Interfaces
{
    public interface IRecommendation
    {
        Task<List<Recommendation>> GetAllRecommendationAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Recommendation> GetRecommendationByIDAsync(int id);
        Task<Recommendation> AddRecommendationAsync(Recommendation model);
        Task<Recommendation> EditRecommendationAsync(Recommendation model);
        Task<bool> DeleteRecommendationAsync(int id);
        bool RecommendationExists(int id);



        Task<List<Recommendation>> GetCasesByRecommendationTypeAsync(int id);
        Task<List<CasesByRecommendationTypeSummaryDTO>> GetCasesByRecommendationTypeSummaryAsync();


        Task<byte[]> GeneratePDFCasesByRecommendationTypeSummaryAsync();
        Task<byte[]> GeneratePDFCasesByRecommendationTypeAsync(int id);
    }
}
