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
    public class PictureExamController : BaseController
    {

        public PictureExamController(IPictureExamActionService pictureexaminee) : base(pictureexaminee) { }

        //
        // GET: /PictureExam/



        public ActionResult Index()
        {
            return View();
        }


        /*
        * Action and view for Picture Exam 
        * 
        * */

        //Picture Exam
        //action
        public ActionResult PictureExam(PictureExam _model)
        {

            return View();
        }

        //Picture Exam
        //view
        public ActionResult _PictureExam()
        {
            var userid = Convert.ToInt32(Session["UserID"]);
            var data = PictureExamineeService.GetListByUserID(userid);
            return PartialView(data);
        }

    }
}
