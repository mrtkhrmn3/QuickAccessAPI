using QuickAccessAPI.Dto;

namespace QuickAccessAPI.Interfaces.IServices
{
    public interface IRegistrationService
    {
        Task<bool> RegisterAdminAsync(AdminRegisterDTO dto);
        Task<bool> RegisterSiteManagerAsync(SiteManagerRegisterDTO dto);
        Task<bool> RegisterResidentAsync(ResidentRegisterDTO dto);
        Task<bool> RegisterSecurityAsync(SecurityRegisterDTO dto);
    }
}
