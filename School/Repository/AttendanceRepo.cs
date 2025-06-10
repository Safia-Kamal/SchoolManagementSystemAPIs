using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Repository
{
    public class AttendanceRepo:GenericRepo<Attendance>
    {
        public AttendanceRepo(SchoolDbContext context) : base(context) { }
        public List<Attendance> getAllWithStd()
        {
            return db.Attendances.Include(a => a.Student).ToList();
        }

        public Attendance getByIdWithStd(int id)
        {
            return db.Attendances.Include(a=>a.Student).FirstOrDefault(a => a.Id == id);

        }
        public List<Attendance> getByStudentId(int studentId)
        {
            return db.Attendances.Include(a=>a.Student).Where(n => n.StudentId == studentId).ToList();
        }

        public List<Attendance> getByStudentName(string studentName)
        {
            return db.Attendances.Include(a => a.Student).Where(a => a.Student.Name == studentName)
             .ToList();
        }

        public List<Attendance> getByDate(DateTime date)
        {
            return db.Attendances
                .Where(a => a.Date.Date == date.Date)
                .Include(a => a.Student)
                .ToList();
        }


        public List<Attendance> getByClassName(string className)
        {
            return db.Attendances.Include(a => a.Student).Where(a => a.Student.Class.Name == className)
             .ToList();
        }

        public List<Attendance> getByClassId(int classId)
        {
            return db.Attendances.Include(a => a.Student).Where(a => a.Student.ClassId == classId)
             .ToList();
        }

        public Attendance check(int stdId)
        {
            var today = DateTime.Today;

            return db.Attendances.Include(a=>a.Student).FirstOrDefault(a =>
                a.StudentId == stdId && a.Date.Date == today);
        }

        public List<Attendance> getTodayByClassName(string className)
        {
            var today = DateTime.Today;
            return db.Attendances
                     .Include(a => a.Student)
                     .ThenInclude(a => a.Class)
                     .Where(a => a.Student.Class.Name == className && a.Date.Date == today)
                     .ToList();
        }


    }
}
