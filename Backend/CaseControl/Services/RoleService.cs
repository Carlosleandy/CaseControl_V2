using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class RoleService : IRole
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Role>> GetAllRoleAsync(PaginationDTO pagination)
        {
            var queryable = _context.Roles
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
            var queryable = _context.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Role> GetRoleByIDAsync(int id)
        {
            var Role = await _context.Roles
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return Role!;
        }

        public async Task<Role> AddRoleAsync(Role model)
        {
            _context.Roles.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Role> EditRoleAsync(Role model)
        {
             _context.Roles.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var bin = await _context.Roles.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool RoleExists(int id)
        {
            return (_context.Roles?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
