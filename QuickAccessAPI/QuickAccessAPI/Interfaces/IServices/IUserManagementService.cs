using QuickAccessAPI.Dto;

namespace QuickAccessAPI.Interfaces.IServices
{
    public interface IUserManagementService
    {
        Task<IEnumerable<AdminDTO>> GetAllAdminsAsync();
        Task<IEnumerable<SiteManagerDTO>> GetAllSiteManagersAsync();
        Task<IEnumerable<Dto.SecurityDTO>> GetAllSecuritiesAsync();
        Task<IEnumerable<ResidentDTO>> GetAllResidentsAsync();
        Task<IEnumerable<SecurityDTO>> GetSecuritiesForSiteManagerAsync(Guid userId);
        Task<IEnumerable<ResidentDTO>> GetResidentsForSiteManagerAsync(Guid userId);


        Task<bool> DeleteAdminAsync(Guid id);
        Task<bool> DeleteSiteManagerAsync(Guid id);
        Task<bool> DeleteSecurityAsync(Guid id);
        Task<bool> DeleteResidentAsync(Guid id);
    }
}
