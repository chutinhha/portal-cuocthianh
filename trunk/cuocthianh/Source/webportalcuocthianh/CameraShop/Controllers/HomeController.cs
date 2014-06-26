using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;
using System.Text.RegularExpressions;
using Facebook;
using System.Configuration;
namespace CameraShop.Controllers
{
   
    public class HomeController : BaseController
    {
        //
        // GET: /Home/


        public HomeController(IUserActionService _user, IProvinceActionService _province,
            IEmailListActionService _emaillist, IExamineeActionService examinee, IConfigurationActionService config)
            : base(_user, _province, _emaillist, examinee, config)
        { }
        public ActionResult Index()
        {
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
            ViewBag.Title = "Jbart Academy - cuộc thi ảnh";
           // var data = UserService.GetList();


            //if (SessionManagement.GetSessionReturnToString("loginFBmode") != null)
            //{
            //    Session["loginFBmode"] = null;
            //    return RedirectToAction("Profile", "User");
            //}


            //if (Request.QueryString["code"] != null)
            //{
            //  //  return  Redirect("http://cuocthijba.com/User/Profile");
            //    return Redirect("http://localhost:1655/User/Profile");
            //}
            if (SessionManagement.GetSessionReturnToString("loginFBmode") != null)
            {
                var redirectUri = new UriBuilder(Request.Url);
                redirectUri.Path = Url.Action("Index", "Home");
                try
                {
                    string SecId = ConfigurationManager.AppSettings["Facbook_SecID"].ToString();
                    string AppId = ConfigurationManager.AppSettings["Facebook_AppID"].ToString();///
                    var client = new FacebookClient();
                    var oauthResult = client.ParseOAuthCallbackUrl(Request.Url);
                  
                    dynamic result = client.Get("/oauth/access_token", new
                    {
                        client_id = AppId,
                        redirect_uri = redirectUri.Uri.AbsoluteUri,
                        client_secret = SecId,
                        code = oauthResult.Code,
                    });
                    // Read the auth values
                    redirectUri.Path = result.redirect_uri;
                    string accessToken = result.access_token;
                    Session["access_token"] = accessToken;
                    DateTime expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(result.expires));
                    // Get the user's profile information
                    dynamic me = client.Get("/me",
                                            new
                                            {
                                                fields = "first_name,last_name,email",
                                                access_token = accessToken
                                            });

                    User u = new CoreData.User();
                    long fbiduser = long.Parse(me.id);
                    var userFBexist = UserService.GetOneByLINQ(c => c.FacebookUserID.Equals(fbiduser));
                    if (userFBexist == null)
                    {
                        u.Active = true;
                        u.UserName = me.first_name;
                        u.Name = me.last_name + " " + me.first_name;
                        u.Password = "daylauserfbma";
                        u.Email = me.email;
                        u.GroupIDExt = 1;
                        u.FacebookUserID = fbiduser;
                        var uid = UserService.Save(u);
                        Session["access_token"] = result.access_token;
                        Session["UserName"] = u.UserName;
                        Session["UserID"] = uid;
                        Session["UserGroup"] = 1;
                        Session["ExamineeID"] = ExamineeService.GetOneByLINQ(c => c.UserID.Equals(uid)).ID;
                        Session["loginFBmode"] = null;
                    }
                    else
                    {
                        Session["access_token"] = result.access_token;
                        Session["UserName"] = userFBexist.UserName;
                        Session["UserID"] = userFBexist.ID;
                        Session["UserGroup"] = 1;
                        Session["ExamineeID"] = ExamineeService.GetOneByLINQ(c => c.UserID.Equals(userFBexist.ID)).ID;
                        Session["loginFBmode"] = null;
                    }
                }
                catch { Session["loginFBmode"] = null; }

                return RedirectToAction("Profile","User");
            }


            //HttpContext..Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            //HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            //HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //HttpContext.Current.Response.Cache.SetNoStore();

            return View();
        }
        public ActionResult _Header()
        {
            ViewBag.Logo = ConfigurationService.GetOneByLINQ(c => c.Code == "Logo").Value;
           
            return PartialView();
        }

        /// <summary>
        /// Register View
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(int id = 0)
        {
            if (SessionManagement.GetSessionReturnToString("UserName") != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        /// <summary>
        /// Register action
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User _model)
        {
            try
            {
                if (string.IsNullOrEmpty(_model.UserName))
                {
                    ModelState.AddModelError("", ErrorCode.UserNameNull);
                }
                if (string.IsNullOrEmpty(_model.Name))
                {
                    ModelState.AddModelError("", "Chưa nhập tên");
                }
                if (string.IsNullOrEmpty(_model.Password))
                {
                    ModelState.AddModelError("", ErrorCode.PasswordNull);
                }
                if (string.IsNullOrEmpty(_model.Address))
                {
                    ModelState.AddModelError("", ErrorCode.AddressNull);
                }
                if (_model.Password != _model.TempPassWordExt)
                {
                    ModelState.AddModelError("", ErrorCode.differentPassword);
                }
                if (UserService.GetByUserName(_model.UserName) != null)
                {
                    ModelState.AddModelError("", ErrorCode.existUserName);
                }
                if (UserService.GetByEmail(_model.Email) != null)
                {
                    ModelState.AddModelError("", ErrorCode.existEmail);
                }
                if (_model.Email != null && Regex.IsMatch(_model.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z") == false)
                {
                    ModelState.AddModelError("", ErrorCode.NotValidEmail);
                }
                if (_model.Phone != null && Regex.IsMatch(_model.Phone, @"\d+") == false)
                {
                    ModelState.AddModelError("", ErrorCode.NotValidPhoneNumber);
                }
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count() != 0)
                    {
                        return View(_model);
                    }
                }
                _model.Active = true;
                _model.GroupIDExt = 1;
                string pass = _model.Password;
                long userid =this.UserService.Save(_model);
                _model.Password = pass;
                int exmineeid = 0;
                if (userid != -1)
                {

                    if (UserService.Login(_model, ref Username, ref Userid, ref Groupid, ref exmineeid) == true)
                    {
                        Session["UserName"] = Username;
                        Session["UserID"] = Userid;
                        Session["UserGroup"] = Groupid;
                        Session["ExamineeID"] = exmineeid;
                        return RedirectToAction("Profile", "User");
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", ErrorCode.Error);
                return View(_model);
            }
            catch
            {
                ModelState.AddModelError("", ErrorCode.UserNameNull);
                return View(_model);
            }
        }


       
        public ActionResult _LoginFacebookButton()
        {
            return PartialView();
        }

        //login Fb action
        public ActionResult LoginFB()
        {
              string SecId = ConfigurationManager.AppSettings["Facbook_SecID"].ToString();
              string AppId = ConfigurationManager.AppSettings["Facebook_AppID"].ToString();
              var redirectUri = new UriBuilder(Request.Url);
              redirectUri.Path = Url.Action("Index", "Home");
            var client = new FacebookClient();
            var uri = client.GetLoginUrl(new
            {
                client_id = AppId,
                redirect_uri = redirectUri.Uri.AbsoluteUri
            });
            SessionManagement.SetSesionValue("loginFBmode", "FB");
            return Redirect(uri.ToString());
        }

        /// <summary>
        /// Contact page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }
        /// <summary>
        /// Menu on top header
        /// </summary>
        /// <returns></returns>
        public ActionResult _TopMenu() {
            return PartialView();
        }

        /// <summary>
        /// Phan thong tin lien he dropdown tren header
        /// </summary>
        /// <returns></returns>
        public ActionResult _TopInforContact() {
            return PartialView();
        }
        [HttpPost]
        public ActionResult LoginHome(string Username="",string Password="")
        {
            User _user = new User();
            _user.UserName = Username;
            _user.Password = Password;
            int exmineeid = 0;
            if (string.IsNullOrEmpty(_user.UserName))
            {
                ModelState.AddModelError("", ErrorCode.UserNameNull);
            }
            if (string.IsNullOrEmpty(_user.Password))
            {
                ModelState.AddModelError("", ErrorCode.PasswordNull);
            }
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count() != 0)
                {
                    return Content("Tài khoản hoặc mật khẩu bị trống");
                 //   return View(_user);
                }
            }
            if (UserService.Login(_user, ref Username, ref Userid, ref Groupid, ref exmineeid) == true)
            {
                Session["UserName"] = Username;
                Session["UserID"] = Userid;
                Session["UserGroup"] = Groupid;
                Session["ExamineeID"] = exmineeid;
                return Content("OK");
               // return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["UserName"] = null;
                Session["UserGroup"] = null;
                Session["UserID"] = null;
                Session["ExamineeID"] = null;
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return Content("Tài khoản hoặc mật khẩu không đúng");
                // return View(_user);
            }
        }

        
        public ActionResult LoginHome(User _user, string returnurl)
        {
            int exmineeid = 0;
            if (string.IsNullOrEmpty(_user.UserName))
            {
                ModelState.AddModelError("", ErrorCode.UserNameNull);
            }
            if (string.IsNullOrEmpty(_user.Password))
            {
                ModelState.AddModelError("", ErrorCode.PasswordNull);
            }
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count() != 0)
                {
                    return View(_user);
                }
            }
            if (UserService.Login(_user, ref Username, ref Userid, ref Groupid, ref exmineeid) == true)
            {
                Session["UserName"] = Username;
                Session["UserID"] = Userid;
                Session["UserGroup"] = Groupid; Session["ExamineeID"] = exmineeid;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["UserName"] = null;
                Session["UserGroup"] = null;
                Session["UserID"] = null; Session["ExamineeID"] = null;
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return View(_user);
            }
        }

        public ActionResult LogOffHome()
        {
            Session["UserName"] = null;
            Session["UserGroup"] = null;
            Session["UserID"] = null;
            
            Session["ExamineeID"] = null;
            Userid = 0;
            if (SessionManagement.GetSessionReturnToString("loginFBmode") != null)
            {
               
                var oauth = new FacebookClient();
                var logoutParameters = new Dictionary<string, object>
                  {
                     {"access_token",  Session["access_token"]},
                      { "next", Url.Action("Index","Home") }
                  };
                Session["loginFBmode"] = null;
                Session["access_token"] = null;
                var logoutUrl = oauth.GetLogoutUrl(logoutParameters);
                return Redirect(logoutUrl.ToString());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Bạn chưa nhập email.");
                return View();
            }
            var user = UserService.GetByEmail(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email bạn nhập sai định dạng hoặc không có trong hệ thống.");
                return View();
            }
            user.Password = "123456";
            long stt = UserService.Save(user);
            //    Kiểm tra status  + Hàm gởi mail ở đây.
            if (stt > 0)
            {
                EmailList _email = new EmailList();
                _email.Email = email;
                _email.TitleExt = "Camerashop - Phục hồi mật khẩu";
                _email.BodyExt = "Mật khẩu mới của bạn là: 123456, vui lòng thay đổi mật khẩu sau khi đăng nhập";
                MvcApplication.EmailQueue.Add(_email);
            }
            return View();
        }

        public ActionResult RegisterEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return Content("Email không được rỗng");
            }
            var check = EmailListService.GetOneByLINQ(c => c.Email.Equals(email));
            if (check != null)
            {
                return Content("Email này đã tồn tại trong hệ thống của chúng tôi");
            }
            EmailList _email = new EmailList()
            {
                Email = email

            };
            if (EmailListService.Save(_email) > 0)
            {
                return Content("Bạn đã đăng ký email thành công");
            }
            return Content("Lỗi, vui lòng thử lại");
        }
        /// <summary>
        /// Footer PartialView
        /// </summary>
        /// <returns></returns>
        public ActionResult _Footer() {
            return PartialView();
        }
    }
}
