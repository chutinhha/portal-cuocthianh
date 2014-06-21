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
            if (data == null)
                data = new List<Examinee>();
            return PartialView(data);
        }

        public ActionResult Detail(string id="0", string username="") {
            var data = ExamineeService.GetByID(int.Parse(id));
            data.ViewCount += 1;
            ExamineeService.Save(data);
            return View(data);
        }
        [ValidateInput(false)]
        public ActionResult UpdateDescription(string content)
        {
            var examinee = ExamineeService.GetOneByLINQ(c=>c.UserID.Equals(Helper.SessionManagement.GetSessionReturnInt("UserID")));
            if(examinee==null)
                return Content("Fail");
            var data = ExamineeService.GetByID(examinee.ID);
            data.Description = content;

            var c1 = ExamineeService.Save(data);
            if (c1 > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("Fail");
            }
        }
    }
}
