using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IRecommendationType
    {
        Task<List<RecommendationType>> GetAllRecommendationTypeAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.RecommendationType> GetRecommendationTypeByIDAsync(int id);
        Task<RecommendationType> AddRecommendationTypeAsync(RecommendationType model);
        Task<RecommendationType> EditRecommendationTypeAsync(RecommendationType model);
        Task<bool> DeleteRecommendationTypeAsync(int id);
        bool RecommendationTypeExists(int id);
    }
}
