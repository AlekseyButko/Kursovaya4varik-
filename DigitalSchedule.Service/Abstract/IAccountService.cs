using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSchedule.Service.Abstract
{
    public interface IAccountService
    {
        public bool RegisterStudent(Student student);
        public User LoginUser(User require);
    }
}
