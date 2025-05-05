using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class LinkTypeService: ILinkType
    {
        private readonly DataContext _context;

        public LinkTypeService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<LinkType>> GetAllLinkTypeAsync(PaginationDTO pagination)
        {
            var queryable = _context.LinkTypes
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.Name)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.LinkTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<LinkType> GetLinkTypeByIDAsync(int id)
        {
            var LinkType = await _context.LinkTypes
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return LinkType!;
        }

        public async Task<LinkType> AddLinkTypeAsync(LinkType model)
        {
            _context.LinkTypes.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<LinkType> EditLinkTypeAsync(LinkType model)
        {
            _context.LinkTypes.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteLinkTypeAsync(int id)
        {
            var bin = await _context.LinkTypes.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool LinkTypeExists(int id)
        {
            return (_context.LinkTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
