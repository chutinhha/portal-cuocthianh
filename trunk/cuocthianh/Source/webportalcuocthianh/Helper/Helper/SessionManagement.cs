using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace Helper
{
   public class SessionManagement
    {
        /// <summary>
        /// Set session for username
        /// Using: call "HttpContext.Current.Session["Username"]"
        /// </summary>
        /// <param name="username"></param>
        public static void SetSesionUsername(string username)
        {
            HttpContext.Current.Session["Username"] = username;
        }
        /// <summary>
        /// Set session for userID
        ///  Using: call "HttpContext.Current.Session["UserId"]"
        /// </summary>
        /// <param name="id"></param>
        public static void SetSesionUseId(string id)
        {
            HttpContext.Current.Session["UserId"] = id;
        }
        /// <summary>
        /// Set session for userrole
        /// Using: call "HttpContext.Current.Session["UserRole"]"
        /// </summary>
        /// <param name="role"></param>
        public static void SetSesionUseRole(string role)
        {
            HttpContext.Current.Session["UserRole"] = role;
        }
        /// <summary>
        /// Set session for password
        /// Using: call "HttpContext.Current.Session["Password"]"
        /// </summary>
        /// <param name="username"></param>
        public static void SetSesionPassword(string password)
        {
            HttpContext.Current.Session["Password"] = password;
        }
        /// <summary>
        /// Set session for Email
        /// Using: call "HttpContext.Current.Session["Email"]"
        /// </summary>
        /// <param name="username"></param>
        public static void SetSesionEmail(string Email)
        {
            HttpContext.Current.Session["Email"] = Email;
        }
        /// <summary>
        /// Set session for Value
        /// Using: call "HttpContext.Current.Session["ten sesion name"]"
        /// </summary>
        /// <param name="Sessionname"></param>
        /// <param name="value"></param>
        public static void SetSesionValue(string sesionname, string value)
        {
            HttpContext.Current.Session["" + sesionname] = value;
        }
        public static void SetSesionValueStringArray(string sesionname, string []value)
        {
            HttpContext.Current.Session["" + sesionname] = value;
        }
       /// <summary>
       /// Return to string
       /// </summary>
       /// <param name="sesionname"></param>
       /// <returns></returns>
        public static string GetSessionReturnToString(string sesionname)
        {
            if (HttpContext.Current.Session["" + sesionname] != null)
                return HttpContext.Current.Session["" + sesionname].ToString();
            return null;
        }
       /// <summary>
       /// Return to int
       /// </summary>
       /// <param name="sesionname"></param>
       /// <returns></returns>
        public static int GetSessionReturnInt(string sesionname)
        {
            if (HttpContext.Current.Session["" + sesionname] != null)
                return int.Parse(HttpContext.Current.Session["" + sesionname].ToString());
            return 0;
        }
        public static object GetSessionReturnobject(string sesionname)
        {
            if (HttpContext.Current.Session["" + sesionname] != null)
            {
                var value = (object)HttpContext.Current.Session["" + sesionname];
                return value;
            }
            return null;
        }
       /// <summary>
       /// Suppend session
       /// </summary>
       /// <param name="sessionname"></param>
       public static void SuppendSession(string sessionname)
        {
            HttpContext.Current.Session["" + sessionname] = null;
        }
    }
}
