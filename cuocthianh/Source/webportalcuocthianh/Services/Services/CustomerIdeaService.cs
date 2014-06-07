using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class CustomerIdeaService
    {
       readonly ICustomerIdeaEntity entity;

       public CustomerIdeaService(ICustomerIdeaEntity entity)
       {
           this.entity = entity;

       }

       #region Main Method

       /// <summary>
       /// Save 
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public long Save(object _model)
       {
           try
           {
               var model = (CustomerIdea)_model;
               model.DateCreate = DateTime.Now;
               var id = long.Parse(_model.GetType().GetProperty("ID").GetValue(_model, null).ToString());
               if (id == 0)
               {
                   return entity.Save(model, Table.CustomerIdea.ToString());
               }
               else
               {
                   return entity.Update(model, Table.CustomerIdea.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.CustomerIdea GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.CustomerIdea.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.CustomerIdea> GetList()
       {
           try
           {
               return entity.GetAll(Table.CustomerIdea.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.CustomerIdea> GetListByLINQ(Func<CoreData.CustomerIdea, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.CustomerIdea.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.CustomerIdea GetOneByLINQ(Func<CoreData.CustomerIdea, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.CustomerIdea.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.CustomerIdea> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.CustomerIdea.ToString()).ToList();
           }
           catch { return null; }
       }


       /// <summary>
       /// Delete
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public bool Delete(object _model)
       {
           try
           {
               entity.Delete((CoreData.CustomerIdea)_model, Table.CustomerIdea.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion

       #region Other Method
       public IList<CoreData.CustomerIdea> GetCustomerIdeaByType(int typeid)
       {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetCustomerIdeaByType, typeid)).ToList();
           }
           catch
           {
               return null;
           }
       }

        #endregion 
    
    }
         
}
