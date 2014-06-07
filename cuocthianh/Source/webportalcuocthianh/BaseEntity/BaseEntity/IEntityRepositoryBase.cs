using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseEntity
{
    public interface IEntityRepositoryBase<T> where T : class
    {
        long Save(T entity, string TableName);
        long Update(T entity, string TableName);
        void Delete(T entity, string TableName);
        void Delete(Func<T, Boolean> predicate, string TableName);
        T GetById(long Id, string TableName);
        T Get(Func<T, Boolean> where, string TableName);
        IEnumerable<T> GetAll(string TableName);
        IEnumerable<T> GetMany(Func<T, bool> where, string TableName);
        IEnumerable<T> GetBySearchString(string where, string TableName);
        IEnumerable<T> GetByCusTomSQL(string SQL);
        IEnumerable<T> GetContext(string TableName);
    }
}
