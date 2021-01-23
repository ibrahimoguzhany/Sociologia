using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using Project.DAL.EntityFramework;
using Project.DAL.DesignPatterns.SingletonPattern;
using Project.ENTITIES.Entities;
using Project.COMMON;
using Project.CORE.DataAccess;

namespace Project.DAL.DesignPattern.GenericRepository.BaseRep
{
    public class BaseRepository<T> : IDataAccess<T> where T : BaseEntity
    {
        private MyContext _db = new MyContext();
        private DbSet<T> _objectSet;




        public BaseRepository()
        {
            _db = DBTool.DBInstance;
            _objectSet = _db.Set<T>();

        }


        public int Insert(T item)
        {
            _objectSet.Add(item);

            if(item is BaseEntity)
            {
                BaseEntity o = item as BaseEntity;
                DateTime now = DateTime.Now;

                o.CreatedDate = now;
                o.ModifiedDate = now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : Islem yapan kullanci adi yazilmali...
                         
            }


            return Save();
             

        }

        public int Delete(T item)
        {

            if (item is BaseEntity)
            {
                BaseEntity o = item as BaseEntity;
                
                o.ModifiedDate = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : Islem yapan kullanci adi yazilmali...
            }
            _objectSet.Remove(item);
            return Save();
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public int Update(T item)
        {
            //item.ModifiedDate = DateTime.Now;
            //item.Status = ENTITIES.Enums.DataStatus.Updated;
            //T guncellecek = Find(item.ID);
            //_db.Entry(guncellecek).CurrentValues.SetValues(item);

            if (item is BaseEntity)
            {
                BaseEntity o = item as BaseEntity;
            
                o.ModifiedDate = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : Islem yapan kullanci adi yazilmali...
            }
            return Save();
        }

    

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
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
