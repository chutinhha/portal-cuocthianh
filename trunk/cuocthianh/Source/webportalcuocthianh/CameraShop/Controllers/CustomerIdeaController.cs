using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;

namespace CameraShop.Controllers
{
    public class CustomerIdeaController : BaseController
    {
        public CustomerIdeaController(ICustomerIdeaActionService _customeridea, IUserActionService _user)
            : base(_customeridea, _user)
        { }
        //
        // GET: /CustomerIdea/
        /// <summary>
        /// Partial View Of Add Order Detail Comment
        /// </summary>
        /// <param name="_orderid"></param>
        /// <returns></returns>
        public ActionResult _AddOrderDetailComment(int _orderid)
        {
            OrderID = _orderid;
            return PartialView();
        }
        /// <summary>
        /// Action
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AddOrderDetailComment(CustomerIdea model, HttpPostedFileBase file)
        {
            try
            {
                model.Attachment = PathUpload;
                model.UserID = Userid;
                model.Type = 1;
                PathUpload = "";
                model.OrderID = OrderID;
                model.ID = 0;
                var _user = UserService.GetByID(Userid);
                model.Email = _user.Email;
                CustomerIdeaService.Save(model);
                return RedirectToAction("Detail", "Order", new { id=OrderID});
            }
            catch {
                return RedirectToAction("Detail", "Order", new { id = OrderID });
            }
        }
        /// <summary>
        /// View
        /// </summary>
        /// <returns></returns>
        public ActionResult _AddContactComment()
        {
            return PartialView();
        }
        /// <summary>
        /// Action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AddContactComment(CustomerIdea model) {
            try {
                model.UserID = Userid;
                model.Type = 2;
                model.ID = 0;
                CustomerIdeaService.Save(model);
                return RedirectToAction("Contact", "Home");
            }
            catch {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}
