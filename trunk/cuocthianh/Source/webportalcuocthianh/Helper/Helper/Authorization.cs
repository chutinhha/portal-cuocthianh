using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace Helper
{
    
   
    public class UserPermission
    {
        public int UserID { get; set; }
        public  int UserGroup { get; set; }
        public string ModuleName { get; set; }
        public string Permission { get; set; }
    }

    public class Authorization
    {
        /// <summary>
        /// Save remember info to cookie
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void RememberMe(string username, string password)
        {
            HttpCookie cookie_user = new HttpCookie("kyers_remember_username", username);
            HttpCookie cookie_pasword = new HttpCookie("kyers_remember_password", password);
            HttpContext.Current.Response.Cookies.Add(cookie_user);
            HttpContext.Current.Response.Cookies.Add(cookie_pasword);
           
        }

        /// <summary>
        /// set coookies
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookies(string key, string value)
        {
            try
            {
                Authorization.SuppendCookies(key);
            }
            catch { }
            HttpCookie cookie = new HttpCookie(key, value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// suppend cookies
        /// </summary>
        /// <param name="key"></param>
        public static void SuppendCookies(string key)
        {
            try
            {
                HttpCookie mycookies = new HttpCookie(key);
                mycookies.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(mycookies);
            }
            catch { }
        }

        /// <summary>
        /// Get remember cookie
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public static void GetRememberMeCookie(out string user, out string password)
        {
            try
            {
                user = HttpContext.Current.Request.Cookies["kyers_remember_username"].Value;
                password = HttpContext.Current.Request.Cookies["kyers_remember_password"].Value;
            }
            catch { user = ""; password = ""; }
        }
        /// <summary>
        /// Remove remember me
        /// </summary>
        public static void RemoveRemember()
        {
            try
            {
                HttpCookie user = new HttpCookie("kyers_remember_username");
                user.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(user);
                HttpCookie pass = new HttpCookie("kyers_remember_password");
                pass.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(pass);
            }
            catch { 
            }
        }

    
   
      


    }
}
