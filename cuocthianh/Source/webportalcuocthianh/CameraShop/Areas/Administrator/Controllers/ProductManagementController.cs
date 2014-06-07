using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;
using System.Threading;
namespace CameraShop.Areas.Administrator.Controllers
{
    public class ProductManagementController : BaseController
    {
        //
        // GET: /Administrator/ProductManagement/

        public ProductManagementController(IProductActionService _product, 
            IProductAttributeActionService _attribute, 
            ICategoryActionService _category, IModelActionService _model,
            IManufacturerActionService _manufacturer, ISearchActionService _search, IConfigurationActionService _config,
            IEmailTemplateActionService _emailtemplate, IEmailListActionService _emailist)
            : base(_product, _category, _model, _manufacturer, _search, _config, _attribute, _emailtemplate, _emailist)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.PRODUCT)).Role;
            }
            catch { }
        }

        static Thread ThreadImportMail;

        #region Main

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll()
        {
            var data = ProductService.GetList();
            return PartialView(data);
        }

        //public ActionResult _ShowAllAttribute(int productID)
        //{
 
        //}


        #endregion


        #region CRUD
        /// <summary>
        /// view
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrUpdate(int id = 0)
        {
            SessionManagement.SetSesionValue("ProductID",id.ToString());
            return CanUpdate((int)id);
        }

        public ActionResult _AddOrUpdate(int id)
        {
            var data = ProductService.GetByID(id);
            if (data == null)
                data = new Product();
            data.ListManufacturerExt = ProductService.GetListManufacturer((int.Parse(data.ManufacturerID.ToString())));
            data.ListModelExt = ProductService.GetListModel((int.Parse(data.ModelID.ToString())));
            data.ListProductTypeExt = ProductService.GetListProductType((int.Parse(data.ProductTypeID.ToString())));
            data.ListCategoryExt = ProductService.GetListCategory((int.Parse(data.CateID.ToString())));
            data.ListEmailTemplateExt = EmailTemplateService.GetListEmailTemplate();
            return PartialView(data);
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrUpdate(Product model, HttpPostedFileBase file)
        {
            model.Image = PathUpload;
            var exist = this.ProductService.GetOneByLINQ(c => c.Name.Equals(model.Name));
            if(exist!=null&&model.ID==0)
                {
                  return Content("exist");
                }
            string link = "";
            var id = this.ProductService.Save(model, ref link);
            PathUpload = "";
            var data = this.ProductService.GetByID(id);
            if (model.IsSendMailExt)
            {
                if (ThreadImportMail != null)
                {
                    ThreadImportMail.Abort();
                 
                }
                   ThreadImportMail = new Thread(new ThreadStart(()=>ImportMail(model.EmailTemplateIDExt,int.Parse(id.ToString()),model.Code,model.Name,link,model.Price.ToString())));
                   ThreadImportMail.Start();
                 
            }
            SessionManagement.SetSesionValue("ProductID", id.ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        void ImportMail(int template,int productid,string code, string name, string link, string price)
        {
            try
            {
                var email = EmailTemplateService.GetByID(template);
                var emaillist = EmailListService.GetList();
                foreach (var item in emaillist)
                {

                    item.TitleExt = "Camera shop - Giới thiệu sản phẩm mới";
                    item.BodyExt = email.Template.Replace("{code}", code).Replace("{product}", name).Replace("{link}", Helper.StringHelper.GetHost() + "/" + link + "-p" + productid + ".html").Replace("{price}", String.Format("{0:0,0 vnđ}", price));
                    MvcApplication.EmailQueue.Add(item);
                }
            }
            catch { }
        }



        public ActionResult Delete(int id)
        {
            this.ProductService.Delete(this.ProductService.GetByID(id));
            return RedirectToAction("Index", "ProductManagement");
        }

        #endregion



        #region Search

        public ActionResult _Search()
        {
            var model = new Product();
            model.ListProductTypeExt = ProductService.GetListProductType(int.Parse(model.ProductTypeID.ToString()));
            model.ListCategoryExt = ProductService.GetListCategory(int.Parse(model.CateID.ToString()));
            model.ListManufacturerExt = ProductService.GetListManufacturer(int.Parse(model.ManufacturerID.ToString()));
            return PartialView(model);
        }

        public ActionResult Search(string code, string name,string producttype, string manufacturer, string category)
        {
            var data = SearchService.SearchProduct(code, name, producttype, manufacturer,category);
            return PartialView("_ShowAll", data);
        }
        #endregion

    }
}
