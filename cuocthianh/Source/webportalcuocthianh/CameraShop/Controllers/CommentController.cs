using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;
namespace CameraShop.Controllers
{
    public class CommentController : BaseController
    {

        public CommentController(ICommentActionService Comment) : base(Comment) { }

        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// View
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult _CommentBox(int id, int type)
        {
            if(type==1)
            {
                return PartialView(); //comment picture
            }
            else
            {
                return PartialView(); //comment article
            }
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public ActionResult CommentBox(Comment Model)
        {
            //code insert here
           // return Json(data, JsonRequestBehavior.AllowGet);
            return View();
        }





    }
}
