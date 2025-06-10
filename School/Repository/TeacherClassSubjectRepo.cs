using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Repository
{
    public class TeacherClassSubjectRepo:GenericRepo<TeacherClassSubject>
    {
        public TeacherClassSubjectRepo(SchoolDbContext context) : base(context) { }


        public List<TeacherClassSubject> GetAllWithObjs() { 
            return db.TeacherClassSubjects.Include(t=>t.Teacher).Include(t=>t.Class).Include(t=>t.Subject).ToList();
        }

        public TeacherClassSubject getByIdWithObjs(int id) {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).FirstOrDefault(t => t.Id == id);

        }

        public List<TeacherClassSubject> getByTeachId(int teachId) {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).Where(t => t.TeacherId == teachId).ToList();
        }

        public List<TeacherClassSubject> getByClsId(int clsId)
        {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).Where(t => t.ClassId == clsId).ToList();
        }

        public List<TeacherClassSubject> getBySubId(int subId)
        {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).Where(t => t.SubjectId == subId).ToList();
        }

        public List<TeacherClassSubject> getByTeachName(string teachName)
        {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).Where(t => t.Teacher.Name == teachName).ToList();
        }

        public List<TeacherClassSubject> getByClsName(string clsName)
        {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).Where(t => t.Class.Name  == clsName).ToList();
        }
        public List<TeacherClassSubject> getBySubName(string subName)
        {
            return db.TeacherClassSubjects.Include(t => t.Teacher)
                .Include(t => t.Class).Include(t => t.Subject).Where(t => t.Subject.Name == subName).ToList();
        }

    }
}
