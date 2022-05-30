using DigitalSchedule.Exceptions;
using DigitalSchedule.Service.Abstract;

namespace DigitalSchedule.Service;

public class AccountService: IAccountService
{
    public AccountService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork unitOfWork;

    public bool RegisterStudent(Student student)
    {
        if (unitOfWork.Users.Find(s => s.Login == student.Login) is not null)
            throw new UserAllreadyExistsException("User " + student.Login + " already excist with Id, " + student.Id);

        unitOfWork.Students.Add(student);
        unitOfWork.SaveChanges();
        return true;
    }

    public User LoginUser(User require)
    { 
        var user = unitOfWork.Users.Find(x => x.Login.Equals(require.Login)&&x.Password.Equals(require.Password));
        if (user is null) throw new UserNotFoundException("Wrong login or password\n");

        switch (user.Role)
        {
            case Domain.Enum.Role.Teacher:
                var teacher = unitOfWork.Teachers.Find(t => t.Login.Equals(user.Login));
                if (teacher is null) throw new UserNotFoundException("Wrong login\n");
                return teacher;

            case Domain.Enum.Role.Student:
                var student = unitOfWork.Students.Find(x => x.Login.Equals(user.Login));
                if (student is null) throw new UserNotFoundException("Wrong login\n");
                return student;

            default:
                return user;
        }
    }
}
