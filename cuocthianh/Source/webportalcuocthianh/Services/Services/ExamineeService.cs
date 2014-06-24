using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class ExamineeService
    {
       readonly IExamineeEntity entity;
       readonly IUserEntity user;
       readonly IPictureExamEntity pictureexam;
       public ExamineeService(IExamineeEntity entity,IUserEntity user, IPictureExamEntity pic)
       {
           this.entity = entity;
           this.user = user;
           this.pictureexam = pic;

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
               var obj = ((Examinee)_model);
               var id = obj.ID;
              // var id = long.Parse(_model.GetType().GetProperty("ID").GetValue(_model, null).ToString());
               if (id == 0)
               {
                   obj.DayOfBirth = DateTime.Now;
                   return entity.Save(obj, Table.Examinee.ToString());
               }
               else
               {
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = GetByID(obj.ID).Image;
                   }
                   return entity.Update(obj, Table.Examinee.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Examinee GetByID(long _id)
       {
           try
           {
               var data = entity.GetById(_id, Table.Examinee.ToString());
               var users = user.Get(c => c.ID.Equals(data.UserID), Table.Users.ToString());
               data.UserNameExt = users.Name;
               return data;
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Examinee> GetList()
       {
           try
           {
               var a = entity.GetMany(c=>c.UserID!=1, Table.Examinee.ToString()).ToList();
               foreach(var i in a)
               {
                   try
                   {
                       var users = user.Get(c => c.ID.Equals(i.UserID), Table.Users.ToString());
                       if (users == null)
                       {
                          // a.Remove(i);
                           i.UserNameExt = "";
                       }
                       else
                       {
                           i.UserNameExt = users.Name;
                       }
                       var pic = pictureexam.Get(c=>c.ExamineeID.Equals(i.ID),Table.PictureExam.ToString());
                       if(pic!=null)
                       {
                           i.Image = pic.Image;
                       }
                   }
                   catch { }
                   
               }
               a = a.Where(c => c.UserNameExt != "").ToList();
               return a;
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Examinee> GetListByLINQ(Func<CoreData.Examinee, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Examinee.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Examinee GetOneByLINQ(Func<CoreData.Examinee, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Examinee.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Examinee> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Examinee.ToString()).ToList();
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
               entity.Delete((CoreData.Examinee)_model, Table.Examinee.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method


     

        #endregion 
    
    }
         
}
