using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;

namespace CameraShop.Controllers
{
    public class ExamineeController : BaseController
    {
        public ExamineeController(IExamineeActionService _examinee)
            : base(_examinee)
        { }
        //
        // GET: /Examinee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ListExaminee() {
            var data = ExamineeService.GetList();
            return PartialView(data);
        }

        public ActionResult Detail(int id, string username="") {
            var data = ExamineeService.GetByID((id));
            data.View += 1;
            ExamineeService.Save(data);
            return View(data);
        }

    }
}
