using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CoreData
{
   public class Search
    {
       public string Name { get; set; }
       public string Image { get; set; }
       public string Link { get; set; }
       public string Description { get; set; }
       public string Type { get; set; }
       public long CatID { get; set; }

       public List<SelectListItem> ListCategoryExt { get; set; }
       public int PageExt { get; set; }
       public int PageSizeExt { get; set; }
       public int TotalPageExt { get; set; }
       public int CountItemExt { get; set; }
    }
}
