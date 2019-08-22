using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using contracted.Models;
using contracted.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contracted.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly AccountRepository _repo;

    public AccountController(AccountRepository repo)
    {
      _repo = repo;
    }

    //STUB OUT METHODS
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] UserRegistration creds)
    {
      try
      {
        var user = _repo.Register(creds);
        user.SetClaims();
        await HttpContext.SignInAsync(user._principal);
        return Ok();
      }
      catch (Exception)
      {
        return BadRequest("Invalid Credentials");
      }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<User>> Login([FromBody] UserLogin creds)
    {
      try
      {
        User user = _repo.Login(creds);
        user.SetClaims();
        await HttpContext.SignInAsync(user._principal);
        return Ok(user);
      }
      catch (Exception e)
      {
        return Unauthorized(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("Logout")]
    public async Task<ActionResult<bool>> Logout()
    {
      try
      {
        await HttpContext.SignOutAsync();
        return Ok(true);
      }
      catch (Exception e)
      {
        return Unauthorized(e.Message);
      }
    }

    [Authorize] // Only Authenticated users will be allowed into this method
    [HttpGet("Authenticate")]
    public async Task<ActionResult<User>> Authenticate()
    {
      try
      {
        //REVIEW GET THE ID OF THE LOGGED IN USER
        var id = HttpContext.User.FindFirstValue("Id");
        var user = _repo.GetUserById(id);
        if (user == null)
        {
          await HttpContext.SignOutAsync();
          throw new Exception("User not logged In");
        }
        return Ok(user);
      }
      catch (Exception e)
      {
        return Unauthorized(e.Message);
      }
    }
  }
}