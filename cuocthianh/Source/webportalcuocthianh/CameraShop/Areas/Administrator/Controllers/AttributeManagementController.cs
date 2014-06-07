using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
namespace CameraShop.Areas.Administrator.Controllers
{
    public class AttributeManagementController :BaseController
    {
         //
        // GET: /Administrator/Category/

        public AttributeManagementController(IAttributeActionService attribute)
            : base(attribute)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.ATTRIBUTE)).Role;
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

        public ActionResult _ShowAll()
        {
            var data = AttributeService.GetList();
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
            var data = AttributeService.GetByID(id);
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(CoreData.Attribute model, HttpPostedFileBase file)
        {
            var exist = this.AttributeService.GetOneByLINQ(c => c.Name.Equals(model.Name));
            if (exist != null && model.ID == 0)
            {
                return Content("exist");
            }
            var id = this.AttributeService.Save(model);
            var data = this.AttributeService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id) 
        {
            this.AttributeService.Delete(this.AttributeService.GetByID(id));
            return RedirectToAction("Index", "AttributeManagement");
        }

        #endregion


    }
}
