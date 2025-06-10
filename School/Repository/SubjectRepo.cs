using School.Models;

namespace School.Repository
{
    public class SubjectRepo:GenericRepo<Subject>
    {
        public SubjectRepo(SchoolDbContext context) : base(context) { }

        public Subject getSubByName(string name)
        {
            return db.Subjects.FirstOrDefault(s => s.Name == name);
        }

        
    }
}
