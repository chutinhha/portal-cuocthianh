using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class Group_Role_ModuleService
    {
       readonly IGroup_Role_ModuleEntity entity;

       public Group_Role_ModuleService(IGroup_Role_ModuleEntity entity)
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
                   return entity.Save((CoreData.Group_Role_Module)_model, Table.Group_Role_Module.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Group_Role_Module)_model, Table.Group_Role_Module.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Group_Role_Module GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Group_Role_Module.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Group_Role_Module> GetList()
       {
           try
           {
               return entity.GetAll(Table.Group_Role_Module.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Group_Role_Module> GetListByLINQ(Func<CoreData.Group_Role_Module, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Group_Role_Module.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Group_Role_Module GetOneByLINQ(Func<CoreData.Group_Role_Module, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Group_Role_Module.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Group_Role_Module> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Group_Role_Module.ToString()).ToList();
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
               entity.Delete((CoreData.Group_Role_Module)_model, Table.Group_Role_Module.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method



        #endregion 
    
    }
         
}
