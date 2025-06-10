using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Repository
{
    public class TimetableRepo:GenericRepo<Timetable>
    {
        public TimetableRepo(SchoolDbContext context) : base(context) { }

        public List<Timetable> getAllWithSubAndTeacher()
        {
            return db.Timetables.Include(t=>t.Subject)
                .Include(t=>t.Teacher).Include(t=>t.Class).ToList();
        }

        public Timetable getByIdWithSubAndTeacher(int id)
        {
            return db.Timetables.Include(t => t.Subject)
                .Include(t => t.Teacher).Include(t => t.Class).FirstOrDefault(t=>t.Id == id);
        }

        public List<Timetable> getTimetablesByTeacherId(int id) { 
            return db.Timetables.Include(t=>t.Teacher).Include(t=>t.Class).Include(t=>t.Subject)
                .Where(t=>t.TeacherId == id).ToList();
        }

        public List<Timetable> getTimetablesByClassId(int id)
        {
            return db.Timetables.Include(t => t.Teacher).Include(t => t.Class).Include(t => t.Subject)
                .Where(t => t.ClassId == id).ToList();
        }

        public List<Timetable> getTimetablesByTeacherName(string name)
        {
            return db.Timetables.Include(t => t.Teacher).Include(t => t.Class).Include(t => t.Subject)
                .Where(t => t.Teacher.Name == name).ToList();
        }

        public List<Timetable> getTimetablesByClassName(string name)
        {
            return db.Timetables.Include(t => t.Teacher).Include(t => t.Class).Include(t => t.Subject)
                .Where(t => t.Class.Name == name).ToList();
        }


    }
}
