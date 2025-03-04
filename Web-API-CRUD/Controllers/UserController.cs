using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_CRUD.Models;
using Web_API_CRUD.Services.IServices;

namespace Web_API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            try
            {
                var resultCreateUserService = await _userService.CreateUser(user);
                if (!resultCreateUserService || resultCreateUserService == null) return BadRequest();
                
                return Ok(resultCreateUserService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        [Route("/UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user)
        {
            try
            {
                var resultUpdateUserService = await _userService.UpdateUser(user);
                if (!resultUpdateUserService || resultUpdateUserService == null) return NotFound();
                
                return Ok(resultUpdateUserService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var resultDeleteUserService = await _userService.DeleteUser(id);
                if (!resultDeleteUserService || resultDeleteUserService == null) return NotFound();
                return Ok(resultDeleteUserService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);

            }
        }

        [HttpGet]
        [Route("/GetUsersWithFilters")]
        public async Task<ActionResult<List<User>>> SearchUsersAsync(
            [FromQuery] int? id,
            [FromQuery] string? name,
            [FromQuery] string? company,
            [FromQuery] List<string>? email,
            [FromQuery] string? personalPhone,
            [FromQuery] string? workPhone
            )
        {
            try
            {
                var filters = new User(id ?? 0, name ?? string.Empty, company, email, personalPhone, workPhone);

                var resultSerchUsersService = await _userService.SearchUsersAsync(filters);
                if (resultSerchUsersService.Count == 0) return NotFound();
                
                return Ok(resultSerchUsersService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
