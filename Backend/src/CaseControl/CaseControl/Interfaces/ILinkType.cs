using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface ILinkType
    {
        Task<List<LinkType>> GetAllLinkTypeAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.LinkType> GetLinkTypeByIDAsync(int id);
        Task<LinkType> AddLinkTypeAsync(LinkType model);
        Task<LinkType> EditLinkTypeAsync(LinkType model);
        Task<bool> DeleteLinkTypeAsync(int id);
        bool LinkTypeExists(int id);
    }
}
