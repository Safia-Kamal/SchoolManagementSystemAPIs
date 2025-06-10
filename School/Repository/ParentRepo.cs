using School.Models;

namespace School.Repository
{
    public class ParentRepo : GenericRepo<Parent>
    {

        public ParentRepo(SchoolDbContext db) : base(db)
        {
        }
        public Parent getParentByNtionalId(string nationalId)
        {
            return db.Parents.FirstOrDefault(propa => propa.NationalId == nationalId);
        }




    }
}
