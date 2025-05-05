using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace CaseControl.Api.Services
{
    public class FaultLinkedService : IFaultLinked
    {
        private readonly DataContext _context;

        public FaultLinkedService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<FaultLinked>> GetAllFaultLinkedAsync(PaginationDTO pagination)
        {
            var queryable = _context.FaultLinkeds
                .Include(a => a.Case)
                .Include(a => a.Linked)
                .Include(a => a.Fault)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.ID)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.FaultLinkeds.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<FaultLinked> GetFaultLinkedByLinkedAsync(int id)
        {
            var FaultLinked = await _context.FaultLinkeds
                .Include(a => a.Case)
                .Include(a => a.Linked)
                .Include(a => a.Fault)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return FaultLinked!;
        }

        public async Task<List<FaultLinked>> GetFaultLinkedByCaseIDAsync(int caseid)
        {
            return await _context.FaultLinkeds
                .Include(a => a.Case)
                .Include(a => a.Linked)
                .Include(a => a.Fault)
                .Where(a => a.CaseID == caseid)
                .OrderBy(x => x.ID)
                .ToListAsync();
        }

        public async Task<List<FaultLinked>> GetFaultLinkedsByLinkedCodeAsync(string linkedid)
        {
            return await _context.FaultLinkeds
                .Include(a => a.Linked)
                    .ThenInclude(a => a.LinkType)
                .Include(a => a.Fault)
                    .ThenInclude(a => a.FaultType)
                .Where(a => a.Linked!.Code == linkedid)
                .OrderBy(x => x.ID)
                .ToListAsync();
        }

        public async Task<FaultLinked> AddFaultLinkedAsync(FaultLinked model)
        {
            model.DateRegistered = DateTime.Now;

            _context.FaultLinkeds.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteFaultLinkedAsync(int id)
        {
            var fl = await _context.FaultLinkeds.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(fl!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool FaultLinkedExists(int id)
        {
            return (_context.FaultLinkeds?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        
        public async Task<byte[]> GeneratePDFFaultsByLinkedCodeAsync(string code)
        {
            var summary = await GetFaultLinkedsByLinkedCodeAsync(code);

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

                document.Add(new Paragraph($"Historial de Faltas del Vinculado - {summary!.FirstOrDefault()!.Linked!.Code} - {summary!.FirstOrDefault()!.Linked!.FullName}")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 10, 10, 10, 70 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha Registro Falta")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo de Falta")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Falta")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
            
                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.CaseID.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.DateRegistered.ToString("dd/MM/yyy"))));
                    table.AddCell(new Cell().Add(new Paragraph(item.Fault!.FaultType == null ? "" : item.Fault.FaultType.Name)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Fault == null ? "" : item.Fault.Name)));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }

    }
}
