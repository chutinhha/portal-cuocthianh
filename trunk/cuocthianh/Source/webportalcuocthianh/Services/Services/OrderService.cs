using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
using System.Web.Mvc;
namespace Services
{

    public partial class OrderService
    {
       readonly IOrderEntity entity;

       public OrderService(IOrderEntity entity)
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
                   return entity.Save((CoreData.Order)_model, Table.Order.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Order)_model, Table.Order.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Order GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Order.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Order> GetList()
       {
           try
           {
               return entity.GetAll(Table.Order.ToString()).ToList();
           //  return  entity.GetByCusTomSQL(SQLCommand.GetOrder).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Order> GetListByLINQ(Func<CoreData.Order, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Order.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Order GetOneByLINQ(Func<CoreData.Order, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Order.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Order> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Order.ToString()).ToList();
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
               entity.Delete((CoreData.Order)_model, Table.Order.ToString());
               return true;
           }
           catch { return false; }
       }
        /// <summary>
        /// Update Order Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public bool UpdateStatus(int id, int statusid)
       {
           try {
               var _model = entity.GetById(id, Table.Order.ToString());
               _model.Status = statusid;
               entity.Update((CoreData.Order)_model, Table.Order.ToString());
               return true;
           }
           catch { return false; }
       }
       #endregion



        #region Other Method

       public List<CoreData.Order> GetListByUserID(int userid) {
           return entity.GetByCusTomSQL(String.Format(SQLCommand.GetOrderByUserID, userid)).ToList();
       }

       public List<SelectListItem> GetListStatus(int status)
       {
           List<SelectListItem> stt = new List<SelectListItem>();
           SelectListItem _new = new SelectListItem() { Text = "Đơn hàng mới", Value = "1" };
           SelectListItem _pending = new SelectListItem() { Text = "Đang xử lý", Value = "2" };
           SelectListItem _complete = new SelectListItem() { Text = "Hoàn thành", Value = "3" };
           SelectListItem _paid = new SelectListItem() { Text = "Đã thanh toán", Value = "4" };
           SelectListItem _cancel = new SelectListItem() { Text = "Yêu cầu hủy", Value = "5" };

           stt.Add(_new); stt.Add(_pending); stt.Add(_complete); stt.Add(_paid); stt.Add(_cancel);
           foreach (var c in stt)
           {

               if (status != 0)
                   c.Selected = c.Value == status.ToString();

           }
           return stt;
       }

       public IList<Order> GetByStatus(int status)
       {
           try {
               return entity.GetMany(c => c.Status.Equals(status),Table.Order.ToString()).ToList();
           }
           catch {
               return null;
           }
       }

        #endregion 
    
    }
         
}
