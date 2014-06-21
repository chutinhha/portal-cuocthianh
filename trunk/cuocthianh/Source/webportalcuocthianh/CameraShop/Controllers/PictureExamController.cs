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
        //xu ly up anh/ hien thi anh 
        public PictureExamController(IPictureExamActionService pictureexaminee, IExamineeActionService ex) : 
            base(pictureexaminee, ex) { }

        //
        // GET: /PictureExam/



        public ActionResult Index()
        {
            return View();
        }



        #region Hinh anh
        /// <summary>
        /// Get detail by UserID
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(int userid=0) {
            var data = PictureExamineeService;
            return View(data);
        }

        /// <summary>
        /// Khung giới thiệu album ảnh
        /// </summary>
        /// <param name="examineeid"></param>
        /// <returns></returns>
        public ActionResult _IntroducePictureExamProfile()
        {
            var users = SessionManagement.GetSessionReturnInt("UserID");
            var data = ExamineeService.GetOneByLINQ(c=>c.UserID.Equals(users));
            return PartialView(data);
        }

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
            
            return PartialView();
        }
        /// <summary>
        /// hien thi danh sach hinh anh tron profile
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult _ListPictureExamProfile()
        {
            var userid =  Convert.ToInt32(Session["UserID"]);
            var data = PictureExamineeService.GetListByUserID(userid);
            return PartialView(data);
        }

        //Picture Exam
        //view
        public ActionResult _ListPictureExam(int userid=0)
        {
            if (userid == 0)
            {
                userid = Convert.ToInt32(Session["UserID"]);
            }
             var data = PictureExamineeService.GetListByUserID(userid);
             return PartialView(data);
        }

        public ActionResult Delete(int id = 0) {
            var picex = PictureExamineeService.GetByID(id);
            var picname = picex.Image;
            if (PictureExamineeService.Delete(picex)==true){
                string fullPath = Request.MapPath("~/Media/PictureExam/" + picex.Image);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return PartialView("_ListPictureExamProfile");
        }

        #region upload control

        //uoload control
        public ActionResult UploadPicture(PictureExam model, HttpPostedFileBase file)
        {
           
            model.Image = PathUpload;
            model.ExamineeID = int.Parse(Session["ExamineeID"].ToString());
            var id = this.PictureExamineeService.Save(model);
            PathUpload = "";
            return Json(id, JsonRequestBehavior.AllowGet);
        }
                    

        #endregion


        #endregion

    }
}
