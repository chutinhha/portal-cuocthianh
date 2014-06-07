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
    public class ProductAttributeManagementController :BaseController
    {
        //
        // GET: /Administrator/ProductAttributeManagement/

        public ProductAttributeManagementController(IProductAttributeActionService _productattribute,
            IConfigurationActionService _config) : base(_productattribute,_config) {
                try
                {
                    Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.PRODUCTATTRIBUTE)).Role;
                }
                catch { }
        }

       

        #region Main

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll(long productid)
        {
            var data = ProductAttribute.GetListByProductID(productid);            
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
            return View(id);
        }

        public ActionResult _AddOrUpdate(int _id=0)
        {
            var data = ProductAttribute.GetByID(_id);
            if (data == null)
                data = new ProductAttribute();
                data.ListAttributeExt = ProductAttribute.GetListAttribute(int.Parse(data.AttributeID.ToString()));
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdate(CoreData.ProductAttribute model, HttpPostedFileBase file)
        {
            if (PathUpload != null && PathUpload != "")
                model.Value = PathUpload;
            if (model.Value == null || model.Value == "")
                return Content(ErrorCode.Null);
            model.ProductID = SessionManagement.GetSessionReturnInt("ProductID");
            var id = this.ProductAttribute.Save(model);
            PathUpload = "";
            if (id > 0)
            {
                return Content(ErrorCode.OK);
            }
            else {
                return Content(ErrorCode.Error);
            }
            
        }


        public ActionResult Delete(int id)
        {
            if(this.ProductAttribute.Delete(this.ProductAttribute.GetByID(id)))
                return Content(ErrorCode.OK);
            return Content(ErrorCode.Error);
        }

        #endregion

    }
}
