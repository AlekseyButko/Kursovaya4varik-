using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSchedule.Exceptions
{
    public class UnccorectSubjectArgumentsException:Exception
    {
        public UnccorectSubjectArgumentsException()
        {
        }
        public UnccorectSubjectArgumentsException(string message):base(message)
        {
        }
    }
}
