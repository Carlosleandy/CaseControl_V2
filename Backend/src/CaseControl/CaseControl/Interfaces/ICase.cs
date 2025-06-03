using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Interfaces
{
    public interface ICase
    {
        Task<List<Case>> GetAllCaseAsync(PaginationDTO pagination);
        Task<List<Case>> GetAllCaseOnlyAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Case> GetCaseByIDAsync(int id);
        Task<Case> AddCaseAsync(Case model);
        Task<Case> EditCaseAsync(Case model);
        Task<bool> DeleteCaseAsync(int id);
        bool CaseExists(int id);


        Task<CaseStatusChange> AddCaseStatusChangeAsync(CaseStatusChange model);

        Task<List<RecoveryHistory>> GetCasesAmountRecoveryAsync(int caseID);
        Task<RecoveryHistory> AddCaseAmountRecoveryAsync(RecoveryHistory model);

        Task<List<CaseStatusChange>> GetCasesStatusChangeHistAsync(int caseID);

        Task<RankingCasesUserDTO> GetRankingCasesByUserAsync();

        Task<List<Case>> GetCasesByStatusAsync(int statusID);
        Task<List<CasesByStatusSummaryDTO>> GetCasesByStatusSummaryAsync();


        Task<List<Case>> GetCasesByUserNameAsync(string username);
        Task<List<CasesByUserNameSummaryDTO>> GetCasesByUserNameSummaryAsync();


        Task<List<CasesRecoverySummaryDTO>> GetCasesRecoverySummaryAsync();

        Task<CasesByLinkedCodeDTO> GetCasesByCodeLinkedAsync(string code);
        Task<byte[]> GeneratePDFRankingCasesByUserAsync();

        Task<byte[]> GeneratePDFCasesByLinkedCodeAsync(string code);
        Task<byte[]> GeneratePDFCasesStatusChangeHistAsync(int caseid);

        Task<byte[]> GeneratePDFCasesByStatusSummaryAsync();
        Task<byte[]> GeneratePDFCasesByUserNameSummaryAsync();


        Task<byte[]> GeneratePDFCasesByStatusAsync(int id);

        Task<byte[]> GeneratePDFCasesByUserNameAsync(string username);


        Task<byte[]> GeneratePDFCasesRecoverySummaryAsync();
        Task<byte[]> GeneratePDFCasesAmountRecoveryAsync(int caseid);


    }
}
