namespace DigitalSchedule.Exceptions;

public class UserAllreadyExistsException : Exception
{
    public UserAllreadyExistsException()
    {
    }

    public UserAllreadyExistsException(string message) : base(message)
    {
    }
}
