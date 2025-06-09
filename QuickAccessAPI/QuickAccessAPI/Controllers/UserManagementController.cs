using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickAccessAPI.Interfaces.IServices;
using System.Security.Claims;

namespace QuickAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet("Admins")]
        [Authorize (Roles="Admin")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _userManagementService.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("SiteManagers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSiteManagers()
        {
            var siteManagers = await _userManagementService.GetAllSiteManagersAsync();
            return Ok(siteManagers);
        }

        [HttpGet("Securities")]
        [Authorize(Roles = "SiteManager")]
        public async Task<IActionResult> GetSecurities()
        {
            var securities = await _userManagementService.GetAllSecuritiesAsync();
            return Ok(securities);
        }

        [HttpGet("Residents")]
        [Authorize(Roles = "SiteManager")]
        public async Task<IActionResult> GetResidents()
        {
            var residents = await _userManagementService.GetAllResidentsAsync();
            return Ok(residents);
        }

        [HttpGet("ResidentsForSiteManager")]
        [Authorize(Roles = "SiteManager")]
        public async Task<IActionResult> GetResidentsForSiteManager()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized("Invalid user ID");

            var residents = await _userManagementService.GetResidentsForSiteManagerAsync(userId);
            return Ok(residents);
        }

        [HttpGet("SecuritiesForSiteManager")]
        [Authorize(Roles = "SiteManager")]
        public async Task<IActionResult> GetSecuritiesForSiteManager()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized("Invalid user ID");

            var securities = await _userManagementService.GetSecuritiesForSiteManagerAsync(userId);
            return Ok(securities);
        }

        [HttpDelete("Admin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var result = await _userManagementService.DeleteAdminAsync(id);
            if (!result) return NotFound();
            return Ok("Admin deleted.");
        }

        [HttpDelete("SiteManager/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSiteManager(Guid id)
        {
            var result = await _userManagementService.DeleteSiteManagerAsync(id);
            if (!result) return NotFound();
            return Ok("SiteManager deleted.");
        }

        [HttpDelete("Security/{id}")]
        [Authorize(Roles = "SiteManager")]
        public async Task<IActionResult> DeleteSecurity(Guid id)
        {
            var result = await _userManagementService.DeleteSecurityAsync(id);
            if (!result) return NotFound();
            return Ok("Security deleted.");
        }

        [HttpDelete("Resident/{id}")]
        [Authorize(Roles = "SiteManager")]
        public async Task<IActionResult> DeleteResident(Guid id)
        {
            var result = await _userManagementService.DeleteResidentAsync(id);
            if (!result) return NotFound();
            return Ok("Resident deleted.");
        }
    }

}
