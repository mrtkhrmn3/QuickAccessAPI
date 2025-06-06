using QuickAccessAPI.Dto;
using QuickAccessAPI.Entities;
using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Admin> _adminRepository;
        private readonly IGenericRepository<SiteManager> _siteManagerRepository;
        private readonly IGenericRepository<Resident> _residentRepository;
        private readonly IGenericRepository<Security> _securityRepository;

        public RegistrationService(
            IGenericRepository<User> userRepository,
            IGenericRepository<Admin> adminRepository,
            IGenericRepository<SiteManager> siteManagerRepository,
            IGenericRepository<Resident> residentRepository,
            IGenericRepository<Security> securityRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _siteManagerRepository = siteManagerRepository;
            _residentRepository = residentRepository;
            _securityRepository = securityRepository;
        }

        public async Task<bool> RegisterAdminAsync(AdminRegisterDTO dto)
        {
            if ((await _userRepository.GetAllAsync()).Any(u => u.Username == dto.Username))
                return false;

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password),
                Role = "Admin"
            };

            var admin = new Admin
            {
                Id = userId,
                Name = dto.Name,
                Surname = dto.Surname
            };

            await _userRepository.AddAsync(user);
            await _adminRepository.AddAsync(admin);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> RegisterSiteManagerAsync(SiteManagerRegisterDTO dto)
        {
            if ((await _userRepository.GetAllAsync()).Any(u => u.Username == dto.Username))
                throw new Exception("Kullanıcı adı zaten mevcut.");

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password),
                Role = "SiteManager"
            };

            var siteManager = new SiteManager
            {
                Id = userId,
                Name = dto.Name,
                Surname = dto.Surname,
                SiteName = dto.SiteName
            };

            await _userRepository.AddAsync(user);
            await _siteManagerRepository.AddAsync(siteManager);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> RegisterResidentAsync(ResidentRegisterDTO dto)
        {
            if ((await _userRepository.GetAllAsync()).Any(u => u.Username == dto.Username))
                throw new Exception("Kullanıcı adı zaten mevcut.");

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password),
                Role = "Resident"
            };

            var resident = new Resident
            {
                Id = userId,
                Name = dto.Name,
                Surname = dto.Surname,
                Block = dto.Block,
                AptNo = dto.AptNo,
                PhoneNumber = dto.PhoneNumber,
                SiteName = dto.SiteName
            };

            await _userRepository.AddAsync(user);
            await _residentRepository.AddAsync(resident);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> RegisterSecurityAsync(SecurityRegisterDTO dto)
        {
            if ((await _userRepository.GetAllAsync()).Any(u => u.Username == dto.Username))
                throw new Exception("Kullanıcı adı zaten mevcut.");

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password),
                Role = "Security"
            };

            var security = new Security
            {
                Id = userId,
                Name = dto.Name,
                Surname = dto.Surname,
                SiteName = dto.SiteName
            };

            await _userRepository.AddAsync(user);
            await _securityRepository.AddAsync(security);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
