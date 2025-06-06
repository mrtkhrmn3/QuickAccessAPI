using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ISiteManagerRepository _siteManagerRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly ISecurityRepository _securityRepository;

        public UserService(
            IUserRepository userRepository,
            IAdminRepository adminRepository,
            ISiteManagerRepository siteManagerRepository,
            IResidentRepository residentRepository,
            ISecurityRepository securityRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _siteManagerRepository = siteManagerRepository;
            _residentRepository = residentRepository;
            _securityRepository = securityRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role
            });
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }

        public async Task<UserDTO?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }

        public async Task<bool> CreateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                Password = userDto.Password,
                Role = userDto.Role
            };

            await _userRepository.AddAsync(user);

            // Rol bazlı ilgili tabloya da ekle
            switch (user.Role)
            {
                case "Admin":
                    var admin = new Admin
                    {
                        Id = user.Id,
                        Name = userDto.Name,
                        Surname = userDto.Surname
                    };
                    await _adminRepository.AddAsync(admin);
                    break;

                case "SiteManager":
                    var siteManager = new SiteManager
                    {
                        Id = user.Id,
                        Name = userDto.Name,
                        Surname = userDto.Surname,
                        SiteName = userDto.SiteName!
                    };
                    await _siteManagerRepository.AddAsync(siteManager);
                    break;

                case "Resident":
                    var resident = new Resident
                    {
                        Id = user.Id,
                        Name = userDto.Name,
                        Surname = userDto.Surname,
                        Block = userDto.Block!,
                        AptNo = userDto.AptNo!.Value,
                        PhoneNumber = userDto.PhoneNumber!,
                        SiteName = userDto.SiteName!
                    };
                    await _residentRepository.AddAsync(resident);
                    break;

                case "Security":
                    var security = new Security
                    {
                        Id = user.Id,
                        Name = userDto.Name,
                        Surname = userDto.Surname,
                        SiteName = userDto.SiteName!
                    };
                    await _securityRepository.AddAsync(security);
                    break;

                default:
                    throw new InvalidOperationException("Unsupported user role.");
            }

            return true;
        }


        public async Task<bool> UpdateUserAsync(UserDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null) return false;

            user.Username = userDto.Username;
            user.Role = userDto.Role;

            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
