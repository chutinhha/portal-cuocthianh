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
    public class CustomerIdeaManagerController : BaseController
    {

        public CustomerIdeaManagerController(ICustomerIdeaActionService _customeridea, IUserActionService _user)
            :base(_customeridea, _user){
                try
                {
                    Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.CUSTOMERIDEA)).Role;
                }
                catch { }
        }
        //
        // GET: /Administrator/CustomerIdeaManager/

        /// <summary>
        /// CustomerIdea Manager View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        /// <summary>
        /// Show Customer Idea By Type
        /// </summary>
        /// <param name="typeid">Type ID</param>
        /// <returns></returns>
        public ActionResult _ShowIdeaByType(int typeid) {
            var data = CustomerIdeaService.GetCustomerIdeaByType(typeid);
            return PartialView(data);
        }

        /// <summary>
        /// Delete Customer Idea
        /// </summary>
        /// <param name="id">Customer Idea ID</param>
        /// <returns></returns>
        public ActionResult Delete(int id) {
            this.CustomerIdeaService.Delete(this.CustomerIdeaService.GetByID(id));
            return RedirectToAction("Index", "CustomerIdeaManager");
        }

        /// <summary>
        /// Show Customer Idea Detail
        /// </summary>
        /// <param name="id">Customer Idea ID</param>
        /// <returns></returns>
        public ActionResult Detail(int id) {
            var data = this.CustomerIdeaService.GetByID(id);
            if (data.UserID == 0)
                data.UserNameExt = "Anonymous";
            else {
                var user = this.UserService.GetByID(data.UserID);
                data.UserNameExt = user.Name;
            }
            return View(data);
        }
    }
}
