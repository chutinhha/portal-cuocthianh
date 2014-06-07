using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class EmailListService
    {
       readonly IEmailListEntity entity;

       public EmailListService(IEmailListEntity entity)
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
               var exist = GetOneByLINQ(c => c.Email.Equals(((CoreData.EmailList)_model).Email));
                   if(exist==null)
                   {
                       var id = long.Parse(_model.GetType().GetProperty("ID").GetValue(_model, null).ToString());
                       if (id == 0)
                       {
                           return entity.Save((CoreData.EmailList)_model, Table.EmailList.ToString());
                       }
                       else
                       {
                           return entity.Update((CoreData.EmailList)_model, Table.EmailList.ToString());
                       }
                   }
                   else
                   {
                       return -1;
                   }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.EmailList GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.EmailList.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.EmailList> GetList()
       {
           try
           {
               return entity.GetAll(Table.EmailList.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.EmailList> GetListByLINQ(Func<CoreData.EmailList, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.EmailList.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.EmailList GetOneByLINQ(Func<CoreData.EmailList, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.EmailList.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.EmailList> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.EmailList.ToString()).ToList();
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
               entity.Delete((CoreData.EmailList)_model, Table.EmailList.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method



        #endregion 
    
    }
         
}
