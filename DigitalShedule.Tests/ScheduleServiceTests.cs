using Moq;
using Xunit;
using DigitalSchedule.Domain.Entity;
using DigitalSchedule.Domain.Enum;
using DigitalSchedule.Service.Abstract;
using DigitalSchedule.Exceptions;
using System.Text.Json;

namespace DigitalShedule.Tests
{
    public class ScheduleServiceTests
    {
        private List<Subject> _subjectList = new List<Subject>{new Subject{
            AudienceNumber=1,
            DayOfWeek=DayOfWeek.Monday,
            Group="firstgroup", 
            Id=1,
            Name="firstSubName",
            Order=SubjectOrder.First,
            Subgroup=StudySubgroup.First,
            TeacherName="firstTeacher",
            Week=Week.First}
        };

        Subject _exsistingSubject = new Subject
        {
            AudienceNumber = 1,
            DayOfWeek = DayOfWeek.Monday,
            Group = "firstgroup",
            Id = 1,
            Name = "firstSubName",
            Order = SubjectOrder.First,
            Subgroup = StudySubgroup.First,
            TeacherName = "firstTeacher",
            Week = Week.First
        };

        Subject _notExsistingSubject = new Subject
        {
            AudienceNumber = 2,
            DayOfWeek = DayOfWeek.Monday,
            Group = "seconfGroup",
            Id = 2,
            Name = "secondGroup",
            Order = SubjectOrder.Second,
            Subgroup = StudySubgroup.Second,
            TeacherName = "SomeTeacher",
            Week = Week.First
        };

        Subject _subjectForEdit = new Subject
        {
            AudienceNumber = 1,
            DayOfWeek = DayOfWeek.Monday,
            Group = "thirdGoup",
            Id = 1,
            Name = "thirdGoup",
            Order = SubjectOrder.Third,
            Subgroup = StudySubgroup.Union,
            TeacherName = "SomeTeacher",
            Week = Week.First
        };

        [Fact]
        public void AddSubject_Success_Test()
        {
            var mock = new Mock<IScheduleService>();
            mock.Setup(mock =>
            mock.AddSubject(_notExsistingSubject)).Returns(AddSubjectTest(_notExsistingSubject));

            var shedulService = mock.Object;
            var result = shedulService.AddSubject(_notExsistingSubject);

            Assert.True(result);
        }
        [Fact]
        public void AddSubject_ThrowsSubjectAllreadyExistException_Test()
        {
            var mock = new Mock<IScheduleService>();

            Assert.Throws<SubjectAllreadyExist>(() => mock.Setup(mock =>
            mock.AddSubject(_exsistingSubject)).Returns(AddSubjectTest(_exsistingSubject)));
        }
        [Fact]
        public void AddSubject_ThrowsTeacherAllreadyHasLessonOnThisPareException_Test()
        {
            var tmpSubject = _exsistingSubject with { Id = 2 };
            var mock = new Mock<IScheduleService>();

            Assert.Throws<TeacherAllreadyHasLessonOnThisPareException>(() => mock.Setup(mock =>
            mock.AddSubject(tmpSubject)).Returns(AddSubjectTest(tmpSubject)));
        }
        [Fact]
        public void EditSubject_Success_Test()
        {
            var mock = new Mock<IScheduleService>();
            mock.Setup(mock =>
            mock.EditSubject(_subjectForEdit)).Returns(EditSubject(_subjectForEdit));

            var shedulService = mock.Object;
            var result = shedulService.EditSubject(_subjectForEdit);

            Assert.True(result);
        }
        [Fact]
        public void EditSubject_ThrowsTeacherAllreadyHasLessonOnThisPareException_Test()
        {        
            var subject = _subjectForEdit with
            {
                AudienceNumber = 1,
                DayOfWeek = DayOfWeek.Monday,
                Group = "firstgroup",
                Name = "firstSubName",
                Order = SubjectOrder.First,
                Subgroup = StudySubgroup.First,
            };

            var mock = new Mock<IScheduleService>();
            Assert.Throws<TeacherAllreadyHasLessonOnThisPareException>(()=> mock.Setup(mock =>
            mock.EditSubject(subject)).Returns(EditSubject(subject)));
        }
        [Fact]
        public void EditSubject_ThrowsSubjectNotFoundException_Test()
        {
            var subject = _subjectForEdit with{Id = 2};
            var mock = new Mock<IScheduleService>();
            Assert.Throws<SubjectNotFoundException>(()=> mock.Setup(mock =>
            mock.EditSubject(subject)).Returns(EditSubject(subject)));
        }
        [Fact]
        public void FilterSubjects_Success_Test()
        {
            var testSubject = new Subject { Name = "firstSubName", Id=1 };

            string expectResult = JsonSerializer.Serialize(_subjectList[0]);
            var mock = new Mock<IScheduleService>();
            mock.Setup(mock =>
            mock.GetByFilter(testSubject)).Returns(FilterSubject(testSubject));

            var shedulService = mock.Object;
            var result = shedulService.GetByFilter(testSubject);

            Assert.True(expectResult.Equals(result));
        }
        [Fact]
        public void FilterSubjects_ThrowsNothingFoundByFilterException_Test()
        {
            var testSubject = new Subject { Id = 3 };

            var mock = new Mock<IScheduleService>();
           Assert.Throws<NothingFoundByFilterException>(()=> mock.Setup(mock =>
            mock.GetByFilter(testSubject)).Returns(FilterSubject(testSubject)));
        }
        private bool AddSubjectTest(Subject subject)
        {
            if (_subjectList.Find(s => s.Id == subject.Id) is not null)
                throw new SubjectAllreadyExist("User " + subject.Name + " already excist with Id, " + subject.Id);

            if (CheckAudienceTeacherAndGroupWithSubgroupOnDateAndPareOrder_Test(subject))
                return true;

            return false;
        }
        public bool EditSubject(Subject subject)
        {
            var subToEdit = _subjectList.Find(s => s.Id == subject.Id);
            if (subToEdit is null)
                throw new SubjectNotFoundException("subject " + subject.Name + " dont excist with Id, " + subject.Id);

            if (!CheckAudienceTeacherAndGroupWithSubgroupOnDateAndPareOrder_Test(subject))
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

            return true;
        }
        private string FilterSubject(Subject subject)
        {
            List<Subject> filteredSubjects = _subjectList;
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

            string tmp = string.Empty;
            foreach (var filterSub in filteredSubjects)
                tmp += JsonSerializer.Serialize(filterSub);

            return tmp;
        }
        private bool CheckAudienceTeacherAndGroupWithSubgroupOnDateAndPareOrder_Test(Subject subject)
        {
            if (_subjectList.Find(s =>
            (s.TeacherName == subject.TeacherName ||
            s.AudienceNumber == subject.AudienceNumber ||
            (s.Group == subject.Group
            && s.Subgroup == subject.Subgroup)) &&
            s.DayOfWeek == subject.DayOfWeek &&
            s.Order == subject.Order)
                is not null)
            {
                throw new TeacherAllreadyHasLessonOnThisPareException("Teacher " + subject.TeacherName +
                "allready has lesson on " + subject.DayOfWeek.ToString() +
                " on " + subject.Order.ToString() + " pare" +
                " in audience " + subject.AudienceNumber);
            }
            return true;
        }
    }
}
