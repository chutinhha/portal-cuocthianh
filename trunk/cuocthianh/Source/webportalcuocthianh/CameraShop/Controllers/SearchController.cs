using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;

namespace CameraShop.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/

        public SearchController(ISearchActionService search, ICategoryActionService category):base(search, category){}

        public ActionResult Index(string value = "", int catid = 0, int page = 1, int pageSize = 10)
        {
            var data = SearchService.SearchAll(value, catid).ToList();
            foreach (var item in data)
            {
                item.PageExt = page;
                item.PageSizeExt = pageSize;
                item.TotalPageExt = (int)Math.Ceiling((float)data.Count / (float)item.PageSizeExt);
                item.CountItemExt = data.Count();
                item.CatID = catid;
            }
            data = data.OrderByDescending(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.TitleName = value;
            return View(data);
        }
        /// <summary>
        /// Search Post back
        /// </summary>
        /// <param name="model">Search Model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Search model, int page = 1, int pageSize = 10)
        {
            var data = SearchService.SearchAll(model.Name, model.CatID).ToList();
            foreach (var item in data) {
                item.PageExt = page;
                item.PageSizeExt = pageSize;
                item.TotalPageExt = (int)Math.Ceiling((float)data.Count / (float)item.PageSizeExt);
                item.CountItemExt = data.Count();
                item.CatID = model.CatID;
            }
            data = data.OrderByDescending(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.TitleName = model.Name;
            return View(data);
        }

        /// <summary>
        /// Search Input Control on Header
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult _SearchInputControl() {
            var data = SearchService.GetSelectListCategory();
            Search cat = new Search();
            if (data != null)
            {
                cat.ListCategoryExt = data;
            }
            return PartialView(cat);
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonResult SearchAll(string value, int id=0)
        {  
            var model = SearchService.SearchAll(value, id).Take(40).ToList();
            foreach(var item in model){
                if (item.Type == null) {
                    item.Type = "";
                }
            }
            return Json(model ,JsonRequestBehavior.AllowGet);
        }

    }
}
