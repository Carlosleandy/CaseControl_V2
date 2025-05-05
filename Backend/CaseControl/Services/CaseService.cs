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
using System.Collections.Generic;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace CaseControl.Api.Services
{
    public class CaseService : ICase
    {
        private readonly DataContext _context;
        private readonly IUser _user;
        private readonly IFaultLinked _faultLinked;

        public CaseService(DataContext context, IUser user, IFaultLinked faultLinked)
        {
            _context = context;
            _user = user;
            _faultLinked = faultLinked;
        }


        public async Task<List<Case>> GetAllCaseAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cases
                .Include(a => a.CaseStatus!)
                .Include(a => a.CaseType!)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CommunicationNumber!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Subject!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.CommunicationNumber!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Branch!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.CaseStatus!.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.CaseType!.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            var result = await queryable
                        .OrderByDescending(x => x.ID)
                        //.Paginate(pagination)
                        .ToListAsync();

            foreach (var item in result)
            {
                var cs = await GetCasesAmountRecoveryAsync(item.ID);
                item.AmountRecovered = cs.Sum(a => a.AmountRecovery);
            }

            return result;
        }

        public async Task<List<Case>> GetAllCaseOnlyAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cases
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CommunicationNumber!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Subject!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.CommunicationNumber!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Branch!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.CaseStatus!.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.CaseType!.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            var result = await queryable
                        .OrderBy(x => x.ID)
                        //.Paginate(pagination)
                        .ToListAsync();

            foreach (var item in result)
            {
                var cs = await GetCasesAmountRecoveryAsync(item.ID);
                item.AmountRecovered = cs.Sum(a => a.AmountRecovery);
            }

            return result;
        }
               

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cases.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CommunicationNumber!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Subject!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Case> GetCaseByIDAsync(int id)
        {
            var cse = await _context.Cases
                 .Include(a => a.CaseStatus!)
                .Include(a => a.ReceptionMedium!)
                .Include(a => a.CaseType!)
                .Include(a => a.User!)
                .Include(a => a.Binnacles!)
                .Include(a => a.CaseAssignments!)
                    .ThenInclude(a => a.User!)
                        .ThenInclude(a => a.Employee)
                .Include(a => a.Linkeds!)
                    .ThenInclude(a => a.LinkType!)
                .Include(a => a.Evidences!)
                    .ThenInclude(a => a.EvidenceClassification!)
                .Include(a => a.Recommendations!)
                    .ThenInclude(a => a.RecommendationStatus!)
                .Include(a => a.Interviews!)
                    .ThenInclude(a => a.IntervieweeType!)
                .Include(a => a.FaultLinkeds!)
                    .ThenInclude(a => a.Linked!)
                .Include(a => a.FaultLinkeds!)
                    .ThenInclude(a => a.Fault!)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            if (cse != null)
            {
                var cs = await GetCasesAmountRecoveryAsync(id);
                cse.AmountRecovered = cs.Sum(a => a.AmountRecovery);
            }

            return cse!;
        }

        public async Task<Case> AddCaseAsync(Case model)
        {
            model.CaseStatus = null;
            model.CaseType = null;
            model.ReceptionMedium = null;

            ///TODO
            model.UserID = 1;
            model.UserNameRegistered = "jmbelen";


            _context.Cases.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Case> EditCaseAsync(Case model)
        {
            model.ReceptionMedium = null;
            model.CaseType = null;
            model.CaseStatus = null;
            model.User = null;

            model.Recommendations = null;
            model.Binnacles = null;
            model.RecoveryHistories = null;
            model.CaseAssignments = null;
            model.CaseStatusChanges = null;
            model.FaultLinkeds = null;
            model.Linkeds = null;
            model.Evidences = null;
            model.Interviews = null;

        _context.Cases.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteCaseAsync(int id)
        {
            var status = await _context.Cases.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(status!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CaseExists(int id)
        {
            return (_context.Cases?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        public async Task<CaseAssignment> AddCaseAssignmentAsync(CaseAssignment model)
        {
            model.Case = null;
            model.User = null;
            model.DateRegistered = DateTime.Now;

            //TODO
            model.userNameRegistered = "jmbelen";


            var cas = _context.Cases.FirstOrDefaultAsync(x => x.ID == model.CaseID).Result;
            cas!.UserID = model.UserID;


            _context.CaseAssignments.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<CaseStatusChange> AddCaseStatusChangeAsync(CaseStatusChange model)
        {
            model.Case = null;
            model.CaseStatus = null;
            model.DateRegistered = DateTime.Now;

            //TODO
            model.userNameRegistered = "jmbelen";

            var cas = _context.Cases.FirstOrDefaultAsync(x => x.ID == model.CaseID).Result;
            cas!.CaseStatusID = model.CaseStatusID;

            _context.CaseStatusChanges.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<RecoveryHistory> AddCaseAmountRecoveryAsync(RecoveryHistory model)
        {
            model.Case = null;
            model.User = null;
            model.DateRegistered = DateTime.Now;

            ///TODO
            model.UserID = 1;


            _context.RecoveryHistories.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<RankingCasesUserDTO> GetRankingCasesByUserAsync()
        {
            var totalCases = await _context.Cases.CountAsync();

            var casesByUser = await _context.Cases
                .GroupBy(a => a.UserNameRegistered)
                .Select(g => new { UserName = g.Key, Count = g.Count() })
                .OrderByDescending(a => a.Count)
                .ToDictionaryAsync(a => a.UserName!, x => x.Count);
                

            List<CasesUserDTO> casesusers = new List<CasesUserDTO>();

            foreach (var item in casesByUser)
            {
                casesusers.Add(new CasesUserDTO
                {
                    User = await _user.GetUserByUserNameAsync(item.Key),
                    CasesCount = item.Value
                });                
            }

            var ranking = new RankingCasesUserDTO
            {
                CasesTotal = totalCases,
                CasesUserDTOs = casesusers
            };
            
            return ranking;
        }


        public async Task<List<RecoveryHistory>> GetCasesAmountRecoveryAsync(int caseID)
        {
            var cse = await _context.RecoveryHistories
                .Include(a => a.Case!)
                .ThenInclude(a => a.CaseStatus)
                .Include(a => a.User!)
                .Where(a => a.CaseID == caseID)
                .OrderBy(a => a.ID)
                .ToListAsync();

            foreach (var item in cse)
            {
                item.User = await _user.GetUserByIDAsync(item.UserID);
            }

            return cse!;
        }

        public async Task<List<CaseStatusChange>> GetCasesStatusChangeHistAsync(int caseID)
        {
            var cse = await _context.CaseStatusChanges
                .Include(a => a.Case!)
                .ThenInclude(a => a.CaseStatus)
                .Where(a => a.CaseID == caseID)
                .OrderBy(a => a.ID)
                .ToListAsync();

            return cse!;
        }

        public async Task<List<CasesRecoverySummaryDTO>> GetCasesRecoverySummaryAsync()
        {
            List<CasesRecoverySummaryDTO> result = new List<CasesRecoverySummaryDTO>();

            var cases = await GetAllCaseAsync(new PaginationDTO());

            foreach (var item in cases)
            {
                item.Interviews = null;
                item.Binnacles = null;
                item.FaultLinkeds = null;
                item.Evidences = null;
                item.Linkeds = null;
                item.Recommendations = null;

                result.Add(new CasesRecoverySummaryDTO
                {
                    Case = item,
                    AmountInvestigated = item.AmountInvestigated,
                    AmountRecovery = item.AmountRecovered,
                    AmountDifference = (item.AmountRecovered - item.AmountInvestigated),
                    PercentRecovery = ((item.AmountRecovered / item.AmountInvestigated) * 100),
                });
            }

            return result!;
        }


        public async Task<List<Case>> GetCasesByStatusAsync(int statusID)
        {
            var cases = await _context.Cases
                 .Include(a => a.CaseStatus!)
                .Include(a => a.ReceptionMedium!)
                .Include(a => a.CaseType!)
                .Include(a => a.User!)
                .Include(a => a.Binnacles!)
                .Include(a => a.Recommendations!)
                    .ThenInclude(a => a.RecommendationStatus!)
                .Where(a => a.CaseStatusID == statusID)
                 .ToListAsync();

            if (cases != null)
            {
                foreach (var cas in cases)
                {
                    var employee = await _context.vwEmployees
                  .Where(a => a.Usuario == cas.User!.UserName)
                   .FirstOrDefaultAsync();

                    cas.User!.Employee = employee;
                }
            }

            return cases!;
        }

        public async Task<List<CasesByStatusSummaryDTO>> GetCasesByStatusSummaryAsync()
        {
            List<CasesByStatusSummaryDTO> result = new List<CasesByStatusSummaryDTO>();

            var cases = await _context.Cases
                .Include(a => a.CaseStatus!)
                .GroupBy(a => new
                {
                    IDStatus = a.CaseStatusID,
                    Status = a.CaseStatus!.Name,
                })
                .Select(a => new
                {
                    IDStatus = a.Key.IDStatus,
                    Status = a.Key.Status,
                    Count = a.Count(),
                })
                .ToListAsync();

            foreach (var item in cases)
            {
                result.Add(new CasesByStatusSummaryDTO
                {
                    ID = item.IDStatus,
                    Status = item.Status,
                    Count = item.Count,
                });
            }

            return result!;
        }


        public async Task<List<Case>> GetCasesByUserNameAsync(string username)
        {
            var cases = await _context.Cases
                 .Include(a => a.CaseStatus!)
                .Include(a => a.ReceptionMedium!)
                .Include(a => a.CaseType!)
                .Include(a => a.User!)
                .Include(a => a.Binnacles!)
                .Include(a => a.Recommendations!)
                    .ThenInclude(a => a.RecommendationStatus!)
                .Where(a => a.User!.UserName!.Trim() == username.Trim())
                 .ToListAsync();

            foreach (var item in cases)
            {
                item.User = await _user.GetUserByIDAsync(item.UserID);
            }

            return cases!;
        }

        public async Task<List<CasesByUserNameSummaryDTO>> GetCasesByUserNameSummaryAsync()
        {
            List<CasesByUserNameSummaryDTO> result = new List<CasesByUserNameSummaryDTO>();

            var cases = await _context.Cases
                .Include(a => a.CaseType!)
                .GroupBy(a => new
                {
                    IDUser = a.UserID,
                })
                .Select(a => new
                {
                    IDUser = a.Key.IDUser,
                    Count = a.Count(),
                })
                .ToListAsync();

            foreach (var item in cases)
            {
                result.Add(new CasesByUserNameSummaryDTO
                {
                    User =  await _user.GetUserByIDAsync(item.IDUser),
                    Count = item.Count,
                });
            }

            return result!;
        }

        public async Task<CasesByLinkedCodeDTO> GetCasesByCodeLinkedAsync(string code)
        {
            var cases = await _context.Cases
                .Include(a => a.CaseType)
                .Where(a => a.Linkeds!.Any(l => l.Code == code))  
                .ToListAsync();

            var result = new CasesByLinkedCodeDTO
            {
                Cases = cases,
                Linked = await _context.Linkeds.Include(a => a.LinkType!).Where(a => a.Code == code).FirstOrDefaultAsync(),
            };

            return result!;
        }

        public async Task<byte[]> GeneratePDFRankingCasesByUserAsync()
        {
            var summary = await GetRankingCasesByUserAsync();

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

                document.Add(new Paragraph($"Ranking de Casos por Usuario")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 70, 10 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("Usuario")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Nombre")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());

                foreach (var item in summary.CasesUserDTOs!)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.User == null ? "" : item.User.UserName))); 
                    table.AddCell(new Cell().Add(new Paragraph(item.User == null ? "" : item.User!.Employee!.Nombre_Completo)));
                    table.AddCell(new Cell().Add(new Paragraph(item.CasesCount.ToString())).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }

        public async Task<byte[]> GeneratePDFCasesByLinkedCodeAsync(string code)
        {
            var summary = await GetCasesByCodeLinkedAsync(code);

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

                document.Add(new Paragraph($"Historial de Casos del Vinculado - {summary.Linked!.Code} - {summary.Linked!.FullName}")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 10, 25, 10, 5, 10, 10, 10, 10, 10 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Asunto")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("No. Comunicación")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Oficina")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Detectado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Investigado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Recuperado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Pérdida")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());

                foreach (var item in summary.Cases!)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.ID.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.Subject == null ? "" : item.Subject)));
                    table.AddCell(new Cell().Add(new Paragraph(item.CommunicationNumber == null ? "" : item.CommunicationNumber)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Branch == null ? "" : item.Branch)));
                    table.AddCell(new Cell().Add(new Paragraph(item.CaseType == null ? "" : item.CaseType!.Name)));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountDetected.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountInvestigated.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountRecovered.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountLost.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }

        public async Task<byte[]> GeneratePDFCasesStatusChangeHistAsync(int caseid)
        {
            var summary = await GetCasesStatusChangeHistAsync(caseid);

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

                document.Add(new Paragraph($"Historial de Cambios de Estado del Caso - {caseid}")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 45, 15, 15, 25 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("Asunto Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha Registro")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Usuario Registró")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Estado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                
                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.Case==null?"":item.Case!.Subject)));
                    table.AddCell(new Cell().Add(new Paragraph(item.DateRegistered.ToString("dd/MM/yyyy hh:mm:ss"))));
                    table.AddCell(new Cell().Add(new Paragraph(item.userNameRegistered)));
                    table.AddCell(new Cell().Add(new Paragraph(item.CaseStatus == null ? "" : item.CaseStatus!.Name)));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }

        public async Task<byte[]> GeneratePDFCasesAmountRecoveryAsync(int caseid)
        {
            var summary = await GetCasesAmountRecoveryAsync(caseid);

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

                document.Add(new Paragraph($"Recuperaciones del Caso - {caseid}")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 5, 15, 15, 15, 15, 35 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Recuperado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Usuario Registró")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha Recuperación")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha Registro")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Observaciones")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
               
                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.ID.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountRecovery.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.User == null ? "" : item.User!.Employee!.Nombre_Completo)));
                    table.AddCell(new Cell().Add(new Paragraph(item.DateRecovery.ToString("dd/MM/yyyy"))));
                    table.AddCell(new Cell().Add(new Paragraph(item.DateRegistered.ToString("dd/MM/yyyy hh:mm:ss"))));
                    table.AddCell(new Cell().Add(new Paragraph(item.Observations == null ? "" : item.Observations!.ToString())));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }

        public async Task<byte[]> GeneratePDFCasesRecoverySummaryAsync()
        {
            var summary = await GetCasesRecoverySummaryAsync();

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

                document.Add(new Paragraph("Resúmen de Recuperaciones por Casos")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 5, 25, 10, 20, 10, 10, 10, 10 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Asunto")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("No. Comunicación")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Investigado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Recuperado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Diferencia")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Porciento Recuperado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
 
                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.Case!.ID.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.Case!.Subject)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Case!.CommunicationNumber)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Case!.CaseType!.Name)));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountInvestigated.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountRecovery.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.AmountDifference.ToString("C2"))).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph(item.PercentRecovery.ToString())).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }


        public async Task<byte[]> GeneratePDFCasesByStatusSummaryAsync()
        {
            var summary=await GetCasesByStatusSummaryAsync();

            using (var ms=new MemoryStream())
            {
                var writer = new PdfWriter(ms);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER);

                //var pageSize = new iText.Kernel.Geom.Rectangle(80, 200);
                //var document = new Document(pdf, pageSize);

                document.Add(new Paragraph($"{DateTime.Now.ToString("dd-MM-yyyy")} {DateTime.Now.ToLocalTime().ToString("HH:mm:ss")}")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                  .SetFontSize(8));

                document.Add(new Paragraph("user")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                  .SetFontSize(8));

                document.Add(new Paragraph("Resúmen de Casos por Estados")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(18)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 80, 20 })) 
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER); 
                             
                table.AddHeaderCell(new Cell().Add(new Paragraph("Estado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetFontColor(ColorConstants.BLACK).SetBold());
               
                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.Status)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Count.ToString())).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }


        public async Task<byte[]> GeneratePDFCasesByUserNameSummaryAsync()
        {
            var summary = await GetCasesByUserNameSummaryAsync();

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

                document.Add(new Paragraph("Resúmen de Casos por Usuario")
                  .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                  .SetFontSize(16)
                  .SetBold());

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 60, 20 }))
                                        .UseAllAvailableWidth()
                                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new Paragraph("Usuario")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Nombre")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold());

                foreach (var item in summary)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.User!.UserName)));
                    table.AddCell(new Cell().Add(new Paragraph(item.User!.Employee!.Nombre_Completo)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Count.ToString())).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }


        public async Task<byte[]> GeneratePDFCasesByStatusAsync(int id)
        {
            var summary = await GetCasesByStatusAsync(id);

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

                    document.Add(new Paragraph($"Listado de Casos en Estado: ({summary.FirstOrDefault()!.CaseStatusID} - {summary.FirstOrDefault()!.CaseStatus!.Name})")
                      .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                      .SetFontSize(16)
                      .SetBold());

                    var table = new Table(UnitValue.CreatePercentArray(new float[] { 5, 25, 10, 5, 15, 20, 5, 5, 5, 5 }))
                                            .UseAllAvailableWidth()
                                            .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Asunto")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. Comunicación")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Oficina")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo Caso")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Usuario Asignado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.LEFT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Detectado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Investigado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Recuperado")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Monto Pérdida")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.RIGHT).SetBold().SetFontSize(10));

                    foreach (var item in summary)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item!.ID.ToString()).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Subject == null ? "" : item.Subject).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.CommunicationNumber == null ? "" : item.CommunicationNumber).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Branch == null ? "" : item.Branch).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.CaseType == null ? "" : item!.CaseType!.Name).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.User == null ? "" : item!.User.Employee!.Nombre_Completo).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountDetected.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountInvestigated.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountRecovered.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountLost.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
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


        public async Task<byte[]> GeneratePDFCasesByUserNameAsync(string username)
        {
            var cases = await GetCasesByUserNameAsync(username);

            if (cases.Count > 0)
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

                    document.Add(new Paragraph($"Listado de Casos del Empleado: ({cases.FirstOrDefault()!.User!.UserName} - {cases.FirstOrDefault()!.User!.Employee!.Nombre_Completo})")
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

                    foreach (var item in cases)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item!.ID.ToString()).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Subject == null ? "" : item.Subject).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.CommunicationNumber == null ? "" : item.CommunicationNumber).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Branch == null ? "" : item.Branch).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.CaseType == null ? "" : item!.CaseType!.Name).SetFontSize(10)));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountDetected.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountInvestigated.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountRecovered.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph(item.AmountLost.ToString("C"))).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
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
