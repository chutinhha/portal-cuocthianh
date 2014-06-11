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
    public class CategoryArticleManagementController : BaseController
    {
        //
        // GET: /Administrator/Category/

        public CategoryArticleManagementController(ICategoryArticleActionService category)
            : base(category)
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
            var data = CategoryArticleService.GetList();
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
            var data = CategoryArticleService.GetByID(id);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(CategoryArticle model, HttpPostedFileBase file)
        {
            var exist = this.CategoryArticleService.GetOneByLINQ(c => c.Name.Equals(model.Name));
            if (exist != null && model.ID == 0)
            {
                return Content("exist");
            }
            model.Image = PathUpload;
            var id = this.CategoryArticleService.Save(model);
            PathUpload = "";
            var data = this.CategoryArticleService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id) 
        {
            this.CategoryArticleService.Delete(this.CategoryArticleService.GetByID(id));
            return RedirectToAction("Index", "CategoryArticleManagement");
        }



        #endregion

    }
}
