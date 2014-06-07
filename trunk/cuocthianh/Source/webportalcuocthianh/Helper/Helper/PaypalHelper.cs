using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class PaypalHelper
    {
        public static string PayPalStandard(string OrderID, string Amout, string returnurl)
        {
            string frm = String.Format(@"
                                           
                                       ", OrderID, Amout, returnurl);
            return frm;
        }



    }
}
