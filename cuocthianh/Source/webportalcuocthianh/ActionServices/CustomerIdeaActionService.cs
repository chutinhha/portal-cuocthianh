using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface ICustomerIdeaActionService
    {
        long Save(object _model);
        CoreData.CustomerIdea GetByID(long _id);
        IList<CoreData.CustomerIdea> GetList();
        IList<CoreData.CustomerIdea> GetListByLINQ(Func<CoreData.CustomerIdea, Boolean> _where);
        CoreData.CustomerIdea GetOneByLINQ(Func<CoreData.CustomerIdea, Boolean> _where);
        IList<CoreData.CustomerIdea> GetList(string _searchstring);
        IList<CoreData.CustomerIdea> GetCustomerIdeaByType(int typeid);
        bool Delete(object _model);
    }

    public partial class CustomerIdeaActionService:ICustomerIdeaActionService
    {
       CustomerIdeaService Service;

       public CustomerIdeaActionService(CustomerIdeaService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.CustomerIdea GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.CustomerIdea> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.CustomerIdea> GetListByLINQ(Func<CoreData.CustomerIdea, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.CustomerIdea GetOneByLINQ(Func<CoreData.CustomerIdea, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.CustomerIdea> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public IList<CoreData.CustomerIdea> GetCustomerIdeaByType(int typeid) {
           return Service.GetCustomerIdeaByType(typeid);
       }
        #endregion

    }
         
}
