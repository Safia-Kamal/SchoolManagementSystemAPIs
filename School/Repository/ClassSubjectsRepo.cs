using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Repository
{
    public class ClassSubjectsRepo:GenericRepo<ClassSubject>
    {
        public ClassSubjectsRepo(SchoolDbContext context) : base(context) { }

        public List<ClassSubject> getAllWithObjs()
        {
            return db.ClassSubjects.Include(s => s.Class).Include(s => s.Subject).ToList();
        }

        public ClassSubject getByIdWithObjs(int id)
        {
            return db.ClassSubjects.Include(s => s.Class).Include(s => s.Subject).FirstOrDefault(s => s.Id == id);
        }

        public List<ClassSubject> getAllBySubId(int id)
        {
            return db.ClassSubjects.Include(s => s.Class).Include(s => s.Subject).Where(s => s.SubjectId == id).ToList();

        }

        public List<ClassSubject> getAllBySubName(string subName)
        {
            return db.ClassSubjects.Include(s => s.Class).Include(s => s.Subject).Where(s => s.Subject.Name == subName).ToList();

        }

        public List<ClassSubject> getAllByClsId(int id)
        {
            return db.ClassSubjects.Include(s => s.Class).Include(s => s.Subject).Where(s => s.ClassId == id).ToList();

        }

        public List<ClassSubject> getAllByClsName(string clsName)
        {
            return db.ClassSubjects.Include(s => s.Class).Include(s => s.Subject).Where(s => s.Class.Name == clsName).ToList();

        }
    }
}

