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
    public class DistrictManagementController:BaseController
    {
        //
        // GET: /Administrator/Category/

        public DistrictManagementController(IDistrictActionService district, IProvinceActionService province)
            : base(district, province)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.DICSTRICT)).Role;
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
            var data = DistrictService.GetList();
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
            var data = DistrictService.GetByID(id);
            if (data == null)
                data = new District();
            data.ListProvinceExt = ProvinceService.GetListProvince(int.Parse(data.ProvinceID.ToString()));
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(District model, HttpPostedFileBase file)
        {
            var id = this.DistrictService.Save(model);
            var data = this.DistrictService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id)
        {
            this.CategoryService.Delete(this.DistrictService.GetByID(id));
            return RedirectToAction("Index", "DistrictManagement");
        }

        #endregion

    }
}
