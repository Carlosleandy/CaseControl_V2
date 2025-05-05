using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CaseControl.Api.Services
{
    public class EvidenceService : IEvidence
    {
        private readonly DataContext _context;

        public EvidenceService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Evidence>> GetAllEvidenceAsync(PaginationDTO pagination)
        {
            var queryable = _context.Evidences
                .Include(a => a.EvidenceClassification)
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
            var queryable = _context.Evidences.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Evidence> GetEvidenceByHashAsync(string hash)
        {
            var evidence = await _context.Evidences
                .Include(a => a.EvidenceClassification)
                .Include(a => a.Case)
                .Where(a => a.Hash.ToString() == hash)
                 .FirstOrDefaultAsync();

            return evidence!;
        }

        public async Task<Evidence> UploadEvidenceAsync(IFormFile file, Evidence model, string path)
        {
            var hash = Guid.NewGuid();
            var fileName = hash + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(path, fileName);

            model.Case = null;
            model.EvidenceClassification = null;
            model.DateRegistered = DateTime.Now;
            model.Extension = Path.GetExtension(file.FileName);
            model.SizeKB = (file.Length / 1000);
            model.Hash = hash;
            model.Path = filePath;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _context.Evidences.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public Task<IFormFile> DownloadEvidenceAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEvidenceAsync(string hash)
        {
            var evi = await GetEvidenceByHashAsync(hash);
            _context.Remove(evi);

            if (System.IO.File.Exists(evi.Path))
            {
                System.IO.File.Delete(evi.Path);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public bool EvidenceExists(string hash)
        {
            return (_context.Evidences?.Any(e => e.Hash.ToString() == hash)).GetValueOrDefault();
        }


    }
}
