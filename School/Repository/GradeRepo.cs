using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Repository
{
    public class GradeRepo:GenericRepo<Grade>
    {
        public GradeRepo(SchoolDbContext context) : base(context) { }

        public Grade getWithSubAndStud(int id)

        {
            return db.Grades.Include(g => g.Subject).Include(g => g.Student).FirstOrDefault(g=>g.Id == id);
        }

        public List<Grade> getAllWithSubAndStud()
        {
            return db.Grades.Include(g => g.Subject).Include(g => g.Student).ToList();
        }

        //------------------------------------------

        public List<Grade> gradesByStdId(int stdId)
        {
            return db.Grades.Include(g => g.Subject).Include(g => g.Student).Where(g => g.StudentId == stdId).ToList();
        }

        public List<Grade> gradesByStdName(string stdName)
        {
            return db.Grades.Include(g => g.Subject).Include(g => g.Student).Where(g => g.Student.Name == stdName).ToList();
        }
        public List<Grade> gradesBySubId(int subId)
        {
            return db.Grades.Include(g => g.Subject).Include(g => g.Student).Where(g => g.SubjectId == subId).ToList();
        }

        public List<Grade> gradesBySubName(string subName)
        {
            return db.Grades.Include(g => g.Subject).Include(g => g.Student).Where(g => g.Subject.Name == subName).ToList();
        }

        public List<Grade> getByStdNationalId(string nationalId) {
            return db.Grades.Include(g => g.Student).Include(g => g.Subject).Where(g => g.Student.NationalId == nationalId).ToList();
        }

    }
}
