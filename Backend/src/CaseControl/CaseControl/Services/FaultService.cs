using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class FaultService : IFault
    {
        private readonly DataContext _context;

        public FaultService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Fault>> GetAllFaultsAsync(PaginationDTO pagination)
        {
            var queryable = _context.Faults
                .Include(a=>a.FaultType)
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
            var queryable = _context.Faults.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Fault> GetFaultByIDAsync(int id)
        {
            var Fault = await _context.Faults
                .Include(a => a.FaultType)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return Fault!;
        }

        public async Task<Fault> AddFaultAsync(Fault model)
        {
            _context.Faults.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Fault> EditFaultAsync(Fault model)
        {
            model.FaultType = null;

            _context.Faults.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteFaultAsync(int id)
        {
            var bin = await _context.Faults.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool FaultExists(int id)
        {
            return (_context.Faults?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        public async Task<List<FaultType>> GetAllFaultTypesAsync()
        {
            var queryable = _context.FaultTypes
                  .AsQueryable();

            return await queryable
                        .OrderBy(x => x.ID)
                        .ToListAsync();
        }

    }
}
