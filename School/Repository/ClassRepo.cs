using Microsoft.AspNetCore.Mvc;
using School.Models;

namespace School.Repository
{
    public class ClassRepo:GenericRepo<Class>
    {
        public ClassRepo(SchoolDbContext context) : base(context) { }

        public Class getClassByName(string name)
        {
            return db.Classes.FirstOrDefault(c => c.Name == name);
        }
        

    }
    }
