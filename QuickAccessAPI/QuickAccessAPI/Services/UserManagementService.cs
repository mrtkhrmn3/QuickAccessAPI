using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;
using QuickAccessAPI.Repositories;

namespace QuickAccessAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly ISiteManagerRepository _siteManagerRepo;
        private readonly ISecurityRepository _securityRepo;
        private readonly IResidentRepository _residentRepo;
        private readonly IUserRepository _userRepo;

        public UserManagementService(IAdminRepository adminRepo,
                                     ISiteManagerRepository siteManagerRepo,
                                     ISecurityRepository securityRepo,
                                     IResidentRepository residentRepo,
                                     IUserRepository userRepo)
        {
            _adminRepo = adminRepo;
            _siteManagerRepo = siteManagerRepo;
            _securityRepo = securityRepo;
            _residentRepo = residentRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<AdminDTO>> GetAllAdminsAsync()
        {
            var admins = await _adminRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync(); // tüm user'ları al

            var adminDTOs = admins.Select(admin =>
            {
                var user = users.FirstOrDefault(u => u.Id == admin.Id); // eşleşen user'ı bul

                return new AdminDTO
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Username = user?.Username ?? "N/A",
                    Role = user?.Role ?? "Admin" // ya User'dan al ya da sabit olarak ata
                };
            });

            return adminDTOs;
        }
        public async Task<IEnumerable<SiteManagerDTO>> GetAllSiteManagersAsync()
        {
            var sitemanagers = await _siteManagerRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var siteManagerDTOs = sitemanagers.Select(sitemanager =>
            {
                var user = users.FirstOrDefault(u => u.Id == sitemanager.Id);

                return new Dto.SiteManagerDTO { 
                    Id = sitemanager.Id,
                    Name = sitemanager.Name,
                    Surname = sitemanager.Surname,
                    Username = user?.Username ?? "N/A",
                    SiteName = sitemanager.SiteName
                };

            });

            return siteManagerDTOs;
        }

        public async Task<IEnumerable<SecurityDTO>> GetAllSecuritiesAsync()
        {
            var securities = await _securityRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var securityDTOs = securities.Select(security =>
            {
                var user = users.FirstOrDefault(u => u.Id == security.Id);

                return new SecurityDTO
                {
                    Id = security.Id,
                    Name = security.Name,
                    Surname = security.Surname,
                    Username = user?.Username ?? "N/A",
                    SiteName = security.SiteName
                };

            });

            return securityDTOs;
        }

        public async Task<IEnumerable<ResidentDTO>> GetAllResidentsAsync()
        {
            var residents = await _residentRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var residentDTOs = residents.Select(resident =>
            {
                var user = users.FirstOrDefault(u => u.Id == resident.Id);

                return new ResidentDTO
                {
                    Id = resident.Id,
                    Name = resident.Name,
                    Surname = resident.Surname,
                    Username = user?.Username ?? "N/A",
                    Block = resident.Block,
                    AptNo = resident.AptNo,
                    PhoneNumber = resident.PhoneNumber
                };

            });

            return residentDTOs;
        }

        public async Task<IEnumerable<SecurityDTO>> GetSecuritiesForSiteManagerAsync(Guid userId)
        {
            var siteManager = await _siteManagerRepo.GetByIdAsync(userId);
            if (siteManager == null)
                return Enumerable.Empty<SecurityDTO>();

            var securities = await _securityRepo.GetBySiteNameAsync(siteManager.SiteName);
            var users = await _userRepo.GetAllAsync();

            var result = securities.Select(sec =>
            {
                var user = users.FirstOrDefault(u => u.Id == sec.Id);
                return new SecurityDTO
                {
                    Id = sec.Id,
                    Name = sec.Name,
                    Surname = sec.Surname,
                    SiteName = sec.SiteName,
                    Username = user?.Username ?? "N/A"
                };
            });

            return result;
        }

        public async Task<IEnumerable<ResidentDTO>> GetResidentsForSiteManagerAsync(Guid userId)
        {
            var siteManager = await _siteManagerRepo.GetByIdAsync(userId);
            if (siteManager == null)
                return Enumerable.Empty<ResidentDTO>();

            var residents = await _residentRepo.GetBySiteNameAsync(siteManager.SiteName);
            var users = await _userRepo.GetAllAsync();

            var result = residents.Select(res =>
            {
                var user = users.FirstOrDefault(u => u.Id == res.Id);
                return new ResidentDTO
                {
                    Id = res.Id,
                    Name = res.Name,
                    Surname = res.Surname,
                    Block = res.Block,
                    AptNo = res.AptNo,
                    PhoneNumber = res.PhoneNumber,
                    Username = user?.Username ?? "N/A"
                };
            });

            return result;
        }

        public async Task<bool> DeleteAdminAsync(Guid id)
        {
            var entity = await _adminRepo.GetByIdAsync(id);
            if (entity == null) return false;

            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
                _userRepo.Delete(user);

            _adminRepo.Delete(entity);
            return await _adminRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteSiteManagerAsync(Guid id)
        {
            var entity = await _siteManagerRepo.GetByIdAsync(id);
            if (entity == null) return false;

            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
                _userRepo.Delete(user);

            _siteManagerRepo.Delete(entity);
            return await _siteManagerRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteSecurityAsync(Guid id)
        {
            var entity = await _securityRepo.GetByIdAsync(id);
            if (entity == null) return false;

            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
                _userRepo.Delete(user);

            _securityRepo.Delete(entity);
            return await _securityRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteResidentAsync(Guid id)
        {
            var entity = await _residentRepo.GetByIdAsync(id);
            if (entity == null) return false;

            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
                _userRepo.Delete(user);

            _residentRepo.Delete(entity);
            return await _residentRepo.SaveChangesAsync();
        }
    }

}
