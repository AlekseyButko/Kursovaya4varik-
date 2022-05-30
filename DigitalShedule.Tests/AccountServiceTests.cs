using Moq;
using Xunit;
using DigitalSchedule.Domain.Entity;
using DigitalSchedule.Domain.Enum;
using DigitalSchedule.Service.Abstract;
using DigitalSchedule.Exceptions;

namespace DigitalShedule.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void LoginUserTest()
        {
            var mock = new Mock<IAccountService>();
            
            mock.Setup(mock =>
                mock.LoginUser(new User { Login = "first" })).Returns(new User { Login = "first", Role = Role.Teacher });

            var accountServices = mock.Object;
            var result = accountServices.LoginUser(new User { Login = "first" });

            Assert.Equal(Role.Teacher, result.Role);
        }
        [Fact]
        public void LoginUser_UserNotExists_AddsUser()
        {
            List<Student> students = new List<Student>();
            Student student = new Student { Login = "second" };
            var mock = new Mock<IAccountService>();
            mock.Setup(mock =>
                mock.RegisterStudent(new Student { Login = "second" })).Returns(CheckRegistration(student, students));
            var accountServices = mock.Object;
            var result = accountServices.RegisterStudent(new Student { Login = "second" });

            Assert.True(result);
        }
        [Fact]
        public void LoginUser_UserAllreadyExists_ThrowsUserAllreadyExistsException()
        {
            List<Student> students = new List<Student>();
            Student student = new Student { Login = "first" };

            var mock = new Mock<IAccountService>();

            Assert.Throws<UserAllreadyExistsException>(() => mock.Setup(mock =>
                mock.RegisterStudent(new Student { Login = "first" })).Returns(CheckRegistration(student, students)));
        }
        private bool CheckRegistration(Student student, List<Student> students)
        {
            students.Add(new Student { Login = "first" });
            if (students.Find(s => s.Login == student.Login) is not null)
                throw new UserAllreadyExistsException();

            students.Add(student);
            return true;
        }
    }
}
