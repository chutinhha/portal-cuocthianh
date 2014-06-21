using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;

namespace CameraShop.Controllers
{
    //trừ trường hợp bất khả kháng không thể xử lý ở mức service mới được phép xử lý trên controller
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/
        public ArticleController(IArticleActionService _article, IUserActionService _user, IConfigurationActionService _config)
            : base(_article, _user,_config)
        { }

        /// <summary>
        /// View All Article
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int catid = 0, string username = "", int page = 1, int PageSize = 8)
        {
            try
            {
                var article = ArticleService.GetListByRelationsID(catid, username);
                foreach (var item in article)
                {
                    item.pageExt = page;
                    item.PageSizeExt = PageSize;
                    item.TotalPageExt = (int)Math.Ceiling((float)article.Count / (float)item.PageSizeExt);
                }
                article = article.Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                return View(article);
            }
            catch {
                return View();
            }
        }

        /// <summary>
        /// View Detail Product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Detail(long id, string name="")
        {
            var data = ArticleService.GetByID(id);
            if (data == null)
                data = new Article();
            var user = UserService.GetByID(data.UserID);
           if(user!=null)
               data.UserNameExt = user.UserName;
            data.ListSameArticleExt = ArticleService.GetSameListByCateID(data.ID, 5);
            return View(data);
        }
        /// <summary>
        /// PartialView NewArticle on Home Page
        /// </summary>
        /// <returns></returns>
        public ActionResult _NewArticles()
        {
            var article = ArticleService.GetListByLINQ(x=> x.ShowHomePage && x.Active).OrderByDescending(x=>x.UpdateDate).Take(5);
            return PartialView("_NewArticles", article);
        }


        public ActionResult _GetArticleDefault()
        {
            var article = ArticleService.GetList();
            return PartialView(article);
        }

    }
}
