using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
using Helper;

namespace CameraShop.Areas.Administrator.Controllers
{
    public class GeneralConfigManagementController : BaseController
    {
        //
        // GET: /Administrator/Category/

        public GeneralConfigManagementController(IConfigurationActionService _config, IEmailTemplateActionService _email)
            : base(_config,_email)
        {
            try
            {
                Permission = ListPermission.FirstOrDefault(c => c.Module.Equals(Helper.ValueDefine.CONFIG)).Role;
            }
            catch { }
        }

        #region Main

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ActionResult _ShowAll()
        {
            var data = ConfigurationService.GetList();
            return PartialView(data);
        }

        #endregion




        #region Genneral

        public ActionResult _General()
        {
            var data = ConfigurationService.GetGenneralConfig();
            return PartialView("_General",data);
        }

        public ActionResult General(string data)
        {
            data += "##Logo+" + PathUpload;
            var id = this.ConfigurationService.UpdateConfig(data);
            PathUpload="";
            if (id != -1)
            {
                return Content(Helper.ErrorCode.Success);
            }
            return Content(Helper.ErrorCode.Error);
        }


        #endregion

        #region _Email

        public ActionResult _Email()
        {
            var data = ConfigurationService.GetEmailConfig();
            return PartialView("_Email", data);
        }

        public ActionResult Email(string data)
        {
            var id = this.ConfigurationService.UpdateConfig(data);
            if (id != -1)
            {
                return Content(Helper.ErrorCode.Success);
            }
            return Content(Helper.ErrorCode.Error);
        }
      
        #endregion


        #region _Email

        public ActionResult _EmailTemplate()
        {
            var data = EmailTemplateService.GetList();
            return PartialView("_EmailTemplate", data);
        }
        [ValidateInput(false)]
        public ActionResult EmailTemplate(EmailTemplate model)
        {
           
            var id = this.EmailTemplateService.Save(model);
            if (id > 0)
            {
                var data = this.EmailTemplateService.GetByID(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("");
            }
        }

        public ActionResult DeleteEmailTemplate(int id)
        {
            
                var data = this.EmailTemplateService.GetByID(id);
               if(this.EmailTemplateService.Delete(data))
                return Content("ok");
               return Content("");
        }

        public ActionResult GetTemplateByID(int id)
        {
            var data = this.EmailTemplateService.GetByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion



    }
}
