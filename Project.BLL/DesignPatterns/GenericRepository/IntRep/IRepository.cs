using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.IntRep
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
        T Find(Expression<Func<T, bool>> where);

        List<T> List();
        IQueryable<T> ListQueryable();
        List<T> List(Expression<Func<T, bool>> where);





    }
}
