using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userService.CreateUserAsync(userDto);
            if (!created)
                return BadRequest("User creation failed.");

            return Ok("User registered successfully.");
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            var success = await _userService.CreateUserAsync(userDTO);
            if (!success) return BadRequest();

            var response = new RegisterResponseDTO
            {
                Id = Guid.NewGuid(),
                Username = userDTO.Username,
                Role = userDTO.Role
            };
            return Ok(("User registered successfully.")+response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id) return BadRequest();
            var success = await _userService.UpdateUserAsync(userDto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
