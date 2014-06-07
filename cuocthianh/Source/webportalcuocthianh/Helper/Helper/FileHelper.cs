using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace Helper
{
    public class FileHelper
    {
        public static string FileUpload(HttpPostedFileBase file, string dir)
        {
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(file.FileName)+"_"+Guid.NewGuid().ToString().Substring(0,4) + Path.GetExtension(file.FileName);
                string filepath = HttpContext.Current.Server.MapPath("~/" + dir + "/" + filename);
                file.SaveAs(filepath);
               
                return filename;
            }
            return "";
        }
    }
}
