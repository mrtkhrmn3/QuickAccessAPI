using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly ISiteManagerRepository _siteManagerRepo;
        private readonly ISecurityRepository _securityRepo;
        private readonly IResidentRepository _residentRepo;

        public UserManagementService(IAdminRepository adminRepo,
                                     ISiteManagerRepository siteManagerRepo,
                                     ISecurityRepository securityRepo,
                                     IResidentRepository residentRepo)
        {
            _adminRepo = adminRepo;
            _siteManagerRepo = siteManagerRepo;
            _securityRepo = securityRepo;
            _residentRepo = residentRepo;
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync() =>
            await _adminRepo.GetAllAsync();

        public async Task<IEnumerable<SiteManager>> GetAllSiteManagersAsync() =>
            await _siteManagerRepo.GetAllAsync();

        public async Task<IEnumerable<Security>> GetAllSecuritiesAsync() =>
            await _securityRepo.GetAllAsync();

        public async Task<IEnumerable<Resident>> GetAllResidentsAsync() =>
            await _residentRepo.GetAllAsync();

        public async Task<bool> DeleteAdminAsync(Guid id)
        {
            var entity = await _adminRepo.GetByIdAsync(id);
            if (entity == null) return false;
            _adminRepo.Delete(entity);
            return await _adminRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteSiteManagerAsync(Guid id)
        {
            var entity = await _siteManagerRepo.GetByIdAsync(id);
            if (entity == null) return false;
            _siteManagerRepo.Delete(entity);
            return await _siteManagerRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteSecurityAsync(Guid id)
        {
            var entity = await _securityRepo.GetByIdAsync(id);
            if (entity == null) return false;
            _securityRepo.Delete(entity);
            return await _securityRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteResidentAsync(Guid id)
        {
            var entity = await _residentRepo.GetByIdAsync(id);
            if (entity == null) return false;
            _residentRepo.Delete(entity);
            return await _residentRepo.SaveChangesAsync();
        }
    }

}
