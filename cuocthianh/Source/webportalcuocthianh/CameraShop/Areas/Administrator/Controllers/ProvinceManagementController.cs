using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using Helper;
using CoreData;

namespace CameraShop.Areas.Administrator.Controllers
{
    public class ProvinceManagementController : BaseController
    {
        //
        // GET: /Administrator/Province/
         #region Main
        public ProvinceManagementController(IProvinceActionService _province) : base(_province) {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.PROVINCE)).Role;
            }
            catch { }
        }
        public ActionResult Index()
        {
            return SetUpSecurity();
        }
        public ActionResult _ShowAll()
        {
            var data = ProvinceService.GetList();
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
            var data = ProvinceService.GetByID(id);
            return PartialView(data);
        }
        public ActionResult Delete(int id)
        {
            this.ProvinceService.Delete(this.ProvinceService.GetByID(id));
            return RedirectToAction("Index", "ProvinceManagement");
        }
         [HttpPost]
        public ActionResult AddOrUpdate(Province model, HttpPostedFileBase file)
        {
            var id = this.ProvinceService.Save(model);
            var data = this.ProvinceService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
