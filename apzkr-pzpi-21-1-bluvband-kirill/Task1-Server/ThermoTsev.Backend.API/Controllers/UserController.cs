using Microsoft.AspNetCore.Mvc;
using ThermoTsev.Backend.API.Attributes;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.User;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController(IUserService userService, IJwtService jwtService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
    {
        User? user = await userService.LoginAsync(loginDto.Email, loginDto.Password);

        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }
        
        string token = jwtService.GenerateToken(user.Id, user.Role);
        return Ok(token);
    }
    
    [HttpPost("SignUp")]
    public async Task<ActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        try
        {
            bool result = await userService.SignUpAsync(signUpDto);

            if (result)
            {
                return Ok("User registration successful");
            }

            return BadRequest("User with the same email already exists");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    
    [AdminRoleAuthorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        IEnumerable<User?> users = await userService.GetUsersAsync();
        return Ok(users);
    }

    [AdminRoleAuthorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        User? user = await userService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [AdminRoleAuthorize]
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto user)
    {
        try
        {
            await userService.CreateUserAsync(user);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("User already exists");
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AdminRoleAuthorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto user)
    {
        try
        {
            await userService.UpdateUserAsync(id, user);
            return NoContent();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AdminRoleAuthorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}
