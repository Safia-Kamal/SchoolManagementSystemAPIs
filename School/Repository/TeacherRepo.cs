using School.Models;

namespace School.Repository
{
    public class TeacherRepo:GenericRepo<Teacher>
    {
        public TeacherRepo(SchoolDbContext db):base(db) { }

        public Teacher getByName(string name) { 
            return db.Teachers.FirstOrDefault(t=>t.Name == name);
        }

        public int numOfTeacherClasses(int teachId)
        {
            return db.TeacherClassSubjects.Count(t => t.TeacherId == teachId);
        }


        //public List<TeacherClassSubject> numOfStdInTeacherClasses(int teachId)
        public int numOfStdInTeacherClasses(int teachId)
        {
            //return db.TeacherClassSubjects.Where(t=>t.TeacherId == teachId).ToList();
            var cls= db.TeacherClassSubjects.Where(t=>t.TeacherId == teachId).ToList();
            int count=0;
            foreach(var t in cls)
            {
                var clsId = t.ClassId;
                count += db.Students.Count(s => s.ClassId == clsId);
            }

            return count;

        }
    }
}
