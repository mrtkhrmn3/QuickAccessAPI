using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public LoginService(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
            if (user == null)
                return null;

            if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.Password))
                return null;

            var token = _tokenService.GenerateToken(user);

            return new LoginResponseDTO
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
