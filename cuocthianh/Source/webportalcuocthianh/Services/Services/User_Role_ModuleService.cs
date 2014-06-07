using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
using System.Web.Mvc;
namespace Services
{

    public partial class User_Role_ModuleService
    {
       readonly IUser_Role_ModuleEntity entity;
       readonly IModuleEntity module;
       public User_Role_ModuleService(IUser_Role_ModuleEntity entity, IModuleEntity _module)
       {
           this.entity = entity;
           this.module = _module;

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
                   return entity.Save((CoreData.User_Role_Module)_model, Table.User_Role_Module.ToString());
               }
               else
               {
                   return entity.Update((CoreData.User_Role_Module)_model, Table.User_Role_Module.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.User_Role_Module GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.User_Role_Module.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User_Role_Module> GetList()
       {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetUserRole,"")).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User_Role_Module> GetListByLINQ(Func<CoreData.User_Role_Module, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.User_Role_Module.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.User_Role_Module GetOneByLINQ(Func<CoreData.User_Role_Module, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.User_Role_Module.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User_Role_Module> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.User_Role_Module.ToString()).ToList();
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
               entity.Delete((CoreData.User_Role_Module)_model, Table.User_Role_Module.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public List<SelectListItem> GetListModule(int id)
       {
           var lst = new List<SelectListItem>();
           var list = module.GetAll(Table.Module.ToString());
           foreach (var c in list)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
          
           return lst;
       }

        #endregion 
    
    }
         
}
