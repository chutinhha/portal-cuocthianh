using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreData
{
 public   class Permission
    {
     public long UserID { get; set; }
     public long GroupID { get; set; }
     public string Role { get; set; }
     public string Module { get; set; }
    }
}
