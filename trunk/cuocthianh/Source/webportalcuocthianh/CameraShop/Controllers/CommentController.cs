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
        public static List<Comment> ListComment;
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
            GetComment();
            //List<Comment> ListComment = GetComment().Where(x => x.ArticleID == articleid).ToList();
            List<Comment> ListComment = (List<Comment>)Session["SSComment"];
            return PartialView(ListComment);
        }

        public List<Comment> GetComment()
        {
            if (Session["SSComment"] == null)
            {
                ListComment = new List<Comment>();
                //Comment c = new Comment(ID, ArticleID, UserID, Type, Content, DateTime.Now, ParentID);
                Comment c = new Comment(1, 1, 1, 1, "Quisque dapibus rhoncus tortor quis", DateTime.Now, 0);
                ListComment.Add(c);
                c = new Comment(2, 1, 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", DateTime.Now, 1);
                ListComment.Add(c);
                c = new Comment(3, 1, 2, 1, "Suspendisse potenti. Vivamus ultricies", DateTime.Now, 2);
                ListComment.Add(c);
                c = new Comment(4, 1, 3, 1, "quis consectetur nunc turpis in nisl. Duis sit amet semper felis.", DateTime.Now, 2);
                ListComment.Add(c);
                c = new Comment(5, 1, 2, 1, "Nunc congue nisl nec nibh tincidunt blandit. Nam iaculis nisi nec cursus facilisis.", DateTime.Now, 0);
                ListComment.Add(c);
                c = new Comment(6, 1, 2, 1, "Phasellus neque risus, cursus sed orci vel, vehicula euismod justo. ", DateTime.Now, 0);
                ListComment.Add(c);
                c = new Comment(7, 1, 1, 1, "Integer tellus mi, congue et eleifend vel, blandit vitae felis. Nam quis condimentum nisi, et malesuada dui", DateTime.Now, 6);
                ListComment.Add(c);
                c = new Comment(8, 1, 2, 1, " Donec lobortis convallis viverra. Aenean rhoncus non est non accumsan. ", DateTime.Now, 6);
                ListComment.Add(c);
                c = new Comment(9, 1, 2, 1, " Donec lobortis convallis viverra. Aenean rhoncus non est non accumsan. ", DateTime.Now, 8);
                ListComment.Add(c);
                c = new Comment(10, 1, 2, 1, " Donec lobortis convallis viverra. Aenean rhoncus non est non accumsan. ", DateTime.Now, 9);
                ListComment.Add(c);
                c = new Comment(11, 1, 2, 1, " Donec lobortis convallis viverra. Aenean rhoncus non est non accumsan. ", DateTime.Now, 9);
                ListComment.Add(c);
                Session["SSComment"] = ListComment;
            }
            return ListComment;
        }

        public ActionResult _AddComment(int parentid = 0, string content = "")
        {
            var idlast = ListComment.OrderBy(x => x.ID).LastOrDefault().ID;
            idlast += 1;
            Comment c = new Comment(idlast, 1, 1, 1, content, DateTime.Now, parentid);
            ListComment.Add(c);
            Session["SSComment"] = ListComment;
            return PartialView(c);
        }
    }
}
