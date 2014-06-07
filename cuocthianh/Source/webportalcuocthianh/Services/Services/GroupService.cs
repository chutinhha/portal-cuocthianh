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

    public partial class GroupService
    {
       readonly IGroupEntity entity;

       public GroupService(IGroupEntity entity)
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
                   return entity.Save((CoreData.Group)_model, Table.Group.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Group)_model, Table.Group.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Group GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Group.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Group> GetList()
       {
           try
           {
               return entity.GetAll(Table.Group.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Group> GetListByLINQ(Func<CoreData.Group, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Group.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Group GetOneByLINQ(Func<CoreData.Group, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Group.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Group> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Group.ToString()).ToList();
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
               entity.Delete((CoreData.Group)_model, Table.Group.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public List<SelectListItem> GetSelectList(int id, string type="")
       {
           var lst = new List<SelectListItem>();
           IList<Group> group;
           if (type == "")
               group = GetList().Where(c => c.ID != 1).ToList();
           else
               group = GetList();
           foreach (var c in group)
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
