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
    public class ManufacturerManagementController : BaseController
    {
        //
        // GET: /Administrator/ManufacturerManagement/

        public ManufacturerManagementController(IManufacturerActionService manufacturer, IConfigurationActionService config)
            : base(manufacturer, config)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.MANUFACTURER)).Role;
            }
            catch { }
        }

        #region Main 

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll()
        {
            var data = ManufacturerService.GetList();
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
            var data = ManufacturerService.GetByID(id);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(Manufacturer model, HttpPostedFileBase file)
        {
            var exist = this.ManufacturerService.GetOneByLINQ(c => c.Name.Equals(model.Name));
            if (exist != null && model.ID == 0)
            {
                return Content("exist");
            }
            model.Image = PathUpload;
            var id = this.ManufacturerService.Save(model);
            PathUpload = "";
            var data = this.ManufacturerService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id)
        {
            this.ManufacturerService.Delete(this.ManufacturerService.GetByID(id));
            return RedirectToAction("Index", "CategoryManagement");
        }
        #endregion

    }
}
