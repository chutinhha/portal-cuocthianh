using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionServices;
using CoreData;

namespace CameraShop.Areas.Administrator.Controllers
{
    public class ManagementController : BaseController
    {
        //
        // GET: /Administrator/Admin/

        public ActionResult Index()
        {
            return SetUpSecurity();
        }

        public ManagementController(IUserActionService user, IUser_GroupActionService usergroup,
            IUser_Role_ModuleActionService rolemodule, IModuleActionService module)
            : base(user, usergroup, rolemodule, module)
        { 
            
        }
        //more

    }
}
