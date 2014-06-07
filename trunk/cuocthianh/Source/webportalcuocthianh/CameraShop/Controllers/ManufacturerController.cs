using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;

namespace CameraShop.Controllers
{
    public class ManufacturerController : BaseController
    {
        //
        // GET: /Manufacturer/
        public ManufacturerController(
            IManufacturerActionService _manufacturer )
            : base(_manufacturer)
        { }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ManufacturerMenu() {
            var data = ManufacturerService.GetListByLINQ(x=>x.Active).Take(7);
            return PartialView(data);
        }

        /// <summary>
        /// Phần link banner liên kết các cửa hàng gần footer
        /// </summary>
        /// <returns></returns>
        public ActionResult _VendorConnect() {
            return PartialView();
        }

    }
}
