using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
using System.Web.Mvc;
namespace ActionServices
{

    public interface IOrderActionService
    {
        long Save(object _model);
        CoreData.Order GetByID(long _id);
        IList<CoreData.Order> GetList();
        IList<CoreData.Order> GetListByLINQ(Func<CoreData.Order, Boolean> _where);
        CoreData.Order GetOneByLINQ(Func<CoreData.Order, Boolean> _where);
        IList<CoreData.Order> GetList(string _searchstring);
        bool Delete(object _model);
        List<CoreData.Order> GetListByUserID(int userid);
        List<SelectListItem> GetListStatus(int status);
        bool UpdateStatus(int id, int statusid);
        IList<Order> GetByStatus(int status);
    }

    public partial class OrderActionService:IOrderActionService
    {
       OrderService Service;

       public OrderActionService(OrderService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Order GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Order> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Order> GetListByLINQ(Func<CoreData.Order, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Order GetOneByLINQ(Func<CoreData.Order, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Order> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public List<CoreData.Order> GetListByUserID(int userid) {
           return Service.GetListByUserID(userid);
       }
       public virtual List<SelectListItem> GetListStatus(int status)
       {
           return Service.GetListStatus(status);
       }
        public virtual bool UpdateStatus(int id, int statusid) {
            return Service.UpdateStatus(id, statusid);
       }

        public virtual IList<Order> GetByStatus(int status)
        {
            return Service.GetByStatus(status);
        }

        #endregion

    }
         
}
