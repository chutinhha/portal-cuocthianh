using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
namespace CameraShop.Areas.Administrator.Controllers
{
    public class PermissionManagementController:BaseController
    {
         //
        // GET: /Administrator/Category/

        public PermissionManagementController(IUserActionService user, IUser_Role_ModuleActionService userrole,
            IModuleActionService module, IGroupActionService group)
            : base(user, userrole, module, group)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.PERMISSION)).Role;
            }
            catch
            {
            }
        }

        #region Main 

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll(int userid=0)
        {
            IList<User_Role_Module> data = null;
            if (userid == 0)
                data = UserRoleModuleService.GetList();
            else
            {
                data = UserRoleModuleService.GetList().Where(c=>c.UserID.Equals(userid)).ToList();
            }
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
            var data = UserRoleModuleService.GetByID(id);
            if (data == null)
                data = new User_Role_Module();
            data.listmoduleExt = UserRoleModuleService.GetListModule(int.Parse(data.ModuleID.ToString()));
            data.listUserExt = UserService.GetListUserManger(int.Parse(data.UserID.ToString()));
            if (data.Role != null && data.Role != "")
            {
                data.RExt = data.Role.Contains("R");
                data.UExt = data.Role.Contains("U");
                data.CExt = data.Role.Contains("C");
                data.DExt = data.Role.Contains("D");
            }
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(User_Role_Module model, HttpPostedFileBase file)
        {
            model.Role="";
            if (model.CExt)
                model.Role += "C";
            if (model.RExt)
                model.Role += "R";
            if (model.UExt)
                model.Role += "U";
            if (model.DExt)
                model.Role += "D";
            var id = this.UserRoleModuleService.Save(model);
            var data = this.UserRoleModuleService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id) 
        {
            this.UserRoleModuleService.Delete(this.UserRoleModuleService.GetByID(id));
            return RedirectToAction("Index", "PermissionManagement");
        }

        public ActionResult _Search()
        {
            var data = new User();
            data.ListUserManegerExt = UserService.GetListUserManger(0);
            return PartialView(data);
        }

        #endregion


    }
}
