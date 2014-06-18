﻿using System;
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
        public PictureExamController(IPictureExamActionService pictureexaminee) : base(pictureexaminee) { }

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
        //Picture Exam
        //view
        public ActionResult _ListPictureExam(int userid=0)
        {
            //var userid = 1;// Convert.ToInt32(Session["UserID"]);
             var data = PictureExamineeService.GetListByUserID(userid);
             return PartialView(data);
        }


                    #region upload control

                    //uoload control
                    public ActionResult UploadPicture(PictureExam model, HttpPostedFileBase file)
                    {
           
                        model.Image = PathUpload;
                        model.ExamineeID = 1;
                        var id = this.PictureExamineeService.Save(model);
                        PathUpload = "";
                        return Json(id, JsonRequestBehavior.AllowGet);
                    }
                    

                    #endregion


        #endregion

    }
}