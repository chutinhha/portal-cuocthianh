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
    public class CategoryManagementController : BaseController
    {
        //
        // GET: /Administrator/Category/

        public CategoryManagementController(ICategoryActionService category, IConfigurationActionService config)
            : base(category, config)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.CATEGORY)).Role;
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
            var data = CategoryService.GetList();
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
            var data = CategoryService.GetByID(id);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(Category model, HttpPostedFileBase file)
        {
            var exist = this.CategoryService.GetOneByLINQ(c => c.Name.Equals(model.Name));
            if (exist != null && model.ID == 0)
            {
                return Content("exist");
            }
            model.Image = PathUpload;
            var id =this.CategoryService.Save(model);
            PathUpload = "";
            var data = this.CategoryService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id) 
        {
            this.CategoryService.Delete(this.CategoryService.GetByID(id));
            return RedirectToAction("Index", "CategoryManagement");
        }



        #endregion

    }
}
