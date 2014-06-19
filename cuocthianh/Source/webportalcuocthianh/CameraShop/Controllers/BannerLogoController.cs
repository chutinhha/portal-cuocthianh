using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;
using Helper;
using System.Text.RegularExpressions;
namespace CameraShop.Controllers
{
    public class BannerLogoController : BaseController
    {
        //
        // GET: /BannerLogo/
        public BannerLogoController(IBanner_LogoActionService _banner)
            : base(_banner)
        { }
        /// <summary>
        /// Banner On Main Page
        /// </summary>
        /// <returns></returns>
        public ActionResult _MainSlideShow()
        {
            IList<Banner_Logo> banner = BannerService.GetList();// (x => x.Active == true && x.Type == ValueDefine.BannerLogoType.Banner && x.Position == ValueDefine.BannerLogoPosition.ShideShow).Take(5).ToList();
            return PartialView(banner);
        }

        /// <summary>
        /// SlideShow ben hong SlideShow chinh
        /// </summary>
        /// <returns></returns>
        public ActionResult _SubSlideShow()
        {
            IList<Banner_Logo> banner = BannerService.GetListByLINQ(x => x.Active == true && x.Type == ValueDefine.BannerLogoType.Banner && x.Position == ValueDefine.BannerLogoPosition.RightSlider).Take(3).ToList();
            return PartialView(banner);
        }
        /// <summary>
        /// Show Banner on Column by Position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ActionResult _ColumnBanner(ValueDefine.BannerLogoPosition position=ValueDefine.BannerLogoPosition.LeftBanner, long CatId=0) {
            IList<Banner_Logo> banner = BannerService.GetListByLINQ(x => x.Active == true && x.Type == ValueDefine.BannerLogoType.Banner && x.Position == position && x.ProductCateID==CatId);
            return PartialView(banner);
        }
    }
}
