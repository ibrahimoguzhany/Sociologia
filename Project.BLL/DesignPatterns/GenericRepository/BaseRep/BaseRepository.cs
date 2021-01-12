using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using Project.BLL.DesignPatterns.RepositoryPattern.IntRep;
using Project.DAL.EntityFramework;
using Project.DAL.DesignPatterns.SingletonPattern;
using Project.ENTITIES.Entities;

namespace Project.BLL.DesignPattern.GenericRepository.BaseRep
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MyContext _db = new MyContext();
        private DbSet<T> _objectSet;




        public BaseRepository()
        {
            _db = DBTool.DBInstance;
            _objectSet = _db.Set<T>();

        }


        public void Add(T item)
        {
            _objectSet.Add(item);
            Save();

        }

        public void Delete(T item)
        {
            _objectSet.Remove(item);
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(T item)
        {
            item.ModifiedDate = DateTime.Now;
            item.Status = ENTITIES.Enums.DataStatus.Updated;
            T guncellecek = Find(item.ID);
            _db.Entry(guncellecek).CurrentValues.SetValues(item);
            Save();
        }

    

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.Find(where);
        }

        public T Find(int id)
        {
            return _objectSet.Find(id);
        }
        public List<T> GetAll()
        {
            return _objectSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return _objectSet.FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            return Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted);
        }

      

        public List<T> GetModifieds()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Updated);
        }

        public List<T> GetPassives()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted);
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _objectSet.Where(exp).ToList();
        }



    }
}
