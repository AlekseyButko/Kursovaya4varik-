using Newtonsoft.Json.Linq;
namespace DigitalSchedule.Service.Abstract;

public interface IScheduleService
{
    public string GetForTeacher(Teacher teacher);
    public string GetForStudent(Student student);
    public bool EditSubject(Subject subject);
    public bool AddSubject(Subject subject);
    public string GetAllSubjects(User user);
    public string GetByFilter(Subject subject);
}
