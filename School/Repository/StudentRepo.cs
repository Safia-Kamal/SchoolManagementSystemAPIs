using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using School.DTOs.OtherDTOs;
using School.Models;

namespace School.Repository
{
    public class StudentRepo:GenericRepo<Student>
    {
        public StudentRepo(SchoolDbContext db):base(db){ }

        public Student getByName(string name)
        {
            return db.Students.FirstOrDefault(s => s.Name == name);
        }

        public List<Student> getAllWithClass()
        {
            return db.Students.Include(s=>s.Class).ToList();
        }

        public Student getByIdWithClass(int id)
        {
            return db.Students.Include(s => s.Class).FirstOrDefault(s=>s.Id==id);
        }

        public Student getByNameWithClass(string name)
        {
            return db.Students.Include(s => s.Class).FirstOrDefault(s => s.Name == name);
        }

        public List<Student> getStudentsByClassName(string name)
        {
            return db.Students.Include(s => s.Class).Where(s => s.Class.Name == name).ToList();
        }

        public List<Student> getStudentsByClassId(int clsId)
        {
            return db.Students.Include(s => s.Class).Where(s => s.ClassId == clsId).ToList();
        }

        public TotalClassesAndStudentsDTO countStudents()
        { 
            var all = new TotalClassesAndStudentsDTO()
            {
                TotalStudents = db.Students.Count(),
                TotalClasses = db.Classes.Count(),
                TotalTeachers = db.Teachers.Count(),
                TotalSubjects = db.Subjects.Count(),
                TotalParents = db.Parents.Count()
            };
            return all;
        }

        public List<Student> getByPrntId(int prntId) { 
            return db.Students.Include(s => s.Class).Where(s => s.ParentId == prntId).ToList();
        }
    }
}

