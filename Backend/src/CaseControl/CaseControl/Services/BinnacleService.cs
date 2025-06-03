using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class BinnacleService : IBinnacle
    {
        private readonly DataContext _context;

        public BinnacleService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Binnacle>> GetAllBinnacleAsync(PaginationDTO pagination)
        {
            var queryable = _context.Binnacles
                .Include(a => a.Case)
                .Include(a => a.User)
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
            var queryable = _context.Binnacles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Binnacle> GetBinnacleByIDAsync(int id)
        {
            var binnacle = await _context.Binnacles
                .Include(a => a.Case)
                .Include(a => a.User)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return binnacle!;
        }

        public async Task<Binnacle> AddBinnacleAsync(Binnacle model)
        {
            model.UserID = 1;
            model.DateRegistered = DateTime.Now;

            // Break circular references
            model.User = null;
            model.Case = null;

            _context.Binnacles.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Binnacle> EditBinnacleAsync(Binnacle model)
        {
            model.User = null;
            model.Case = null;

            _context.Binnacles.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteBinnacleAsync(int id)
        {
            var bin = await _context.Binnacles.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool BinnacleExists(int id)
        {
            return (_context.Binnacles?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
