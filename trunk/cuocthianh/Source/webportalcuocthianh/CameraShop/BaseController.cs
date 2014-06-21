using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;
using System.IO;

namespace CameraShop
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public static string PathUpload;
        public static string Username;
        public static int Userid;
        public static int Groupid;
        public static int OrderID;
        public static string Permission="";
        public static List<CoreData.Permission> ListPermission;

        public readonly IUserActionService UserService;
        public readonly IBanner_LogoActionService BannerService;
        public readonly ICategoryActionService CategoryService;
        public readonly IManufacturerActionService ManufacturerService;
        public readonly IModelActionService ModelService;
        public readonly IGroupActionService GroupService;
        public readonly IProductActionService ProductService;
        public readonly IProductAttributeActionService ProductAttribute;
        public readonly IOrderActionService OrderService;
        public readonly IOrderDetailActionService OrderDetailService;
        public readonly IAttributeActionService AttributeService;
        public readonly IProvinceActionService ProvinceService;
        public readonly ISearchActionService SearchService;
        public readonly IArticleActionService ArticleService;
        public readonly IConfigurationActionService ConfigurationService;
        public readonly IDistrictActionService DistrictService;
        public readonly IUser_Role_ModuleActionService UserRoleModuleService;
        public readonly IUser_GroupActionService UserGroupService;
        public readonly IModuleActionService ModuleService;
        public readonly IEmailTemplateActionService EmailTemplateService;
        public readonly ICustomerIdeaActionService CustomerIdeaService;
        public readonly IEmailListActionService EmailListService;
        public readonly IShoppingCartActionService ShoppingCartService;
        public readonly ICategoryArticleActionService CategoryArticleService;
        public readonly IExamineeActionService ExamineeService;
        public readonly IPictureExamActionService PictureExamineeService;
        public readonly ICommentActionService CommentService;

        #region Contructor

        public BaseController() { }

        public BaseController(IManufacturerActionService _manufacturer)
        {
            this.ManufacturerService = _manufacturer;
        }
        public BaseController(IBanner_LogoActionService _banner) {
            this.BannerService = _banner;
        }
        public BaseController(IProductActionService _product)
        {
            this.ProductService = _product;
        }

        public BaseController(IConfigurationActionService _service, IEmailTemplateActionService _email)
        { this.ConfigurationService = _service; this.EmailTemplateService = _email; }
        public BaseController(ISearchActionService _search) { this.SearchService = _search; }
        public BaseController(ISearchActionService _search, ICategoryActionService _category) { this.SearchService = _search; this.CategoryService = _category; }
        public BaseController(IUserActionService _user, IUser_GroupActionService usergroup,
            IUser_Role_ModuleActionService rolemodule, IModuleActionService module) 
        { 
            this.UserService = _user;
            this.UserGroupService = usergroup;
            this.UserRoleModuleService = rolemodule;
            this.ModuleService = module;
        }

        public BaseController(IUserActionService _user, IProvinceActionService _province, IEmailListActionService _emaillist,
            IExamineeActionService _examinee) {
            this.UserService = _user;
            this.ProvinceService = _province;
            this.EmailListService =_emaillist;
            this.ExamineeService = _examinee;
        }
        public BaseController(IUserActionService _user, IProvinceActionService _province, IEmailListActionService _emaillist,
            IExamineeActionService _examinee, IConfigurationActionService config)
        {
            this.UserService = _user;
            this.ProvinceService = _province;
            this.EmailListService = _emaillist;
            this.ExamineeService = _examinee;
            this.ConfigurationService = config;
        }
        public BaseController(IUserActionService _user, IProvinceActionService _province, IEmailListActionService _emaillist)
        {
            this.UserService = _user;
            this.ProvinceService = _province;
            this.EmailListService = _emaillist;
            //this.ExamineeService = ex;
        }

        public BaseController(IUserActionService _user, IGroupActionService _group, ISearchActionService _search, IUser_Role_ModuleActionService _userrole)
        { 
            this.UserService = _user; this.GroupService = _group;
            this.SearchService = _search;
            this.UserRoleModuleService = _userrole;
        }
        public BaseController(ICategoryActionService _category, IConfigurationActionService _config)
        { this.CategoryService = _category; this.ConfigurationService = _config; }
        public BaseController(IManufacturerActionService _manufacturer, IConfigurationActionService _config) {
            this.ManufacturerService = _manufacturer;
            this.ConfigurationService = _config;
        }
        //for Order +++ NTT +++
        public BaseController(IOrderActionService _order, IOrderDetailActionService _orderdetail, IUserActionService _user
            ,IProductActionService _product) { this.OrderService = _order;
            this.OrderDetailService = _orderdetail;
            this.UserService = _user;
            this.ProductService = _product;
        }

        public BaseController(IModelActionService _model, IManufacturerActionService _manufacturer) { this.ModelService = _model; this.ManufacturerService = _manufacturer; }
        public BaseController(IProductActionService _product, ICategoryActionService _category,
            IModelActionService _model, IManufacturerActionService _manufacturer, ISearchActionService _search,
            IConfigurationActionService _config, IProductAttributeActionService _attribute,
            IEmailTemplateActionService _emailtemplate, IEmailListActionService _emailist)
        {
            this.ProductService = _product;
            this.CategoryService = _category;
            this.ModelService = _model;
            this.ManufacturerService = _manufacturer;
            this.SearchService = _search;
            this.ConfigurationService = _config;
            this.ProductAttribute = _attribute;
            this.EmailTemplateService = _emailtemplate;
            this.EmailListService = _emailist;
        }
        public BaseController(IProductActionService _product, ICategoryActionService _category,
            IModelActionService _model, IManufacturerActionService _manufacturer, ISearchActionService _search,
            IConfigurationActionService _config, IProductAttributeActionService _attribute,IAttributeActionService _att)
        {
            this.ProductService = _product;
            this.CategoryService = _category;
            this.ModelService = _model;
            this.ManufacturerService = _manufacturer;
            this.SearchService = _search;
            this.ConfigurationService = _config;
            this.ProductAttribute = _attribute;
            this.AttributeService = _att;
        }
        public BaseController(IOrderActionService _order)
        {
            this.OrderService = _order;
        }
        public BaseController(IAttributeActionService _attribute)
        { this.AttributeService = _attribute; }
        public BaseController(IProductAttributeActionService _productattribute, IConfigurationActionService _config
            ) { this.ProductAttribute = _productattribute;
            this.ConfigurationService = _config;
            }

        public BaseController(IArticleActionService _article, IUserActionService _user, IConfigurationActionService _config
            ) { this.ArticleService = _article; this.UserService = _user;
            this.ConfigurationService = _config;
            }

        public BaseController(IArticleActionService _article) { this.ArticleService = _article; }
        public BaseController(IProvinceActionService _province) { this.ProvinceService = _province; }

        public BaseController(IDistrictActionService district, IProvinceActionService province)
        {
            this.ProvinceService = province;
            this.DistrictService = district;

                
        }

        public BaseController(ICategoryActionService _category, IManufacturerActionService _manufacturer)
        {
            this.CategoryService = _category;
            this.ManufacturerService = _manufacturer;
        }

        public BaseController(IUserActionService _user, IOrderActionService _order,
            IOrderDetailActionService _orderdetail, IProductActionService _product
            , IEmailListActionService _emaillist,
            IEmailTemplateActionService _mailtemplate)
        {
            this.UserService = _user;
            this.OrderService = _order;
            this.OrderDetailService = _orderdetail;
            this.ProductService = _product;
            this.EmailListService = _emaillist;
            this.EmailTemplateService = _mailtemplate;
        }

        public BaseController(IUserActionService _user, IOrderActionService _order,
            IOrderDetailActionService _orderdetail, IProductActionService _product
            , IEmailListActionService _emaillist,
            IEmailTemplateActionService _mailtemplate, IShoppingCartActionService _shoppingcart)
        {
            this.UserService = _user;
            this.OrderService = _order;
            this.OrderDetailService = _orderdetail;
            this.ProductService = _product;
            this.EmailListService = _emaillist;
            this.EmailTemplateService = _mailtemplate;
            this.ShoppingCartService = _shoppingcart;
        }

        public BaseController(IUserActionService user, IUser_Role_ModuleActionService userrole,
           IModuleActionService module, IGroupActionService group)
        {
            this.UserService = user;
            this.UserRoleModuleService = userrole;
            this.ModuleService = module;
            this.GroupService = group;
        }
        public BaseController(ICustomerIdeaActionService _customeridea, IUserActionService _user) {
            this.CustomerIdeaService = _customeridea;
            this.UserService = _user;
        }
        public BaseController(IProductActionService _product, IShoppingCartActionService _shoppingcart)
        {
            this.ProductService = _product;
            this.ShoppingCartService = _shoppingcart;
        }


        public BaseController(ICategoryArticleActionService _categoryarticle)
        {
            this.CategoryArticleService = _categoryarticle;
           
        }

        public BaseController(IExamineeActionService _examinee) {
            this.ExamineeService = _examinee;
        }

        public BaseController(IPictureExamActionService _pictureexaminee)
        {
            this.PictureExamineeService = _pictureexaminee;
        }

        public BaseController(IPictureExamActionService _pictureexaminee, IExamineeActionService ex)
        {
            this.PictureExamineeService = _pictureexaminee;
            this.ExamineeService = ex;
        }

        public BaseController(ICommentActionService _comment)
        {
            this.CommentService = _comment;
        }


        #endregion

        public ActionResult SetUpSecurity()
        {
            if (Session["UserName"] != null)
            {
                Groupid = int.Parse(Session["UserGroup"].ToString());
            }
            else
            {
                Groupid = 1;

            }
           // if (Enum.IsDefined(typeof(RoleValue), UserGroup))
           // {
                if (Groupid == 1)
                {
                    //logout remove all session
                    Session["UserName"] = null;
                    Session["UserGroup"] = null;
                    Session["UserID"] = null;
                    Session["url"] = Request.Url.AbsoluteUri;
                    return RedirectToAction("Login", "Management", new { returnurl = Request.Url.AbsoluteUri });
                }
                return View();
        }


        [HttpPost]
        public ContentResult UploadFiles(string dir)
        {
            var r = new List<ViewDataUploadFilesResult>();
            string _file = "";
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                if(dir.Contains("Category"))
                {
                   _file = FileHelper.FileUpload(hpf, "Media/Category");
                   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("Categoryimg")).Value;
                   var path = Server.MapPath("~/Media/Category/" + Path.GetFileNameWithoutExtension(_file)+Guid.NewGuid().ToString().Substring(0,4)+Path.GetExtension(_file));
                   var _name = Server.MapPath("~/Media/Category/" + _file);
                   _file = ImageHelper.SaveCompressed(_name, path,long.Parse(value));
                }

                if (dir.Contains("Manufacturer"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/Manufacturer");
                    var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("Manufacturerimg")).Value;
                    var path = Server.MapPath("~/Media/Manufacturer/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/Manufacturer/" + _file);
                    _file = ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                if (dir.Contains("Model"))
                    _file = FileHelper.FileUpload(hpf, "Media/Model");
                if (dir.Contains("Product"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/Product");
                    var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("Poductimg")).Value;
                    var path = Server.MapPath("~/Media/Product/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/Product/" + _file);
                    _file = ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                if (dir.Contains("ProductAttribute"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/ProductAttribute");
                    var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("Poductattributeimg")).Value;
                    var path = Server.MapPath("~/Media/ProductAttribute/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/ProductAttribute/" + _file);
                    _file = ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                if (dir.Contains("Article"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/Article");
                    var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("Articleimg")).Value;
                    var path = Server.MapPath("~/Media/Article/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/Article/" + _file);
                    _file = ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                if (dir.Contains("CustomerIdea"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/CustomerIdea");
                 //   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("CustomerIdeaimg")).Value;
                    var path = Server.MapPath("~/Media/CustomerIdea/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/CustomerIdea/" + _file);
                    _file = Path.GetFileName(_name);// ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                if (dir.Contains("BannerLogo"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/BannerLogo");
                    //   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("CustomerIdeaimg")).Value;
                    var path = Server.MapPath("~/Media/BannerLogo/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/BannerLogo/" + _file);
                    _file = Path.GetFileName(_name);// ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                if (dir.Contains("CateContent"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/CateContent");
                    //   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("CustomerIdeaimg")).Value;
                    var path = Server.MapPath("~/Media/CateContent/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/CateContent/" + _file);
                    _file = Path.GetFileName(_name);// ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }

                if (dir.Contains("ProfileAvatar"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/ProfileAvatar");
                    //   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("CustomerIdeaimg")).Value;
                    var path = Server.MapPath("~/Media/ProfileAvatar/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/ProfileAvatar/" + _file);
                    _file = Path.GetFileName(_name);// ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }

                if (dir.Contains("PictureExam"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/PictureExam");
                    //   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("CustomerIdeaimg")).Value;
                    var path = Server.MapPath("~/Media/PictureExam/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/PictureExam/" + _file);
                    _file = Path.GetFileName(_name);// ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }

                if (dir.Contains("Jbart"))
                {
                    _file = FileHelper.FileUpload(hpf, "Media/Jbart");
                    //   var value = ConfigurationService.GetOneByLINQ(c => c.Code.Equals("CustomerIdeaimg")).Value;
                    var path = Server.MapPath("~/Media/Jbart/" + Path.GetFileNameWithoutExtension(_file) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(_file));
                    var _name = Server.MapPath("~/Media/Jbart/" + _file);
                    _file = Path.GetFileName(_name);// ImageHelper.SaveCompressed(_name, path, long.Parse(value));
                }
                PathUpload = _file;
                r.Add(new ViewDataUploadFilesResult()
                {
                    Name = _file,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });

            }
            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
        }


        public ActionResult Login(string returnurl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User _user, string returnurl)
        {
            int exmineeid = 0;
            if (UserService.Login(_user, ref Username, ref Userid, ref Groupid,ref exmineeid) == true)
            {
                ListPermission = new List<CoreData.Permission>();
                var permiss = UserRoleModuleService.GetListByLINQ(c => c.UserID == Userid);
                foreach (var item in permiss)
                {
                    CoreData.Permission p = new CoreData.Permission();
                    p.GroupID = Groupid;
                    p.Module = ModuleService.GetByID(item.ModuleID).Name;
                    p.Role = item.Role;
                    p.UserID = Userid;
                    ListPermission.Add(p);
                }
                Session["UserName"] = Username;
                Session["UserID"] = Userid;
                Session["UserGroup"] = Groupid;
                Session["ExamineeID"] = exmineeid;
                string decodedUrl = "";
                if(Session["url"]!=null)
                {
                    decodedUrl = Server.UrlDecode(Session["url"].ToString());
                    Session["url"] = null;
                    return Redirect(decodedUrl);
                }
                return RedirectToAction("Index", "Management");
            }
            else
            {
                ListPermission = null;
                Session["UserName"] = null;
                Session["UserGroup"] = null;
                Session["ExamineeID"] = null;
                Session["UserID"] = null;
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return View(_user);
            }
        }


        public ActionResult LogOff()
        {
            ListPermission = null;
            Session["UserName"] = null;
            Session["UserGroup"] = null;
            Session["UserID"] = null;
            Session["ExamineeID"] = null;
            return RedirectToAction("Login", "Management", new { returnurl = Request.Url.AbsoluteUri });
            
        }


        public ActionResult CanUpdate(object model = null)
        {
            Groupid = SessionManagement.GetSessionReturnInt("UserGroup");
            if (Groupid <= 1)
            {
                Session["url"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "Management", new { returnurl = Request.Url.AbsoluteUri });
            }
            return View(model);
        }

        public ActionResult CanView(object model = null)
        {
            Groupid = SessionManagement.GetSessionReturnInt("UserGroup");
            if (Groupid <= 1)
            {
                Session["url"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "Management", new { returnurl = Request.Url.AbsoluteUri });
            }
            return View(model);
        }
    
    }
}
