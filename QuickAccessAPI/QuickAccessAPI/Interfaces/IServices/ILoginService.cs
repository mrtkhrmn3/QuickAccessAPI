using QuickAccessAPI.Dto;

namespace QuickAccessAPI.Interfaces.IServices
{
    public interface ILoginService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest);
    }
}
