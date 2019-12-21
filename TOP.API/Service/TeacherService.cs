using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.API.Data.Helpers;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public class TeacherService : IModelService<Teacher>
    {
        readonly TOPContext _topContext;
        readonly List<Teacher> _teachers = new List<Teacher>();//Azure testing

        public TeacherService(TOPContext topContext)
        {
            _topContext = topContext;
            if (AppSettings.isAzure)
            {
                AddDefaults();
            }
        }

        private void AddDefaults()
        {
            Teacher teacher = new Teacher()
            {
                Id = Guid.Parse("0d66b512-b2dc-49aa-a4af-01842c15c342"),
                teacher = "test"
            };
            Teacher teacher2 = new Teacher()
            {
                Id = Guid.Parse("4c4be516-9021-4031-a3e6-eb7fc8f2d760"),
                teacher = "test2"
            };
            _teachers.Add(teacher);
            _teachers.Add(teacher2);
        }

        public Teacher Add(Teacher teacherParam)
        {
            Teacher teacher = new Teacher();
            if (AppSettings.isAzure)
            {
                teacher = _teachers.FirstOrDefault(x => x.teacher == teacherParam.teacher);

                if (teacher != null)
                    return teacher;

                teacherParam.Id = new Guid();
                _teachers.Add(teacherParam);
                return teacherParam;
            }
            teacher = _topContext.Teachers.FirstOrDefault(x => x.teacher == teacherParam.teacher);

            if (teacher != null)
                return teacher;

            _topContext.Teachers.Add(teacherParam);
            _topContext.SaveChanges();
            return teacherParam;
        }

        public Teacher Get(Guid teacherId)
        {
            Teacher teacher = new Teacher();
            if (AppSettings.isAzure)
            {
                teacher = _teachers.FirstOrDefault(x => x.Id == teacherId);

                if (teacher == null)
                    return null;

                return teacher;
            }
            teacher = _topContext.Teachers.FirstOrDefault(x => x.Id == teacherId);

            if (teacher == null)
                return null;

            return teacher;
        }

        public Teacher GetByName(string name)
        {
            Teacher teacher = new Teacher();
            if (AppSettings.isAzure)
            {
                teacher = _teachers.FirstOrDefault(x => x.teacher == name);

                if (teacher == null)
                    return null;

                return teacher;
            }
            teacher = _topContext.Teachers.FirstOrDefault(x => x.teacher == name);

            if (teacher == null)
                return null;

            return teacher;
        }

        public void Delete(Teacher teacher)
        {
            if (AppSettings.isAzure)
            {
                _teachers.Remove(teacher);
                return;
            }
            _topContext.Teachers.Remove(teacher);
            _topContext.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            Teacher dbTeacher = new Teacher();
            if (AppSettings.isAzure)
            {
                dbTeacher = _teachers.FirstOrDefault(x => x.Id == teacher.Id);
                dbTeacher.teacher = teacher.teacher;
                return;
            }
            dbTeacher = _topContext.Teachers.FirstOrDefault(x => x.Id == teacher.Id);
            dbTeacher.teacher = teacher.teacher;
            _topContext.SaveChanges();
        }

        public IEnumerable<Teacher> GetAll()
        {
            if (AppSettings.isAzure)
            {
                return _teachers.ToList();
            }
            return _topContext.Teachers.ToList();
        }
    }
}
