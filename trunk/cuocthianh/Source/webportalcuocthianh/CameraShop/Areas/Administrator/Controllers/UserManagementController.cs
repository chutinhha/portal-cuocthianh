using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
using Helper;

namespace CameraShop.Areas.Administrator.Controllers
{
    public class UserManagementController : BaseController
    {
        //
        // GET: /Administrator/Category/

        public UserManagementController(IUserActionService user, IGroupActionService group, ISearchActionService _search, IUser_Role_ModuleActionService _userrole)
            : base(user, group, _search, _userrole)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.USER)).Role;
            }
            catch { }
        }

        #region Main 

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll(int _groupid)
        {
            var data = UserService.GetByGroup(_groupid);
            return PartialView(data);
        }

       
        #endregion




        #region CRUD
         /// <summary>
         /// view
         /// </summary>
         /// <returns></returns>
        public ActionResult AddOrUpdate(int id=0)
        {
            return CanUpdate((int)id);
        }

        public ActionResult _AddOrUpdate(int id)
        {
            var data = UserService.GetByCustomID(id);
            if (data == null)
                data = new User();
            data.ListGroupExt = GroupService.GetSelectList((int.Parse(data.GroupIDExt.ToString())),"full");

            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(User model, HttpPostedFileBase file)
        {

            var id = this.UserService.Save(model); 
         //   var a = GroupService.d
            var data = this.UserService.GetByCustomID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id) 
        {
            this.UserService.Delete(this.UserService.GetByID(id));
            return RedirectToAction("Index", "UserManagement");
        }

        #endregion


        #region Search

        public ActionResult _Search()
        {
            var model = new User();
            model.ListGroupExt = GroupService.GetSelectList(model.GroupIDExt,"full");
            return PartialView(model);
        }

        public ActionResult Search(string name, string username, string email, string groupid)
        {
            var data = SearchService.SearchUser(name, username, email, groupid);
            return PartialView("_ShowAll", data);
        }


        #endregion

    }
}
