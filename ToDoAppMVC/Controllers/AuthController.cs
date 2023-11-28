using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoAppMVC.Models;
using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TodoDBContext _context;
    private readonly IConfiguration _config;

    public AuthController(TodoDBContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User
        {
            Name = model.Name,
            Password = model.Password,
            Username = model.Username
        };

        _context.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user.Id);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Username == model.Username && user.Password == model.Password);
        
        // Get the roles aswell

        if (user == null)
        {
            return Unauthorized("Invalid credetials");
        }

        var token = GenerateToken(user);

        return Ok(token);
    }
    
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Username),
            new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);
            
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}