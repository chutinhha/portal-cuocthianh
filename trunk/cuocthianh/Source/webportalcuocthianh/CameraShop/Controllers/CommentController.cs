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
     //   public static List<Comment> ListComment;
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
        /// Return json object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult CommentBoxJson(int id, int type)
        {
            if (type == 1)
            {
                // return Json(data, JsonRequestBehavior.AllowGet);
                return PartialView(); //comment picture
            }
            else
            {
                // return Json(data, JsonRequestBehavior.AllowGet);
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

        public ActionResult _ListComment(int articleid = 0)
        {
           // GetComment(articleid);
            //List<Comment> ListComment = GetComment().Where(x => x.ArticleID == articleid).ToList();
            List<Comment> ListComment = GetComment(articleid);
            return PartialView(ListComment);
        }

        public List<Comment> GetComment(int examid)
        {
            List<Comment> ListComment = new List<Comment>();
            //if (Session["SSComment"] == null)
            //{
            //   Session["SSComment"] = CommentService.GetListByLINQ(c => c.CommentType == 1 && c.ReferenceID == examid);
            //   ListComment = (List<Comment>)Session["SSComment"];
              
            //}
            ListComment = CommentService.GetListByLINQ(x => x.CommentType == 1 && x.ReferenceID == examid).ToList();
            return ListComment;
        }

        public ActionResult _AddComment(int parentid = 0, int articleid = 0, string content = "", string name = "")
        {

            Comment c = new Comment();
            c.ReferenceID = articleid;
            c.ParentID = parentid;
            if (Session["Username"] != null)
            {
                c.Name = Session["Username"].ToString();
            }
            else
            {
                c.Name = name;
            }
            c.CommentContent = content;
            c.PostDate = DateTime.Now;
            // c.UserID = 
            //var ListComment = (List<Comment>)Session["SSComment"];
            //ListComment.Add(c);
            CommentService.Save(c);
            //Session["SSComment"] = ListComment;
            return PartialView(c);
        }
    }
}
