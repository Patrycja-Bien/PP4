using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Exceptions;

namespace User.Application;

public class LoginService : ILoginService
{
    private readonly JwtTokenService _jwtTokenService;
    public string Login(string username, string password)
    {

        if (username == "admin" && password == "password")
        {
            var roles = new List<string> { "Client", "Employee", "Administrator" };
            var token = _jwtTokenService.GenerateToken(123, roles);
            return token;
        }
        else
        {
            throw new InvalidCredentialsException();
        }
    }
}
