using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class PictureExamService
    {
       readonly IPictureExamEntity entity;

       public PictureExamService(IPictureExamEntity entity)
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
                   return entity.Save((CoreData.PictureExam)_model, Table.PictureExam.ToString());
               }
               else
               {
                   return entity.Update((CoreData.PictureExam)_model, Table.PictureExam.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.PictureExam GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.PictureExam.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.PictureExam> GetList()
       {
           try
           {
               return entity.GetAll(Table.PictureExam.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.PictureExam> GetListByLINQ(Func<CoreData.PictureExam, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.PictureExam.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.PictureExam GetOneByLINQ(Func<CoreData.PictureExam, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.PictureExam.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.PictureExam> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.PictureExam.ToString()).ToList();
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
               entity.Delete((CoreData.PictureExam)_model, Table.PictureExam.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method


       /// <summary>
       /// Get List by examineeID
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.PictureExam> GetListByExamineeID(int ExamineeID)
       {    
           try
           {
               return entity.GetMany(c=>c.ExamineeID.Equals(ExamineeID), Table.PictureExam.ToString()).ToList();
           }
           catch { return null; }
       }



       /// <summary>
       /// Get List by UserID
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.PictureExam> GetListByUserID(int UserID)
       {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetPictureExamByUserID, UserID)).ToList();
           }
           catch { return null; }
       }

        #endregion 
    
    }
         
}
