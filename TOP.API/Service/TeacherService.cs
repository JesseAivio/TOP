using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public class TeacherService : IModelService<Teacher>
    {
        readonly TOPContext _topContext;

        public TeacherService(TOPContext topContext)
        {
            _topContext = topContext;
        }

        public Teacher Add(Teacher teacherParam)
        {
            var teacher = _topContext.Teachers.FirstOrDefault(x => x.teacher == teacherParam.teacher);

            if (teacher != null)
                return teacher;

            _topContext.Teachers.Add(teacherParam);
            _topContext.SaveChanges();
            return teacherParam;
        }

        public Teacher Get(Guid teacherId)
        {
            var teacher = _topContext.Teachers.FirstOrDefault(x => x.Id == teacherId);

            if (teacher == null)
                return null;

            return teacher;
        }

        public Teacher GetByName(string name)
        {
            var teacher = _topContext.Teachers.FirstOrDefault(x => x.teacher == name);

            if (teacher == null)
                return null;

            return teacher;
        }

        public void Delete(Teacher teacher)
        {
            _topContext.Teachers.Remove(teacher);
            _topContext.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            var dbTeacher = _topContext.Teachers.FirstOrDefault(x => x.Id == teacher.Id);
            dbTeacher.teacher = teacher.teacher;
            _topContext.SaveChanges();
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _topContext.Teachers.ToList();
        }
    }
}
