using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class User_GroupService
    {
       readonly IUser_GroupEntity entity;

       public User_GroupService(IUser_GroupEntity entity)
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
               var obj = (CoreData.User_Group)_model;
               var check = GetOneByLINQ(c=>c.GroupID.Equals(obj.GroupID)&&c.UserID.Equals(obj.UserID));
               if(check!=null)
               {
                   obj.ID = check.ID;
               }
               else{
                   obj.ID=0;
               }
               var id = obj.ID;
               if (id == 0)
               {
                   return entity.Save(obj, Table.User_Group.ToString());
               }
               else
               {
                   return entity.Update(obj, Table.User_Group.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.User_Group GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.User_Group.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User_Group> GetList()
       {
           try
           {
               return entity.GetAll(Table.User_Group.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User_Group> GetListByLINQ(Func<CoreData.User_Group, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.User_Group.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.User_Group GetOneByLINQ(Func<CoreData.User_Group, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.User_Group.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User_Group> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.User_Group.ToString()).ToList();
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
               entity.Delete((CoreData.User_Group)_model, Table.User_Group.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method



        #endregion 
    
    }
         
}
