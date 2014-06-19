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
    public class ArticleManagementController : BaseController
    {
        //
        // GET: /Administrator/ArticleManagement/
        public ArticleManagementController(IArticleActionService _article, IUserActionService _user, IConfigurationActionService _config)
            : base(_article, _user, _config)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.ARTICLE)).Role;
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
            var data = ArticleService.GetList();
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
            SessionManagement.SetSesionValue("ArticleID", id.ToString());
            return CanUpdate((int)id);
        }

        public ActionResult _AddOrUpdate(int id)
        {
            var data = ArticleService.GetByID(id);
            if (data == null)
                data = new Article();
            data.ListCategoryExt = ArticleService.GetListCategory((int.Parse(data.CateID.ToString())));
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrUpdate(Article model, HttpPostedFileBase file)
        {
            var exist = this.ArticleService.GetOneByLINQ(c => c.Title.Equals(model.Title));
            if (exist != null && model.ID == 0)
            {
                return Content("exist");
            }
            model.UserID = SessionManagement.GetSessionReturnInt("UserID");
            model.Image = PathUpload;
            var id = this.ArticleService.Save(model);
            PathUpload = "";
            var data = this.ArticleService.GetByID(id);
            SessionManagement.SetSesionValue("ArticleID", id.ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Delete(int id)
        {
            this.ArticleService.Delete(this.ArticleService.GetByID(id));
            return RedirectToAction("Index", "ArticleManagement");
        }

        #endregion

    }
}
