namespace DigitalSchedule.Exceptions
{
    public class TeacherAllreadyHasLessonOnThisPareException:Exception
    {
        public TeacherAllreadyHasLessonOnThisPareException()
        { 
        }
        public TeacherAllreadyHasLessonOnThisPareException(string message):base(message)
        { 
        }
    }
}
