using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class CaseTypeService : ICaseType
    {
        private readonly DataContext _context;

        public CaseTypeService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<CaseType>> GetAllCaseTypeAsync(PaginationDTO pagination)
        {
            var queryable = _context.CaseTypes
                .Include(a => a.Cases)
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
            var queryable = _context.CaseTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<CaseType> GetCaseTypeByIDAsync(int id)
        {
            var type = await _context.CaseTypes
                .Include(a => a.Cases)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return type!;
        }

        public async Task<CaseType> AddCaseTypeAsync(CaseType model)
        {
            _context.CaseTypes.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<CaseType> EditCaseTypeAsync(CaseType model)
        {
            _context.CaseTypes.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteCaseTypeAsync(int id)
        {
            var type = await _context.CaseTypes.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(type!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CaseTypeExists(int id)
        {
            return (_context.CaseTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
