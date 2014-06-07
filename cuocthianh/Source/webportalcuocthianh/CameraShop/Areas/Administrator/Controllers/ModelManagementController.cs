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
    public class ModelManagementController : BaseController
    {
        //
        // GET: /Administrator/ModelManagement/

        public ModelManagementController(IModelActionService _model, IManufacturerActionService _manufacturer)
            : base(_model, _manufacturer)
        { }

        #region Main 

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll()
        {
            var data = ModelService.GetList();
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
            var data = ModelService.GetByID(id);
            if (data == null)
                data = new Model();
            data.ListManufacturerExt = ManufacturerService.GetSelectList((int.Parse(data.ManufacturerID.ToString())));
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(Model model, HttpPostedFileBase file)
        {
            model.Image = PathUpload;
            var id = this.ModelService.Save(model);
            PathUpload = "";
            var data = this.ModelService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id) 
        {
            this.ModelService.Delete(this.ModelService.GetByID(id));
            return RedirectToAction("Index", "ModelManagement");
        }

        #endregion

    }
}
