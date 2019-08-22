using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using contracted.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace contracted.Repositories
{
  public class AccountRepository
  {
    private readonly IDbConnection _db;
    public AccountRepository(IDbConnection db)
    {
      _db = db;
    }

    public User Register(UserRegistration creds)
    {
      var passwordHash = BCrypt.Net.BCrypt.HashPassword(creds.Password);
      var id = Guid.NewGuid().ToString();
      var user = new User()
      {
        Id = id,
        PasswordHash = passwordHash,
        Email = creds.Email,
        Name = creds.Name
      };
      try
      {
        _db.Execute(@"
      INSERT INTO users (id, email, name, passwordHash, rate)
      VALUES (@Id, @Email, @Name, @PasswordHash, 0);
      ",
          new
          {
            Id = id,
              PasswordHash = passwordHash,
              Email = creds.Email,
              Name = creds.Name
          });
        return user;
      }
      catch (Exception e)
      {
        throw new Exception("EMAIL IN USE");
      }

    }
    public User Login(UserLogin creds)
    {
      User user = _db.Query<User>(@"
                SELECT * FROM users WHERE email = @Email
                ", creds).FirstOrDefault();
      if (user == null) { throw new Exception("Invalid Credentials"); }
      bool validPass = BCrypt.Net.BCrypt.Verify(creds.Password, user.PasswordHash);
      if (!validPass) { throw new Exception("Invalid Credentials"); }
      return user;
    }

    internal User GetUserById(string id)
    {
      var user = _db.Query<User>(@"
      SELECT * FROM users WHERE id = @id
      ", new { id }).FirstOrDefault();
      if (user == null) { throw new Exception("Invalid UserId"); }
      return user;
    }

  }
}