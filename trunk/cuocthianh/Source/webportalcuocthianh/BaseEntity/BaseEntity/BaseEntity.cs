using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kyers.DataEntity;
using System.Data;
namespace BaseEntity
{
    public class BaseEntity
    {
       

        static KyersSoftWareMSSQLBaseEntity DBContext { get; set; }

        public static KyersSoftWareMSSQLBaseEntity DBModel()
        {
            return DBContext;
        }
        public static void StartDB(string connection)
        {
            if (DBContext == null)
            {
                DBContext = new KyersSoftWareMSSQLBaseEntity(connection);
                DBContext.Connect();
            }
        }
        public static void StopDB()
        {
            DBContext = null;
           
        }
    }
}
