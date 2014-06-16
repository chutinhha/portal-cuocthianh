using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;
using System.Text.RegularExpressions;

namespace CameraShop.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/


        public HomeController(IUserActionService _user, IProvinceActionService _province, IEmailListActionService _emaillist)
            : base(_user, _province, _emaillist)
        { }
        public ActionResult Index()
        {
            var data = UserService.GetList();
            return View();
        }
        public ActionResult _Header()
        {

            return PartialView();
        }

        /// <summary>
        /// Register View
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(int id = 0)
        {
            if (Userid != 0)
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
                if (_model.Phone != null && Regex.IsMatch(_model.Phone, @"0\d{9,10}") == false)
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

                if (this.UserService.Save(_model) != -1)
                    return RedirectToAction("LoginHome", "Home");
                ModelState.AddModelError("", ErrorCode.Error);
                return View(_model);
            }
            catch
            {
                ModelState.AddModelError("", ErrorCode.UserNameNull);
                return View(_model);
            }
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
            if (UserService.Login(_user, ref Username, ref Userid, ref Groupid) == true)
            {
                Session["UserName"] = Username;
                Session["UserID"] = Userid;
                Session["UserGroup"] = Groupid;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["UserName"] = null;
                Session["UserGroup"] = null;
                Session["UserID"] = null;
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return View(_user);
            }
        }

        
        public ActionResult LoginHome(User _user, string returnurl)
        {
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
            if (UserService.Login(_user, ref Username, ref Userid, ref Groupid) == true)
            {
                Session["UserName"] = Username;
                Session["UserID"] = Userid;
                Session["UserGroup"] = Groupid;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["UserName"] = null;
                Session["UserGroup"] = null;
                Session["UserID"] = null;
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return View(_user);
            }
        }

        public ActionResult LogOffHome()
        {
            Session["UserName"] = null;
            Session["UserGroup"] = null;
            Session["UserID"] = null;
            Userid = 0;
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
