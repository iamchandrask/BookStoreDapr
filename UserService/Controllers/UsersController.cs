using System;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using Microsoft.AspNetCore.Mvc;
using UserService.Application;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceApp _userService;

        public UsersController(IUserServiceApp userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            var created = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}