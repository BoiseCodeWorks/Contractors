using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace contracted.Models
{

  //HELPER MODELS
  public class UserLogin
  {
    [EmailAddress]
    public string Email { get; set; }

    [MinLength(6)]
    public string Password { get; set; }
  }

  public class UserRegistration : UserLogin
  {
    [Required]
    public string Name { get; set; }
  }

  public class User
  {
    public string Id { get; set; }
    public string Email { get; set; }
    internal string PasswordHash { get; set; }
    public string Name { get; set; }
    internal ClaimsPrincipal _principal { get; private set; }

    internal void SetClaims()
    {
      var claims = new List<Claim>
      {
        new Claim("Id", Id), //req.session.uid = id
        new Claim(ClaimTypes.Email, Email)
      };
      var userIdentity = new ClaimsIdentity(claims, "login");
      _principal = new ClaimsPrincipal(userIdentity);
    }

  }
}