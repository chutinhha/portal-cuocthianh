using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;

namespace CameraShop.Controllers
{
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/
        public CategoryController(ICategoryActionService _category, IConfigurationActionService _config)
            : base(_category, _config)
        { }

        /// <summary>
        /// Menu danh mục sản phẩm
        /// </summary>
        /// <returns></returns>
        public ActionResult _NavigationCategory()
        {
            var model = CategoryService.GetListByLINQ(x => x.Active == true);
            return PartialView(model);
        }
        /// <summary>
        /// Menu danh mục sản phẩm trong trang Category
        /// </summary>
        /// <param name="CatId"></param>
        /// <returns></returns>
        public ActionResult _GetChildCategory(int CatId) {
            var model = CategoryService.GetListByLINQ(x=> x.Active== true && x.ParentID==CatId);
            return PartialView(model);
        }
    }
}
