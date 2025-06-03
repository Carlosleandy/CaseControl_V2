using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class UserLevelService : IUserLevel
    {
        private readonly DataContext _context;

        public UserLevelService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<UserLevel>> GetAllUserLevelAsync(PaginationDTO pagination)
        {
            var queryable = _context.UserLevels
                .Include(a=>a.Users)
                .Include(a=>a.Role)
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
            var queryable = _context.UserLevels.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<UserLevel> GetUserLevelByIDAsync(int id)
        {
            var ulevel = await _context.UserLevels
                .Include(a => a.Users)
                .Include(a => a.Role)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return ulevel!;
        }

        public async Task<UserLevel> AddUserLevelAsync(UserLevel model)
        {
            _context.UserLevels.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<UserLevel> EditUserLevelAsync(UserLevel model)
        {
            model.Role = null;

            _context.UserLevels.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteUserLevelAsync(int id)
        {
            var ulevel = await _context.UserLevels.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(ulevel!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserLevelExists(int id)
        {
            return (_context.UserLevels?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
