using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
using Helper;

namespace CameraShop.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IOrderActionService _order, IOrderDetailActionService _orderdetail, IUserActionService _user, IProductActionService _product)
            : base(_order, _orderdetail, _user, _product)
        { }
        //
        // GET: /Order/

        /// <summary>
        /// Get Order by UserID
        /// </summary>
        /// <returns></returns>
        public ActionResult _OrderList()
        {
            var userid = Convert.ToInt32(Session["UserID"]);
            var data = OrderService.GetListByUserID(userid);
            return View(data);
        }
        /// <summary>
        /// View Order Detail
        /// </summary>
        /// <param name="id">OrderID</param>
        /// <returns></returns>
        public ActionResult Detail(int id) {
            if (Session["UserID"] != null)
            {
                var userid = Convert.ToInt32(Session["UserID"]);
                var data = OrderDetailService.GetListWithUserIDbyOrderID(id, userid);                
                return View(data);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Update Order status by status ID = 1 and 5
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="statusid">Status ID</param>
        /// <returns></returns>
        public ActionResult UpdateStatus(int id, int statusid) {
            OrderService.UpdateStatus(id, statusid);
            return RedirectToAction("Profile", "User");
        }
    }
}
