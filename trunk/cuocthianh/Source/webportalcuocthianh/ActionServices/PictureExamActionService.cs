using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IPictureExamActionService
    {
        long Save(object _model);
        CoreData.PictureExam GetByID(long _id);
        IList<CoreData.PictureExam> GetList();
        IList<CoreData.PictureExam> GetListByLINQ(Func<CoreData.PictureExam, Boolean> _where);
        CoreData.PictureExam GetOneByLINQ(Func<CoreData.PictureExam, Boolean> _where);
        IList<CoreData.PictureExam> GetList(string _searchstring);
        bool Delete(object _model);
        IList<CoreData.PictureExam> GetListByExamineeID(int ExamineeID);
        IList<CoreData.PictureExam> GetListByUserID(int UserID);
    }

    public partial class PictureExamActionService:IPictureExamActionService
    {
       PictureExamService Service;

       public PictureExamActionService(PictureExamService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.PictureExam GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.PictureExam> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.PictureExam> GetListByLINQ(Func<CoreData.PictureExam, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.PictureExam GetOneByLINQ(Func<CoreData.PictureExam, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.PictureExam> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public  virtual IList<CoreData.PictureExam> GetListByExamineeID(int ExamineeID)
       {
           return Service.GetListByExamineeID(ExamineeID);
       }

         public IList<CoreData.PictureExam> GetListByUserID(int UserID)
       {
           return Service.GetListByUserID(UserID);
       }

        #endregion

    }
         
}
