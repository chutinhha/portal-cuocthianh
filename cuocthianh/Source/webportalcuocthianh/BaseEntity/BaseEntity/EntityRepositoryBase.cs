using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseEntity
{
    public abstract class EntityRepositoryBase<T> where T : class
    {


        /// <summary>
        /// Add object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="TableName"></param>
        public virtual long Save(T entity, string TableName)
        {
           return BaseEntity.DBModel().insertObject(entity, TableName);

        }

        /// <summary>
        /// Update object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="TableName"></param>
        public virtual long Update(T entity, string TableName)
        {
            var pkey = long.Parse(entity.GetType().GetProperty("ID").GetValue(entity, null).ToString());
            BaseEntity.DBModel().UpdateOnObject(entity, TableName, " ID=" + pkey);
            return pkey;
        }
        /// <summary>
        /// Delete object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="TableName"></param>
        public virtual void Delete(T entity, string TableName)
        {
            var pkey = long.Parse(entity.GetType().GetProperty("ID").GetValue(entity, null).ToString());
            BaseEntity.DBModel().deleteObject(TableName, " ID=" + pkey);
        }
        /// <summary>
        /// Delete object by where
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="TableName"></param>
        public virtual  void Delete(Func<T, Boolean> where, string TableName)
        {
            try
            {
                var dt = BaseEntity.DBModel().SelectObjects(TableName);
                var entity = BaseEntity.DBModel().DynamicCast<T>(dt).ToList();
                IEnumerable<T> objects = entity.Where<T>(where).AsEnumerable();
                foreach (T obj in objects)
                {
                    BaseEntity.DBModel().deleteObject(TableName, "ID=" + obj.GetType().GetProperty("ID").GetValue(obj, null));

                }
            }
            catch(Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public virtual T GetById(long Id, string TableName)
        {
            var dt = BaseEntity.DBModel().SelectObject(TableName, "ID=" + Id);
            var entity = BaseEntity.DBModel().DynamicCast<T>(dt).ToList()[0];
            return entity;

        }

        /// <summary>
        /// Get one row
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public  virtual T Get(Func<T, Boolean> where, string TableName)
        {
            var dt = BaseEntity.DBModel().SelectObjects(TableName);
            var entity = BaseEntity.DBModel().DynamicCast<T>(dt).ToList();
           return entity.Where(where).FirstOrDefault<T>();
        }


        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
       public virtual IEnumerable<T> GetAll(string TableName)
        {
            var dt = BaseEntity.DBModel().SelectObjects(TableName);
            return BaseEntity.DBModel().DynamicCast<T>(dt).ToList();
        }

        /// <summary>
        /// Get many 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
       public virtual IEnumerable<T> GetMany(Func<T, bool> where, string TableName)
       {
           var dt = BaseEntity.DBModel().SelectObjects(TableName);
           var entity = BaseEntity.DBModel().DynamicCast<T>(dt).ToList();
           return entity.Where(where).ToList();
        }

        /// <summary>
        /// Get by search string
        /// </summary>
        /// <param name="where"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
       public virtual IEnumerable<T> GetBySearchString(string where, string TableName)
       {
           var dt = BaseEntity.DBModel().SelectObjects(TableName, where);
           var entity = BaseEntity.DBModel().DynamicCast<T>(dt).ToList();
           return entity.ToList();
       }


        /// <summary>
        /// Get by custom sql
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
       public virtual IEnumerable<T> GetByCusTomSQL(string SQL)
       {
           var dt = BaseEntity.DBModel().selectObjectsBySql(SQL);
           var entity = BaseEntity.DBModel().DynamicCast<T>(dt).ToList();
           return entity;
        }


       public virtual IEnumerable<T> GetContext(string TableName)
       {
          return  GetAll(TableName);
       }

    }
}
