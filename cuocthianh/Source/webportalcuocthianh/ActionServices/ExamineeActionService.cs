using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IExamineeActionService
    {
        long Save(object _model);
        CoreData.Examinee GetByID(long _id);
        IList<CoreData.Examinee> GetList();
        IList<CoreData.Examinee> GetListByLINQ(Func<CoreData.Examinee, Boolean> _where);
        CoreData.Examinee GetOneByLINQ(Func<CoreData.Examinee, Boolean> _where);
        IList<CoreData.Examinee> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class ExamineeActionService:IExamineeActionService
    {
       ExamineeService Service;

       public ExamineeActionService(ExamineeService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Examinee GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Examinee> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Examinee> GetListByLINQ(Func<CoreData.Examinee, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Examinee GetOneByLINQ(Func<CoreData.Examinee, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Examinee> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method
        #endregion

    }
         
}
