using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class RecommendationTypeService : IRecommendationType
    {
        private readonly DataContext _context;

        public RecommendationTypeService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<RecommendationType>> GetAllRecommendationTypeAsync(PaginationDTO pagination)
        {
            var queryable = _context.RecommendationTypes
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
            var queryable = _context.RecommendationTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<RecommendationType> GetRecommendationTypeByIDAsync(int id)
        {
            var type = await _context.RecommendationTypes
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return type!;
        }

        public async Task<RecommendationType> AddRecommendationTypeAsync(RecommendationType model)
        {
            _context.RecommendationTypes.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<RecommendationType> EditRecommendationTypeAsync(RecommendationType model)
        {
            _context.RecommendationTypes.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteRecommendationTypeAsync(int id)
        {
            var type = await _context.RecommendationTypes.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(type!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool RecommendationTypeExists(int id)
        {
            return (_context.RecommendationTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
