using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
namespace CameraShop.Areas.Administrator.Controllers
{
    public class ValidateController :BaseController
    {
        //
        // GET: /Administrator/Validate/

        public ValidateController(ICategoryActionService _category, IManufacturerActionService _manufacturer)
            : base(_category, _manufacturer) { }


        public ActionResult CheckExistCatagoryName(Category _model)
        {
            string error;
            CategoryService.ValidateExist(_model, CategoryService.GetList().ToList(), "Name", out error);
            return Content(error);
        }

    }
}
