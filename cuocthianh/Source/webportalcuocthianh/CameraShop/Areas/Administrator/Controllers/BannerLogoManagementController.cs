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
    public class BannerLogoManagementController : BaseController
    {
        //
        // GET: /Administrator/Banner_Logo/
        public BannerLogoManagementController(IBanner_LogoActionService _banner)
            : base(_banner)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.BANNERLOGO)).Role;
            }
            catch { }
        }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Show All Banner
        /// </summary>
        /// <returns></returns>
        public ActionResult _ShowAll() {
            var data = BannerService.GetList();
            return PartialView(data);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            SessionManagement.SetSesionValue("BannerID", id.ToString());
            return CanUpdate((int)id);
        }

        public ActionResult _AddOrUpdate(int id)
        {
            var data = BannerService.GetByID(id);
            if (data == null)
                data = new Banner_Logo();
            data.ListCategoryExt = BannerService.GetListCategory((int.Parse(data.ProductCateID.ToString())));
            int position = (int)Enum.Parse(typeof(ValueDefine.BannerLogoPosition), Enum.GetName(typeof(ValueDefine.BannerLogoPosition), data.Position));
            data.ListPositionExt = BannerService.GetListPosition(position);
            int type = (int)Enum.Parse(typeof(ValueDefine.BannerLogoType), Enum.GetName(typeof(ValueDefine.BannerLogoType), data.Type));
            data.ListTypeExt = BannerService.GetListType(type);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrUpdate(Banner_Logo model, HttpPostedFileBase file)
        {
            var exist = this.BannerService.GetOneByLINQ(c => c.Name.Equals(model.Name));
            model.Position = 0;
            model.Type = 0;
            if (exist != null && model.ID == 0)
            {
                return Content("exist");
            }
            if (PathUpload == "" || PathUpload == null)
            {
                model.Image = exist.Image;
            }
            else
            {
                model.Image = PathUpload;
            }
            
            var id = this.BannerService.Save(model);
            PathUpload = "";
            var data = this.BannerService.GetByID(id);
            SessionManagement.SetSesionValue("BannerID", id.ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            this.BannerService.Delete(this.BannerService.GetByID(id));
            return RedirectToAction("Index", "BannerLogoManagement");
        }

    }
}
