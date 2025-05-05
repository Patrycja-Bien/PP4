using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using User.Application;
using User.Domain.Exceptions;
using User.Domain.Models;
using User.Domain.Requests;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    public LoginService _loginService;

    [HttpPost]
    public IActionResult Login([FromBody] User.Domain.Requests.LoginRequest request)
    {
        try
        {
            var token = _loginService.Login(request.Username, request.Password);
            return Ok(new { token });
        }
        catch (InvalidCredentialsException)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    [Authorize]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult AdminPage()
    {
        return Ok();
    }
}
