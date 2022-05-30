namespace DigitalSchedule.Exceptions
{
    public class SubjectAllreadyExist:Exception
    {
        public SubjectAllreadyExist()
        {
        }
        public SubjectAllreadyExist(string massage):base(massage)
        {
        }
    }
}
