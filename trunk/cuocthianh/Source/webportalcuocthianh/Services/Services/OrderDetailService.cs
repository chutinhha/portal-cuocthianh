using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class OrderDetailService
    {
       readonly IOrderDetailEntity entity;
       readonly IOrderEntity orderentity;
       public OrderDetailService(IOrderDetailEntity entity, IOrderEntity orderentity)
       {
           this.entity = entity;
           this.orderentity = orderentity;

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
               var obj = (OrderDetail)_model;
               var id = long.Parse(_model.GetType().GetProperty("ID").GetValue(_model, null).ToString());
               if (id == 0)
               {
                   return entity.Save((CoreData.OrderDetail)_model, Table.OrderDetail.ToString());
               }
               else
               {
                   obj.Total = obj.Price * obj.Amount;
                   var sum = obj.Total;
                   var Orderdetail = entity.GetMany(c=>c.OrderID.Equals(obj.OrderID), Table.OrderDetail.ToString());
                   foreach (var item in Orderdetail)
                   {
                       if (item.ID != obj.ID)
                       {
                           sum += item.Total;
                       }
                   }
                   var oder = orderentity.GetById(obj.OrderID, Table.Order.ToString());
                   oder.TotalPrice = sum + oder.Ship_Fee;
                   orderentity.Save(oder, Table.Order.ToString());
                   return entity.Update((CoreData.OrderDetail)_model, Table.OrderDetail.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.OrderDetail GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.OrderDetail.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.OrderDetail> GetList()
       {
           try
           {
               return entity.GetAll(Table.OrderDetail.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.OrderDetail> GetListByLINQ(Func<CoreData.OrderDetail, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.OrderDetail.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.OrderDetail GetOneByLINQ(Func<CoreData.OrderDetail, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.OrderDetail.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.OrderDetail> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.OrderDetail.ToString()).ToList();
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
               entity.Delete((CoreData.OrderDetail)_model, Table.OrderDetail.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public IList<OrderDetail> GetListWithProductNameByOrderID(int _orderid)
       {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetOrderDetailByID,_orderid)).ToList();
           }
           catch { return null; }
       }


       public IList<CoreData.OrderDetail> GetListWithUserIDbyOrderID(int _orderid, long userid)
       {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetOrderDetailWithUserIDByOrderID, _orderid, userid)).ToList();
           }
           catch { return null; }
       }
        #endregion 
    
    }
         
}
