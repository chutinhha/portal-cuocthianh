using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ExternalDatabaseHelper
{
    public static class ExtendModel
    {
        private static CameraDBEntities _ExtendModel = new CameraDBEntities();

        public static CameraDBEntities ExtendDBModel
        {
            get { return _ExtendModel; }
        }
        //public static VanaShopDBEntities ExtendModel()
        //{
        //    return _ExtendModel;
        //}

       public static void CheckDiscountProduct(Thread _thread)
        {
            while (_thread.IsAlive)
            {
                try
                {
                    var now = DateTime.Now;
                    GetProductExpirateddate(now);
                }
                catch
                {

                }

                Thread.Sleep(10000);
            }
        }


         static void GetProductExpirateddate(DateTime now)
        {
            try
            {
                //_ExtendModel = null;
                //_ExtendModel = new VanaShopDBEntities();
                //var product = _ExtendModel.Products.ToList();
                //foreach(var p in product)
                //{
                //    if (p.DiscountTo != null)
                //    {
                //        var time = p.DiscountTo.Value.Ticks;
                //        var timenow = now.Ticks;
                //        if (timenow > time)
                //        {
                //            p.DiscountFrom = null;
                //            p.DiscountTo = null;
                //            p.DiscountPrice = 0;
                //        }
                //        _ExtendModel.SaveChanges();
                //    }

               // }
            }
            catch { }
        }
    }
}
