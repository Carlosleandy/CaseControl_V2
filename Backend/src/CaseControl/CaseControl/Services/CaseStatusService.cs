using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CaseControl.Api.Services
{
    public class CaseStatusService : ICaseStatus
    {
        private readonly DataContext _context;

        public CaseStatusService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<CaseStatus>> GetAllCaseStatusAsync(PaginationDTO pagination)
        {
           var queryable = _context.CaseStatuses
                .Include(a => a.Cases)
                 .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Percent.ToString().ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.ID)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.CaseStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Percent.ToString().ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<CaseStatus> GetCaseStatusByIDAsync(int id)
        {
            var status = await _context.CaseStatuses
                .Include(a => a.Cases)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return status!;
        }

        public async Task<CaseStatus> AddCaseStatusAsync(CaseStatus model)
        {
            _context.CaseStatuses.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<CaseStatus> EditCaseStatusAsync(CaseStatus model)
        {
            _context.CaseStatuses.Update(model); ;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteCaseStatusAsync(int id)
        {
            var status = await _context.CaseStatuses.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(status!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CaseStatusExists(int id)
        {
            return (_context.CaseStatuses?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
