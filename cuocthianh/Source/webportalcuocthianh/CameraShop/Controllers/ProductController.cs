using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;

namespace CameraShop.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        public ProductController(IProductActionService _product,
            ICategoryActionService _category, IModelActionService _model,
            IManufacturerActionService _manufacturer, ISearchActionService _search, IConfigurationActionService _config, IProductAttributeActionService _attribute, IAttributeActionService _att)
            :base(_product,_category,_model,_manufacturer, _search,_config,_attribute,_att)
        { }
       static List<Product> ProductViewed = new List<Product>();
       static string name = "";
       static string manufac = "0";
       static string price = "0";

        /// <summary>
        /// Hiển thị tất cả sản phẩm 
        /// Phân trang sản phẩm
        /// Chọn kích thước trang ( PageSize)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int p = 1, int PageSize = 20, FormCollection frm=null)
        {
     
            SetupSearch(frm);
            IList<Product>  model = ProductService.GetBySearch(name, price, manufac,"","","productall");
            if (model != null && model.Count <= PageSize)
                p = 1;
            ViewBag.CurrentPage = p;
            ViewBag.PageSize = PageSize;
            int pagesize = (int)ViewBag.PageSize;
            ViewBag.TotalPages = Convert.ToInt32(Math.Ceiling((double)model.Count() / (int)ViewBag.PageSize));
            return View(model.OrderByDescending(x => x.ID).Skip((p - 1) * pagesize).Take(pagesize).ToList());
        }

        public ActionResult _ViewedProduct() {
            return PartialView();
        }
        /// <summary>
        /// Hiển thị chi tiết sản phẩm
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(long Id) {
            string link = "";
            var product = ProductService.GetByID(Id);
            if (Session["ListViewedProduct"] == null)
            {
                ProductViewed.Clear();
            }

            var existP = ProductViewed.Where(c => c.ID == Id);
            if (existP.Count() == 0)
            {
                ProductViewed.Add(product);
                Session["ListViewedProduct"] = ProductViewed;
            }

            var category = CategoryService.GetByID(product.CateID);
            var manufactuer = ManufacturerService.GetByID(product.ManufacturerID);
            var attribute = ProductAttribute.GetListByProductID(Id);
            Product model = new Product();
            model = product;
            model.ManufactuerExt = manufactuer;
            model.CategoryNameExt = category.Name;
            model.ProductAttributeExt = attribute;
            model.View = model.View + 1;
            ProductService.Save(model, ref link);
            return View(model);
        }
        /// <summary>
        /// Breadcrumb cho phan danh muc va san pham
        /// </summary>
        /// <param name="Id">Id cua danh muc con cuoi cung dung de de quy ra cac thang cha</param>
        /// <param name="name">Ten cua breadcrumb vi du như khi show san pham se co ten san pham o cuoi, danh muc ko can</param>
        /// <returns></returns>
        public ActionResult _BreadCrumb(long Id, string name) {
            Product model = new Product();
            //Get category by Id dung de tra ve 
            var category = CategoryService.GetByID(Id);
            //Get toan bo category
            var Listcategory = CategoryService.GetList();
            model.CategoryNameExt = category.Name;
            model.CategoryListExt = Listcategory;
            model.CateID = category.ID;
            model.Name = name;
            return PartialView(model);
        }
        /// <summary>
        /// Hiển thị sản phẩm mới
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult NewProductsPartial()
        {
            var product = ProductService.GetListByLINQ(x=>x.New &&x.ShowHomePage && x.Active).OrderByDescending(x=>x.ID).Take(4);
            return PartialView("_NewProductsPartial", product);
        }
        /// <summary>
        /// Hiển thị sản phẩm nổi bật
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult FeatureProducts()
        {
            var product = ProductService.GetListByLINQ(x => x.ShowHomePage && x.Typical && x.Active);
            return PartialView("_FeatureProducts", product);
        }

        /// <summary>
        /// Lấy danh sách sản phẩm theo nhà cung cấp
        /// Phân trang sản phẩm
        /// Chọn số sản phẩm hiển thị trên 1 trang.
        /// </summary>
        /// <param name="p">Số trang</param>
        /// <param name="PageSize">Kích thước trang</param>
        /// <param name="Id">Mã nhà sản xuất</param>
        /// <returns>View ListProduct</returns>
        public ActionResult ListProduct(int p = 1, int PageSize = 20, int Id = 0, FormCollection frm = null)
        {

            if (Id == 0)
                return View("Error");
            var manu = ManufacturerService.GetByID(Id);
            if (manu == null)
                return View("Error");
           
            SetupSearch(frm);
            IList<Product> model = ProductService.GetBySearch(name, price, manufac, "", manu.ID.ToString(), "Manufac");
            if (model != null && model.Count <= PageSize)
                p = 1;
            ViewBag.CurrentPage = p;
            ViewBag.PageSize = PageSize;
            int pagesize = (int)ViewBag.PageSize;
            ViewBag.CatName = manu.Name;
            ViewBag.Id = manu.ID;
            ViewBag.TotalPages = Convert.ToInt32(Math.Ceiling((double)model.Count() / (int)ViewBag.PageSize));

            return View(model.OrderByDescending(x => x.ID).Skip((p - 1) * pagesize).Take(pagesize).ToList());
        }

        /// <summary>
        /// Sản phẩm liên quan
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult RelatedProduct(int Id)
        {
            List<Product> model = new List<Product>();
            if (Id > 0)
            {
                model = ProductService.GetListByLINQ(x => x.Active && x.CateID == Id).OrderByDescending(x => Guid.NewGuid()).ToList();
            }
            else {
                model = ProductService.GetListByLINQ(x => x.Active).ToList();
            }
            return PartialView("_RelatedProduct", model);
        }



        void SetupSearch(FormCollection frm)
        {
            if (frm.Count != 0)
            {
                name = frm["txtname"];
                manufac = frm["searchmanufac"];
                price = frm["searchprice"];
            }

           //ProductService.GetListByLINQ(x=>x.Active);
            if (string.IsNullOrEmpty(frm["searchmanufac"]))
            {
                frm["searchmanufac"] = manufac;
            }
            if (string.IsNullOrEmpty(frm["searchprice"]))
                frm["searchprice"] = price;
            try
            {
                var testprice = int.Parse(frm["searchprice"]);
            }
            catch { frm["searchprice"] = "0"; }
            try
            {
                var testmenufac = int.Parse(frm["searchmanufac"]);
            }
            catch { frm["searchmanufac"] = "0"; }
            ViewBag.Manufacturer = ProductService.GetListManufacturer(int.Parse(frm["searchmanufac"]));
            ViewBag.GetListPrice = ProductService.GetListSearchPrice(int.Parse(frm["searchprice"]));
        }

        public ActionResult BestSellerPartial()
        {
            var model = ProductService.GetListByLINQ(x => x.Active&&x.SoleAmout!=0).OrderByDescending(x => x.SoleAmout).Take(5);
            return PartialView("_BestSellerPartial",model);
        }
        /// <summary>
        /// Sản phẩm nổi bật
        /// dùng Partialview
        /// </summary>
        /// <param name="p"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public ActionResult BestSellers(int p = 1, int PageSize = 20, FormCollection frm=null)
        {
            SetupSearch(frm);
            IList<Product> model = ProductService.GetBySearch(name, price, manufac, "", "", "");
            //IEnumerable<Product> model = ProductService.GetListByLINQ(x => x.Active&&x.SoleAmout!=0).OrderByDescending(x => x.SoleAmout);
            if (model != null)
            {
                model = model.Where(c => c.SoleAmout != 0).OrderByDescending(c => c.SoleAmout).ToList();
                if (model.Count() <= PageSize)
                     p = 1;
            }
            ViewBag.CurrentPage = p;
            ViewBag.PageSize = PageSize;
            int pagesize = (int)ViewBag.PageSize;
            ViewBag.TotalPages = Convert.ToInt32(Math.Ceiling((double)model.Count() / (int)ViewBag.PageSize));
            return View(model.OrderByDescending(x => x.ID).Skip((p - 1) * pagesize).Take(pagesize).ToList());
        }

        /// <summary>
        /// Sản phẩm mới 
        /// Dùng View
        /// </summary>
        /// <param name="p"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public ActionResult NewProducts(int p = 1, int PageSize = 20, FormCollection frm= null)
        {
            SetupSearch(frm);
            IList<Product> model = ProductService.GetBySearch(name, price, manufac, "", "", "");

            if (model != null)
            {
                model = model.OrderByDescending(c => c.ID).ToList();
                if( model.Count() <= PageSize)
                    p = 1;
            }
            ViewBag.CurrentPage = p;
            ViewBag.PageSize = PageSize;
            int pagesize = (int)ViewBag.PageSize;
            ViewBag.TotalPages = Convert.ToInt32(Math.Ceiling((double)model.Count() / (int)ViewBag.PageSize));
            return View(model.OrderByDescending(x => x.ID).Skip((p - 1) * pagesize).Take(pagesize).ToList());
        }

        /// <summary>
        /// Sản phẩm theo danh mục 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="PageSize"></param>
        /// <param name="Id"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        /// <param name="Manufacturer"></param>
        /// <returns></returns>
        public ActionResult ProductCategory(int page = 1, int pageSize = 20, int Id = 0, FormCollection frm = null)
        {

            if (Id == 0)
                return View("Error");
            var cat = CategoryService.GetByID(Id);
            if (cat == null)
                return View("Error");

            List<Product> model = ProductService.GetBySearch(name, price, manufac, cat.ID.ToString(), "", "Category").ToList();
            //Nhằm lấy luôn của cả category con
            var LstCatParent = CategoryService.GetListByLINQ(c => c.ParentID == cat.ID);
            foreach (var item in LstCatParent)
            {
                var p = ProductService.GetListByLINQ(c => c.CateID == item.ID);
                if (p != null && p.Count() != 0)
                {
                    model.AddRange(p);
                }
                AddChildProduct(item.ID, model);
            }
            foreach (var item in model)
            {
                item.CategoryExt = new Category();
                item.CategoryNameExt = cat.Name;
                item.CategoryExt.Link = cat.Link;
                item.pageExt = page;
                item.PageSizeExt = pageSize;
                item.TotalPageExt = (int)Math.Ceiling((float)model.Count / (float)item.PageSizeExt);
            }
            
            model = model.OrderByDescending(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (model.Count == 0) {
                ViewBag.Title = cat.Name;
                ViewBag.CatId = cat.ID;
            }
            return View(model);
        }
        /// <summary>
        /// Show list product by Category Id ----- Đáng ra nên viết trong servie nhưng làm biếng quá
        /// </summary>
        /// <param name="CatId">Category Id</param>
        /// <returns></returns>
        public ActionResult _ShowProductByCategory(int CatId) {
            List<Product> model = ProductService.GetListByCategory(CatId).ToList();
            var LstCatParent = CategoryService.GetListByLINQ(c => c.ParentID == CatId);
            foreach (var item in LstCatParent)
            {
                var p = ProductService.GetListByLINQ(c => c.CateID == item.ID);
                if (p != null && p.Count() != 0)
                {
                    model.AddRange(p);
                }
                AddChildProduct(item.ID, model);
            }
            Category category = CategoryService.GetByID(CatId);
            model.First().CategoryExt = new Category();
            model.First().CategoryExt.Link = category.Link;
            return PartialView(model);
        }
        public void AddChildProduct(long categoryid, List<Product> _listproduct)
        {
            var LstCatChild = CategoryService.GetListByLINQ(c => c.ParentID == categoryid);
            if (LstCatChild != null && LstCatChild.Count() != 0)
            {
                foreach (var item in LstCatChild)
                {
                    var p = ProductService.GetListByLINQ(c => c.CateID == item.ID);
                    if (p != null && p.Count() != 0)
                    {
                        _listproduct.AddRange(p);
                    }
                    AddChildProduct(item.ID, _listproduct);
                }
            }
        }
    }
}
