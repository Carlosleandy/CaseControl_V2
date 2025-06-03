using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class ReceptionMediumService : IReceptionMedium
    {
        private readonly DataContext _context;

        public ReceptionMediumService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<ReceptionMedium>> GetAllReceptionMediumAsync(PaginationDTO pagination)
        {
            var queryable = _context.ReceptionMedia
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
            var queryable = _context.ReceptionMedia.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<ReceptionMedium> GetReceptionMediumByIDAsync(int id)
        {
            var status = await _context.ReceptionMedia
                .Include(a => a.Cases)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return status!;
        }

        public async Task<ReceptionMedium> AddReceptionMediumAsync(ReceptionMedium model)
        {
            _context.ReceptionMedia.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ReceptionMedium> EditReceptionMediumAsync(ReceptionMedium model)
        {
            _context.ReceptionMedia.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteReceptionMediumAsync(int id)
        {
            var status = await _context.ReceptionMedia.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(status!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool ReceptionMediumExists(int id)
        {
            return (_context.ReceptionMedia?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
