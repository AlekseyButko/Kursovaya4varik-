using DigitalSchedule.Domain.Model;
using DigitalSchedule.Exceptions;
using DigitalSchedule.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using DigitalSchedule.Mapper;
using DigitalSchedule.Domain.Entity;

namespace DigitalSchedule.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpPost]
    [Route("addSubject")]
    public IActionResult AddSubject(SubjectModel subject)
    {
        if (UserSession.User is null)
            return BadRequest("Unregistered user");

        if (UserSession.User.Role != Domain.Enum.Role.Admin &&
            UserSession.User.Role != Domain.Enum.Role.Editor)
        {
            return BadRequest("Not enough rights");
        }

        try
        {
            _scheduleService.AddSubject(subject.ToDomain());
        }
        catch (SubjectAllreadyExist ex)
        {
            return BadRequest(ex.Message);            
        }
        catch (TeacherAllreadyHasLessonOnThisPareException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UnccorectSubjectArgumentsException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPost]
    [Route("editSubject")]
    public IActionResult EditSubject(Subject subject)
    {
        if (UserSession.User is null)
            return BadRequest("Unregistered user");

        if (UserSession.User.Role != Domain.Enum.Role.Admin &&
            UserSession.User.Role != Domain.Enum.Role.Editor)
        {
            return BadRequest("Not enough rights");
        }

        try
        {
            _scheduleService.EditSubject(subject);
        }
        catch (SubjectNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (TeacherAllreadyHasLessonOnThisPareException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpGet]
    [Route("getAll")]
    public IActionResult GetAll()
    {
        if (UserSession.User is null)
            return BadRequest("Unregistered user");

        if (UserSession.User.Role != Domain.Enum.Role.Admin &&
            UserSession.User.Role != Domain.Enum.Role.Editor)
        {
            return BadRequest("Not enough rights");
        }

        try
        {
            return Ok(_scheduleService.GetAllSubjects(UserSession.User));
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("getForStudent")]
    public IActionResult GetForStudent()
    {
        if (UserSession.User is null)
            return BadRequest("Unregistered user");

        if (UserSession.User.Role != Domain.Enum.Role.Student)
            return BadRequest("Not a student");

        try
        {
            return Ok(_scheduleService.GetForStudent((Student)UserSession.User));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("getByFilter")]
    public IActionResult GetByFilter(Subject subject)
    {
        if (UserSession.User is null)
            return BadRequest("Unregistered user");

        if (UserSession.User.Role != Domain.Enum.Role.Editor &&
            UserSession.User.Role != Domain.Enum.Role.Admin)
            return BadRequest("Not enough rights");

        try
        {
            return Ok(_scheduleService.GetByFilter(subject));
        }
        catch (NothingFoundByFilterException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
