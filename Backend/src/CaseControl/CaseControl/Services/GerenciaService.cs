using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class GerenciaService : IGerencia
    {
        private readonly DataContext _context;

        public GerenciaService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Gerencia>> GetAllGerenciaAsync(PaginationDTO pagination)
        {
            var queryable = _context.Gerencias
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
            var queryable = _context.Gerencias.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Gerencia> GetGerenciaByIDAsync(int id)
        {
            var type = await _context.Gerencias
                .Include(a => a.Cases)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return type!;
        }

        public async Task<Gerencia> AddGerenciaAsync(Gerencia model)
        {
            _context.Gerencias.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Gerencia> EditGerenciaAsync(Gerencia model)
        {
            _context.Gerencias.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteGerenciaAsync(int id)
        {
            var type = await _context.Gerencias.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(type!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool GerenciaExists(int id)
        {
            return (_context.Gerencias?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
