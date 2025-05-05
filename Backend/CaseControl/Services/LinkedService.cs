using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class LinkedService : ILinked
    {
        private readonly DataContext _context;

        public LinkedService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Linked>> GetAllLinkedAsync(PaginationDTO pagination)
        {
            var queryable = _context.Linkeds
                .Include(a => a.LinkType)
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
            var queryable = _context.Linkeds.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Linked> GetLinkedByIDAsync(int id)
        {
            var type = await _context.Linkeds
                .Include(a => a.LinkType)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return type!;
        }

        public async Task<Linked> AddLinkedAsync(Linked model)
        {
            _context.Linkeds.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Linked> EditLinkedAsync(Linked model)
        {
            model.LinkType = null;

            _context.Linkeds.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteLinkedAsync(int id)
        {
            var type = await _context.Linkeds.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(type!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool LinkedExists(int id)
        {
            return (_context.Linkeds?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        public async Task<List<LinkType>> GetAllLinkTypesAsync()
        {
            var queryable = _context.LinkTypes
                  .AsQueryable();

            return await queryable
                        .OrderBy(x => x.ID)
                        .ToListAsync();
        }


    }
}
