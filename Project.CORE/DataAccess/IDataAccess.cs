using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.CORE.DataAccess
{
    public interface IDataAccess<T> where T : class
    {
        int Insert(T item);
        int Update(T item);
        int Delete(T item);
        int Save();
        T Find(Expression<Func<T, bool>> where);
        List<T> List();
        IQueryable<T> ListQueryable();
        List<T> List(Expression<Func<T, bool>> where);





    }
}
