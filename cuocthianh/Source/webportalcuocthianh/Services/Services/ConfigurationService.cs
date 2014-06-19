using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class ConfigurationService
    {
       readonly IConfigurationEntity entity;

       public ConfigurationService(IConfigurationEntity entity)
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
               var id = long.Parse(_model.GetType().GetProperty("ID").GetValue(_model, null).ToString());
               if (id == 0)
               {
                   return entity.Save((CoreData.Configuration)_model, Table.Configuration.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Configuration)_model, Table.Configuration.ToString());
               }
           }
           catch
           {
               return -1;
           }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Configuration GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Configuration.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Configuration> GetList()
       {
           try
           {
               return entity.GetAll(Table.Configuration.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Configuration> GetListByLINQ(Func<CoreData.Configuration, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Configuration.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Configuration GetOneByLINQ(Func<CoreData.Configuration, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Configuration.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Configuration> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Configuration.ToString()).ToList();
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
               entity.Delete((CoreData.Configuration)_model, Table.Configuration.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public IList<Configuration> GetGenneralConfig()
       {
           try
           {
               return entity.GetByCusTomSQL(SQLCommand.GetGenneralConfig).ToList();
           }
           catch { return null; }
       }
       public IList<Configuration> GetEmailConfig()
       {
           try
           {
               return entity.GetByCusTomSQL(SQLCommand.GetEmailConfig).ToList();
           }
           catch { return null; }
       }
       


       public long UpdateConfig(string data)
       {
           try
           {
               char[] delimiters = new char[] { '#', '#', };
               char[] delimiters1 = new char[] { '+' };
               var _data = data.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
               foreach (var item in _data)
               {
                   var separe = item.Split(delimiters1);
                   var _item = entity.Get(c => c.Code.Equals(separe[0]), Table.Configuration.ToString());
                   if (_item != null)
                   {
                       if (_item.Code == "Logo")
                       {
                           if (_item.Value != "")
                               _item.Value = separe[1];
                           Save(_item);
                       }
                       else
                       {
                           _item.Value = separe[1];
                           Save(_item);
                       }
                   }
               }
               return 1;
           }
           catch {
               return -1;
           }
       }

      
        #endregion 
    
    }
         
}
