using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IServices;
using QuickAccessAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegisterController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDTO dto)
    {
        var success = await _registrationService.RegisterAdminAsync(dto);

        if (!success)
            return BadRequest("Username is already taken.");

        return Ok("Admin registered successfully.");
    }


    [HttpPost("sitemanager")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterSiteManager(SiteManagerRegisterDTO dto)
    {
        try
        {
            await _registrationService.RegisterSiteManagerAsync(dto);
            return Ok("SiteManager başarıyla oluşturuldu.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("resident")]
    [Authorize(Roles = "SiteManager")]
    public async Task<IActionResult> RegisterResident(ResidentRegisterDTO dto)
    {
        try
        {
            await _registrationService.RegisterResidentAsync(dto);
            return Ok("Resident başarıyla oluşturuldu.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("security")]
    [Authorize(Roles = "SiteManager")]
    public async Task<IActionResult> RegisterSecurity(SecurityRegisterDTO dto)
    {
        try
        {
            await _registrationService.RegisterSecurityAsync(dto);
            return Ok("Security başarıyla oluşturuldu.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
