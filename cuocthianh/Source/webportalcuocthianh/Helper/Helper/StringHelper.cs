﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace Helper
{
   public class StringHelper
    {


        public static double VNDTOUSD(double dong, double usd_rate)
        {

            return dong / usd_rate;

        }

        public static double USDTOVND(double usd, double usd_rate)
        {

            return usd * usd_rate;

        }

        public static string FormatVND(int VND)
        {
            return String.Format("{0:0,0 ₫}", VND);
        }


        public static string FormatCurrencyVN(double currency)
        { 
            return currency.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN"));
        }
       /// <summary>
       /// Convert Number To VietNam Currency
       /// </summary>
       /// <param name="_number"></param>
       /// <returns></returns>
       public static string ConvertNumberToVietNamCurrency(string _number)
       {
           try
           {
               string tmpString = String.Empty;
               for (int i = 0; i < _number.Length; i++)
               {
                   if (char.IsDigit(_number, i))
                   {
                       tmpString += _number[i];
                   }
               }

               for (int i = tmpString.Length-1; i > 0; i--)
               {
                   if (i % 3 == 0)
                   {
                       tmpString = tmpString.Insert(tmpString.Length - i, ".");
                   }
               }
               return tmpString + "₫";
               //return String.Format("{0:0,0 VNĐ}", long.Parse(_number));
           }
           catch
           {
               return "";
           }
       }
       /// <summary>
       /// Convert VietNam Currency To Number
       /// </summary>
       /// <param name="_vncurrency"></param>
       /// <returns></returns>
       public static int ConvertVietNamCurrencyToNumber(string _vncurrency)
       {
           try
           {
                return int.Parse(Regex.Replace(_vncurrency, @"\D", ""));
           }
           catch
           {
               return 0;
           }
       }

       /// <summary>
       /// Encrypt
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
       public static string EncryptData(string data)
       {
           return Security.EncryptString(data);
       }
       /// <summary>
       /// Decrypt
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
       public static string DecryptData(string data)
       {
           return Security.DecryptString(data);
       }



       private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };

       public static string RemoveVietNamString(string str)
       {

           for (int i = 1; i < VietnameseSigns.Length; i++)
           {

               for (int j = 0; j < VietnameseSigns[i].Length; j++)

                   str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

           }
           return str;
       }


       public static string GeneratorLink(string value)
       {
           try
           {
               var item = RemoveVietNamString(value.Replace(" ", "-").Replace("/", "-").Replace("&", "-"));
               for (int i = 0; i < item.Length; i++)
               {
                   try
                   {
                       if (item[i] == '-' && item[i + 1] == '-')
                       {
                           item = item.Remove(i, 1);
                           i--;
                       }
                   }
                   catch
                   {

                   }
               }
               return item;
           }
           catch
           {
               return null;
           }
       }

       public static string GetHost()
       {
           return "http://" + HttpContext.Current.Request.Url.Host +":"+ HttpContext.Current.Request.Url.Port;
       }

       /// <summary>
       /// Tạo số báo danh
       /// </summary>
       public static string GenerateCode(int num)
       {
           if (num < 10)
               return "0" + num;
           return num.ToString();
       }

    }
}
