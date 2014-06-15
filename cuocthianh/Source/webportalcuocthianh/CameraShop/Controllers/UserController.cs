using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
using Helper;

namespace CameraShop.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IUserActionService _user, IProvinceActionService _province, IEmailListActionService emaillist, IExamineeActionService examinee)
            : base(_user, _province,emaillist,examinee)
        { }
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile() {
            //if (Session["UserName"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else {
            //    var username = Session["UserName"].ToString();
            //    var data = UserService.GetByUserName(username);
            //    return View(data);
            //}
            return View();
        }

        public ActionResult _ProfileInfor() {
            var username = Session["UserName"].ToString();
            var data = UserService.GetByUserName(username);
            if (data == null)
                data = new User();
            data.ListProvinceExt = UserService.GetListProvince((int.Parse(data.ProvinceIDExt.ToString())));
            return PartialView(data);
        }

        public ActionResult UpdateInfor(User _model) {
            try
            {
                var userid = Convert.ToInt32(Session["UserID"]);
                var data = UserService.GetByID(userid);
                
                if (data != null)
                {
                    _model.ID = userid;
                    if (Security.EncryptString(_model.TempPassWordExt) == data.Password)
                    {
                        this.UserService.Save(_model);
                    }
                    else if(_model.Password == "" || _model.TempPassWordExt==""){
                        this.UserService.Save(_model);
                    }
                    //trong profile thêm các html control để luu lại thông tin người dự thi
                    ExamineeService.Save(_model.ExamineeExt);
                }
                return RedirectToAction("Profile", "User");
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Navigation Dropdown Account on header
        /// </summary>
        /// <returns></returns>
        public ActionResult _NavigationAccount() {
            return PartialView();

        }



       

    }
}
