namespace DigitalSchedule.Exceptions
{
    public class NothingFoundByFilterException:Exception
    {
        public NothingFoundByFilterException()
        {
        }
        public NothingFoundByFilterException(string massage) : base(massage)
        {
        }
    }
}
