using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class RecommendationService : IRecommendation
    {
        private readonly DataContext _context;

        public RecommendationService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Recommendation>> GetAllRecommendationAsync(PaginationDTO pagination)
        {
            var queryable = _context.Recommendations
                .Include(a=>a.RecommendationStatus)
                .Include(a => a.Case)
                .Include(a => a.User)
                .Include(a => a.RecommendationType)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Title!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.Title)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Recommendations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Title!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Recommendation> GetRecommendationByIDAsync(int id)
        {
            var recommendation = await _context.Recommendations
                .Include(a => a.RecommendationStatus)
                .Include(a => a.Case)
                .Include(a => a.User)
                .Include(a => a.RecommendationType)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return recommendation!;
        }

        public async Task<Recommendation> AddRecommendationAsync(Recommendation model)
        {
            try
            {
                model.UserID = 1;
                model.DateRegistered = DateTime.Now;

                _context.Recommendations.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateException)
            {
                // Re-lanzar la excepción original
                throw;
            }
        }

        public async Task<Recommendation> EditRecommendationAsync(Recommendation model)
        {
            try
            {
                model.User = null;
                model.RecommendationStatus = null;
                model.Case = null;
                model.RecommendationType = null;

                _context.Recommendations.Update(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateException)
            {
                // Re-lanzar la excepción original
                throw;
            }
        }

        public async Task<bool> DeleteRecommendationAsync(int id)
        {
            var recommendation = await _context.Recommendations.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(recommendation!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool RecommendationExists(int id)
        {
            return (_context.Recommendations?.Any(e => e.ID == id)).GetValueOrDefault();
        }





        public async Task<List<Recommendation>> GetCasesByRecommendationTypeAsync(int id)
        {
            var cases = await _context.Recommendations
                .Include(a => a.RecommendationType)
                .Include(a => a.RecommendationStatus)
                .Include(a => a.User)
                .Include(a => a.Case)
                    .ThenInclude(a => a.CaseStatus)
                .Include(a => a.Case)
                    .ThenInclude(a => a.CaseType)
                .Where(a => a.RecommendationTypeID == id)
                .ToListAsync();

            return cases!;
        }

        public async Task<List<CasesByRecommendationTypeSummaryDTO>> GetCasesByRecommendationTypeSummaryAsync()
        {
            List<CasesByRecommendationTypeSummaryDTO> result = new List<CasesByRecommendationTypeSummaryDTO>();

            var cases = await _context.Recommendations
                .Include(a => a.RecommendationType!)
                .GroupBy(a => new
                {
                    IDRecommendationType = a.RecommendationTypeID,
                    RecommendationType = a.RecommendationType!.Name,
                })
                .Select(a => new
                {
                    IDRecommendation = a.Key.IDRecommendationType,
                    RecommendationType = a.Key.RecommendationType,
                    Count = a.Count(),
                })
                .ToListAsync();

            foreach (var item in cases)
            {
                result.Add(new CasesByRecommendationTypeSummaryDTO
                {
                    ID = item.IDRecommendation,
                    RecommendationType = item.RecommendationType,
                    Count = item.Count,
                });
            }

            return result!;
        }



        public async Task<byte[]> GeneratePDFCasesByRecommendationTypeSummaryAsync()
        {
            var summary = await GetCasesByRecommendationTypeSummaryAsync();

            using (var ms = new MemoryStream())
            {
                var writer = new PdfWriter(ms);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER);

                document.Add(new Paragraph($"{DateTime.Now.ToString("dd-MM-yyyy")} {DateTime.Now.ToLocalTime().ToString("HH:mm:ss")}")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                  .SetFontSize(8));

                document.Add(new Paragraph("user")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                  .SetFontSize(8));

                document.Add(new Paragraph("Resúmen de Casos por Tipo de Recomendación")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 80, 20 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo de Recomendación")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold());

                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.RecommendationType)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Count.ToString())).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }


        public async Task<byte[]> GeneratePDFCasesByRecommendationTypeAsync(int id)
        {
            var summary = await GetCasesByRecommendationTypeAsync(id);

            if (summary.Count > 0)
            {
                using (var ms = new MemoryStream())
                {
                    var writer = new PdfWriter(ms);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER.Rotate());

                    

                    document.Add(new Paragraph($"{DateTime.Now.ToString("dd-MM-yyyy")} {DateTime.Now.ToLocalTime().ToString("HH:mm:ss")}")
                      .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                      .SetFontSize(8));

                    document.Add(new Paragraph("user")
                      .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                      .SetFontSize(8));

                    document.Add(new Paragraph($"Listado de Casos con Recomendación Tipo: ({summary.FirstOrDefault()!.RecommendationTypeID} - {summary.FirstOrDefault()!.RecommendationType!.Name})")
                      .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                      .SetFontSize(16)
                      .SetBold());

                    var table = new Table(UnitValue.CreatePercentArray(new float[] { 5, 25, 10, 5, 15, 10, 10, 10, 10 }))
                                            .UseAllAvailableWidth()
                                            .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Asunto")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. Comunicación")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Oficina")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Detectado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Investigado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Recuperado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Pérdida")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));

                    foreach (var item in summary)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.Case!.ID.ToString()).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.Subject == null ? "" : item.Case.Subject).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.CommunicationNumber == null ? "" : item.Case.CommunicationNumber).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.Branch == null ? "" : item.Case.Branch).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.CaseType == null ? "" : item.Case!.CaseType!.Name).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.AmountDetected.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.AmountInvestigated.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.AmountRecovered.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.Case.AmountLost.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                    }

                    document.Add(table);
                    document.Close();
                    return ms.ToArray();
                }
            }
            else
            {
                return null!;
            }
        }



    }
}
