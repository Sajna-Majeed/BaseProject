using Core.DTOs;
using Core.Interfaces;
using Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var id = await _service.CreateUserAsync(dto);
            return Ok(ApiResponse<int>.Ok(id));
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            await _service.UpdateUserAsync(dto);
            return Ok(ApiResponse<string>.Ok("User updated"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteUserAsync(id);
            return Ok(ApiResponse<string>.Ok("User deleted"));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _service.GetUsersAsync();
            return Ok(ApiResponse<object>.Ok(users));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var users = await _service.GetUsersByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(users));
        }
    }

}
