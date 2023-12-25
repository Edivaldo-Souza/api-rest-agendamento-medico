using AgendaMedica.Dto;
using AgendaMedica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaMedica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase{
    private readonly ApplicationDbContext _context;
    private readonly UserService _service;

    public UserController(ApplicationDbContext applicationDbContext, UserService userService)
    {
        _context = applicationDbContext;
        _service = userService;
    }

    [HttpGet]
    [Authorize("TokenRequired")]
    public async Task<ActionResult<IEnumerable<User>>> Get(){
        return await _context.users.ToListAsync();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<User>> Post(UserModel user)
    {
        User user1 = new User(user.Name,user.Cpf,user.Name,user.Password,user.Vocation);
        user1.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password,8);
        user1.ProfilePicture = Convert.FromBase64String(user.ProfilePicture);

        if(user.Name==null || user.Cpf==null || user.Email==null || user.Vocation==null)
        {
            return BadRequest();
        }
        UserDTO userDTO = new UserDTO(user.Name,user.Cpf, user.Email, user.Vocation);
        _context.users.Add(user1);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new {id=user1.Id},userDTO);
    }  
}
public class UserModel{
    public string? Name {get;set;}
    public string? Cpf {get;set;}
    public string? Email {get;set;}
    public string? Password {get;set;}
    public string? Vocation {get;set;}
    public string? ProfilePicture {get;set;}
}

public class PictureRequest
{
    public string? Email {get;set;}
    public string? Picture {get;set;}
}