using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface ILinked
    {
        Task<List<Linked>> GetAllLinkedAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Linked> GetLinkedByIDAsync(int id);
        Task<Linked> AddLinkedAsync(Linked model);
        Task<Linked> EditLinkedAsync(Linked model);
        Task<bool> DeleteLinkedAsync(int id);
        bool LinkedExists(int id);

        Task<List<LinkType>> GetAllLinkTypesAsync();
    }
}
