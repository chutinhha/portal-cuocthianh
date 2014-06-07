using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;

namespace CameraShop.Areas.Administrator.Controllers
{
    public class OrderManagementController : BaseController
    {
        //
        // GET: /Administrator/OrderManagement/
        public OrderManagementController(IOrderActionService order,IOrderDetailActionService orderdetail, IUserActionService user, IProductActionService product)
            : base(order,orderdetail,user,product)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.ORDER)).Role;
            }
            catch { }
        }

        #region Main
        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll(int status)
        {
            var data = OrderService.GetByStatus(status);
            return PartialView(data);
        }
        #endregion

        #region CRUD
        /// <summary>
        /// view
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrUpdate(int id = 0)
        {
            return CanUpdate((int)id);
        }

        public ActionResult _AddOrUpdate(int id)
        {
            var data = OrderService.GetByID(id);
            if (data != null)
                data.ListStatusExt = OrderService.GetListStatus(data.Status);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(Order model, HttpPostedFileBase file)
        {
            
            var id = this.OrderService.Save(model);
            if (model.Status == (int)Helper.ValueDefine.OrderStatus.Completed)
            {
                var p = OrderDetailService.GetListByLINQ(x=>x.OrderID==model.ID);
                foreach (var item in p)
                {
                    ProductService.UpdateSoleAmount(int.Parse(item.ProductID.ToString()), item.Amount);
                }
            }
            var data = this.OrderService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id)
        {
            this.OrderService.Delete(this.OrderService.GetByID(id));
            return RedirectToAction("Index", "OrderManagement");
        }

        #endregion

        #region Details



        public ActionResult Details(int id)
        {
            var data = OrderDetailService.GetListWithProductNameByOrderID(id);
            return CanView((IList<OrderDetail>)data);
        }

        /// <summary>
        /// view
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrUpdateDetail(int id = 0)
        {
            return CanUpdate((int)id);
        }

        public ActionResult _AddOrUpdateDetail(int id)
        {
            var data = OrderDetailService.GetByID(id);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdateDetail(OrderDetail model, HttpPostedFileBase file)
        {
            var id = this.OrderService.Save(model);
            var data = this.OrderService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteDetail(int id)
        {
            this.OrderService.Delete(this.OrderService.GetByID(id));
            return RedirectToAction("Index", "OrderManagement");
        }


        #endregion

        #region Update Qty

        [HttpPost]
        public ActionResult UpdateQty(OrderDetail model)
        {
            var order = OrderDetailService.GetByID(model.ID);
            order.OrderID = model.OrderID;
            order.Amount = model.Amount;
            long rs = OrderDetailService.Save(order);
            if (rs == -1)
                return Content("error");
            return Content("success");
           
        }
        #endregion

        public ActionResult _PartialDetails(int id)
        {
            var data = OrderDetailService.GetListWithProductNameByOrderID(id);
            return PartialView(data);
        }
    }
}





