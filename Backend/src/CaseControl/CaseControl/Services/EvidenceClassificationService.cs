using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class EvidenceClassificationService : IEvidenceClassification
    {
        private readonly DataContext _context;

        public EvidenceClassificationService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<EvidenceClassification>> GetAllEvidenceClassificationAsync(PaginationDTO pagination)
        {
            var queryable = _context.EvidenceClassifications
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
            var queryable = _context.EvidenceClassifications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<EvidenceClassification> GetEvidenceClassificationByIDAsync(int id)
        {
            var EvidenceClassification = await _context.EvidenceClassifications
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return EvidenceClassification!;
        }

        public async Task<EvidenceClassification> AddEvidenceClassificationAsync(EvidenceClassification model)
        {
            _context.EvidenceClassifications.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<EvidenceClassification> EditEvidenceClassificationAsync(EvidenceClassification model)
        {
            _context.EvidenceClassifications.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteEvidenceClassificationAsync(int id)
        {
            var bin = await _context.EvidenceClassifications.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool EvidenceClassificationExists(int id)
        {
            return (_context.EvidenceClassifications?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
