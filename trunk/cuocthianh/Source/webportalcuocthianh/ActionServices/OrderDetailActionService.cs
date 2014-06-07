using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IOrderDetailActionService
    {
        long Save(object _model);
        CoreData.OrderDetail GetByID(long _id);
        IList<CoreData.OrderDetail> GetList();
        IList<CoreData.OrderDetail> GetListByLINQ(Func<CoreData.OrderDetail, Boolean> _where);
        CoreData.OrderDetail GetOneByLINQ(Func<CoreData.OrderDetail, Boolean> _where);
        IList<CoreData.OrderDetail> GetList(string _searchstring);
        bool Delete(object _model);
        IList<OrderDetail> GetListWithProductNameByOrderID(int _orderid);
        IList<OrderDetail> GetListWithUserIDbyOrderID(int _orderid, long userid);
    }

    public partial class OrderDetailActionService:IOrderDetailActionService
    {
       OrderDetailService Service;

       public OrderDetailActionService(OrderDetailService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.OrderDetail GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.OrderDetail> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.OrderDetail> GetListByLINQ(Func<CoreData.OrderDetail, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.OrderDetail GetOneByLINQ(Func<CoreData.OrderDetail, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.OrderDetail> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method
       public virtual IList<OrderDetail> GetListWithProductNameByOrderID(int _orderid)
       {
           return Service.GetListWithProductNameByOrderID(_orderid);
       }
       public virtual IList<OrderDetail> GetListWithUserIDbyOrderID(int _orderid, long userid)
       {
           return Service.GetListWithUserIDbyOrderID(_orderid, userid);
       }
        #endregion

    }
         
}
