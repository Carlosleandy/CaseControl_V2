using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IRecommendationStatus
    {
        Task<List<RecommendationStatus>> GetAllRecommendationStatusAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.RecommendationStatus> GetRecommendationStatusByIDAsync(int id);
        Task<RecommendationStatus> AddRecommendationStatusAsync(RecommendationStatus model);
        Task<RecommendationStatus> EditRecommendationStatusAsync(RecommendationStatus model);
        Task<bool> DeleteRecommendationStatusAsync(int id);
        bool RecommendationStatusExists(int id);
    }
}
