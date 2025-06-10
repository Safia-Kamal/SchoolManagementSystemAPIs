using School.Models;

namespace School.Repository
{
    public class GenericRepo<T> where T : class
    {
        public SchoolDbContext db;
        public GenericRepo(SchoolDbContext db)
        {
            this.db = db;    
        }
        public List<T> getAll() 
        { 
            return db.Set<T>().ToList();

        }
        public T getById(int id)
        {
            return db.Set<T>().Find(id);
        }
        public void add(T item) { 
            db.Set<T>().Add(item);
        }
        public void edit(T item) { 
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void delete(int id) {
            T e = db.Set<T>().Find(id);
            db.Set<T>().Remove(e);
        }

        internal async Task getParentByNtionalId(string parentNationalId)
        {
            throw new NotImplementedException();
        }
    }
}
