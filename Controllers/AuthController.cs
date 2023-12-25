
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AgendaMedica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService){
        _userService = userService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginModel login){
        if(IsValid(login.Email,login.Password)){
            var token = GenerateToken(login.Email);
            return Ok(new {Token = token});
        }
        return Unauthorized();
    }

    private bool IsValid(string email, string password){
        var u = _userService.GetUserByEmail(email);
        if(BCrypt.Net.BCrypt.EnhancedVerify(password,u.Password)){
            return true;
        }
        else return false;
    }

    private string GenerateToken(string username){
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("sjwBearerdddddddddddd");
        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature) 
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class LoginModel{
    public string Email {get;set;}
    public string Password {get;set;}
}