using DigitalSchedule.Domain.Entity;
using DigitalSchedule.Domain.Model;
using DigitalSchedule.Exceptions;
using DigitalSchedule.Mapper;
using DigitalSchedule.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSchedule.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(Student student)
    {
        try
        {
            accountService.RegisterStudent(student);
            return Ok();
        }
        catch (UserAllreadyExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(UserLoginModel user)
    {
        if (UserSession.User != null)
            return BadRequest("AllreadyLoggedIn");

        try
        {
            User u = accountService.LoginUser(user.ToDomain());
            UserSession.User = u;

            return Ok();
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        if (UserSession.User == null)
            return BadRequest("AllreadyLoggedOut");

        UserSession.User = null;
        return Ok();
    }
}
