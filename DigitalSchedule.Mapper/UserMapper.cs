using DigitalSchedule.Domain.Entity;
using DigitalSchedule.Domain.Model;

namespace DigitalSchedule.Mapper;

public static class UserMapper
{
    public static User ToDomain(this UserLoginModel user)
    {
        return new User
        {
            Login = user.Login,
            Password = user.Password,
        };
    }
}