using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IEmailListActionService
    {
        long Save(object _model);
        CoreData.EmailList GetByID(long _id);
        IList<CoreData.EmailList> GetList();
        IList<CoreData.EmailList> GetListByLINQ(Func<CoreData.EmailList, Boolean> _where);
        CoreData.EmailList GetOneByLINQ(Func<CoreData.EmailList, Boolean> _where);
        IList<CoreData.EmailList> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class EmailListActionService:IEmailListActionService
    {
       EmailListService Service;

       public EmailListActionService(EmailListService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.EmailList GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.EmailList> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.EmailList> GetListByLINQ(Func<CoreData.EmailList, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.EmailList GetOneByLINQ(Func<CoreData.EmailList, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.EmailList> GetList(string _searchstring)
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
