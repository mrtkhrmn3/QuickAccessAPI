namespace QuickAccessAPI.Interfaces.IServices
{
    public interface IUserManagementService
    {
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        Task<IEnumerable<SiteManager>> GetAllSiteManagersAsync();
        Task<IEnumerable<Security>> GetAllSecuritiesAsync();
        Task<IEnumerable<Resident>> GetAllResidentsAsync();

        Task<bool> DeleteAdminAsync(Guid id);
        Task<bool> DeleteSiteManagerAsync(Guid id);
        Task<bool> DeleteSecurityAsync(Guid id);
        Task<bool> DeleteResidentAsync(Guid id);
    }
}
