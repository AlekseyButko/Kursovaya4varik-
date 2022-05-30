using DigitalSchedule.Service.Abstract;
using DigitalSchedule.Exceptions;
using System.Text.Json;
using DigitalSchedule.Domain.Enum;

namespace DigitalSchedule.Service;

public class ScheduleService : IScheduleService
{
    public ScheduleService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork unitOfWork;
    private bool CheckAudienceTeacherAndGroupWithSubgroupOnDateAndPareOrder(Subject subject)
    {
        if (unitOfWork.Subjects.Find(s =>
        (s.TeacherName == subject.TeacherName ||
        s.AudienceNumber == subject.AudienceNumber ||
        (s.Group == subject.Group
        && s.Subgroup == subject.Subgroup)) &&
        s.DayOfWeek == subject.DayOfWeek &&
        s.Order == subject.Order)
            is not null)
        {
            return true;
        }

        return false;
    }

    public bool AddSubject(Subject subject)
    {
        if (unitOfWork.Subjects.Find(s => s.Id == subject.Id) is not null)
            throw new SubjectAllreadyExist("subject " + subject.Name + " already excist with Id, " + subject.Id);

        if (unitOfWork.Teachers.Find(s => s.Name == subject.TeacherName) is null)
            throw new UserNotFoundException("Teacher with name" + subject.TeacherName + " doesn't exist\n");

        if (!Enum.IsDefined(typeof(Week), subject.Week) || subject.Week == Week.Undefined)
            throw new UnccorectSubjectArgumentsException("Week " + subject.Week + " is not defined\n");
       
        if(!Enum.IsDefined(typeof(DayOfWeek),subject.DayOfWeek))
            throw new UnccorectSubjectArgumentsException("Day of week " + subject.DayOfWeek + " is not defined\n");
        
        if (!Enum.IsDefined(typeof(SubjectOrder), subject.Order) || subject.Order == SubjectOrder.Undefined)
            throw new UnccorectSubjectArgumentsException("Subject order " + subject.Order + " is not defined\n");

        if (!Enum.IsDefined(typeof(StudySubgroup), subject.Subgroup) || subject.Subgroup == StudySubgroup.Undefined)
            throw new UnccorectSubjectArgumentsException("Subgroup " + subject.Subgroup + " is not defined\n");

        if (CheckAudienceTeacherAndGroupWithSubgroupOnDateAndPareOrder(subject))
        {
            throw new TeacherAllreadyHasLessonOnThisPareException("Teacher " + subject.TeacherName +
                "allready has lesson on " + subject.DayOfWeek.ToString() +
                " on " + subject.Order.ToString() + " pare" +
                " in audience " + subject.AudienceNumber);
        }

        unitOfWork.Subjects.Add(subject);
        unitOfWork.SaveChanges();
        return true;
    }

    public bool EditSubject(Subject subject)
    {
        var subToEdit = unitOfWork.Subjects.Find(s => s.Id == subject.Id);
        if (subToEdit is null)
            throw new SubjectNotFoundException("subject " + subject.Name + " dont excist with Id, " + subject.Id);

        if (CheckAudienceTeacherAndGroupWithSubgroupOnDateAndPareOrder(subject))
        {
            throw new TeacherAllreadyHasLessonOnThisPareException(
                "Teacher " + subject.TeacherName +
                "allready has lesson on " + subject.DayOfWeek.ToString() +
                " on " + subject.Order.ToString() + " pare" +
                " in audience " + subject.AudienceNumber +
                " with group" + subject.Group +
                "(subgroup " + subject.Subgroup +
                "), subject is " + subject.Name);
        }

        subToEdit.Name = subject.Name;
        subToEdit.Subgroup = subject.Subgroup;
        subToEdit.Group = subject.Group;
        subToEdit.Order = subject.Order;
        subToEdit.DayOfWeek = subject.DayOfWeek;
        subToEdit.Week = subject.Week;
        subToEdit.Id = subject.Id;
        subToEdit.AudienceNumber = subject.AudienceNumber;
        subToEdit.TeacherName = subject.TeacherName;

        unitOfWork.SaveChanges();
        return true;
    }

    public string GetAllSubjects(User user)
    {
        if (unitOfWork.Users.Find(s => s.Login == user.Login) is null)
            throw new UserNotFoundException("Wrong login\n");

        return JsonSerializer.Serialize(unitOfWork.Subjects.GetAll());
    }

    public string GetForStudent(Student student)
    {
        return JsonSerializer.Serialize(unitOfWork.Subjects.GetAll().Where(s => s.Group == student.Group));
    }

    public string GetForTeacher(Teacher teacher)
    {
        return JsonSerializer.Serialize(unitOfWork.Subjects.GetAll().Where(s => s.TeacherName == teacher.Login));
    }
    public string GetByFilter(Subject subject)
    {
        List<Subject> filteredSubjects = unitOfWork.Subjects.GetAll().ToList();
        if (subject.Name != "")
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.Name == subject.Name))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.Subgroup != 0)
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.Subgroup == subject.Subgroup))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.Group != "")
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.Group == subject.Group))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.Order != 0)
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.Order == subject.Order))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.DayOfWeek != 0)
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.DayOfWeek == subject.DayOfWeek))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.Week != 0)
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.Week == subject.Week))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.Id != 0)
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.Id == subject.Id))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.AudienceNumber != 0)
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.AudienceNumber == subject.AudienceNumber))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (subject.TeacherName != "")
        {
            List<Subject> tmpSubjects = new List<Subject>();
            foreach (var filterSub in filteredSubjects.Where(s => s.TeacherName == subject.TeacherName))
            {
                tmpSubjects.Add(filterSub);
            }
            filteredSubjects.Clear();
            filteredSubjects.AddRange(tmpSubjects);
        }

        if (filteredSubjects.Count == 0)
            throw new NothingFoundByFilterException("Nothing found by your filter");

        return JsonSerializer.Serialize(filteredSubjects);
    }
}
