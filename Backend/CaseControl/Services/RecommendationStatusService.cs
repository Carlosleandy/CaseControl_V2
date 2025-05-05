using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class RecommendationStatusService : IRecommendationStatus
    {
        private readonly DataContext _context;

        public RecommendationStatusService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<RecommendationStatus>> GetAllRecommendationStatusAsync(PaginationDTO pagination)
        {
            var queryable = _context.RecommendationStatuses
                .Include(a => a.Recommendations)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.Name)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.RecommendationStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<RecommendationStatus> GetRecommendationStatusByIDAsync(int id)
        {
            var status = await _context.RecommendationStatuses
                .Include(a => a.Recommendations)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return status!;
        }

        public async Task<RecommendationStatus> AddRecommendationStatusAsync(RecommendationStatus model)
        {
            _context.RecommendationStatuses.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<RecommendationStatus> EditRecommendationStatusAsync(RecommendationStatus model)
        {
            _context.RecommendationStatuses.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteRecommendationStatusAsync(int id)
        {
            var status = await _context.RecommendationStatuses.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(status!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool RecommendationStatusExists(int id)
        {
            return (_context.RecommendationStatuses?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
