using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpi.Net.SecureMail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Helper
{
  public  class EmailHelper
    {
      //Send multi mail
      public static bool Send(string[] emailto, string emailfrom, string passwordfrom, string displayname, string title, string body, string[] Attachfile=null)
      {
          try
          {

              // send email  file attached
              SecureMailMessage message = new SecureMailMessage();

              message.From = new SecureMailAddress(emailfrom, displayname);
              //support for sending email to multi users
              foreach (var mail in emailto)
              {
                  try
                  {
                      message.To.Add(new SecureMailAddress(mail, mail));
                  }
                  catch { }
              }
              message.Subject = title;
              message.Body = body;
              message.IsBodyHtml = true;
              //message.IsSigned = true;
              // message.IsEncrypted = false;
              if (Attachfile != null && Attachfile.Length > 0)
              {
                  foreach (var f in Attachfile)
                  {
                      try
                      {
                          SecureAttachment item = new SecureAttachment(f);
                          message.Attachments.Add(item);
                      }
                      catch { }
                  }
              }
              // Instantiate a good old-fashioned SmtpClient to send your message
              System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
              client.EnableSsl = true;

              // If your SMTP server requires you to authenticate, you need to specify your
              // username and password here.
              client.Credentials = new NetworkCredential(emailfrom, passwordfrom);
              ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

              client.Send(message);
              return true;
          }
          catch (Exception ex)
          {
              Console.WriteLine(ex.Message);
              return false;
          }
      }

      //send one mail
      public static bool Send(string emailto, string emailfrom, string passwordfrom, string displayname, string title, string body, string[] Attachfile = null)
      {
          try
          {

              // send email  file attached
              SecureMailMessage message = new SecureMailMessage();

              message.From = new SecureMailAddress(emailfrom, displayname);
              //support for sending email to multi users

              message.To.Add(new SecureMailAddress(emailto, emailto));

              message.Subject = title;
              message.Body = body;
              message.IsBodyHtml = true;
              //message.IsSigned = true;
              // message.IsEncrypted = false;
              if (Attachfile != null && Attachfile.Length > 0)
              {
                  foreach (var f in Attachfile)
                  {
                      try
                      {
                          SecureAttachment item = new SecureAttachment(f);
                          message.Attachments.Add(item);
                      }
                      catch { }
                  }
              }
              // Instantiate a good old-fashioned SmtpClient to send your message
              System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
              client.EnableSsl = true;

              // If your SMTP server requires you to authenticate, you need to specify your
              // username and password here.
              client.Credentials = new NetworkCredential(emailfrom, passwordfrom);
              ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

              client.Send(message);
              return true;
          }
          catch (Exception ex)
          {
              Console.WriteLine(ex.Message);
              return false;
          }
      }



      #region Order send to client


      public static string TableHeader()
      {
          string table = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"5\" border=\"1\" style=\"border-collapse:collapse;border-style:solid;border-color:#cccccc;font-family:Arial,Helvetica,sans-serif;font-size:10pt\">";
          table += "    <tbody><tr>";
          table += "   <td width=\"200\"><div align=\"center\" style=\"font-family:Arial,Helvetica,sans-serif;font-size:10pt;font-weight:bold\">Tên sản phẩm</div></td>";
          table += "    <td width=\"150\"><div align=\"center\" style=\"font-family:Arial,Helvetica,sans-serif;font-size:10pt;font-weight:bold\">Số lượng</div></td>";
          table += "    <td width=\"150\"><div align=\"right\" style=\"font-family:Arial,Helvetica,sans-serif;font-size:10pt;font-weight:bold\">Số tiền (VNĐ)</div></td>";
          table += "    <td width=\"100\"><div align=\"center\" style=\"font-family:Arial,Helvetica,sans-serif;font-size:10pt;font-weight:bold\">VAT</div></td>";
          table += "   <td width=\"150\"><div align=\"right\" style=\"font-family:Arial,Helvetica,sans-serif;font-size:10pt;font-weight:bold\">Thanh toán (VNĐ)</div></td>";
          table += "  </tr>";
          return table;
      }
      public static string TableFooter(int totalprice)
      {
          string total = String.Format("{0:0,0}", totalprice) + " VNĐ";
          string footer = "  <tr height=\"25px\"><td align=\"right\" style=\"padding-right:5px\" colspan=\"5\"><div align=\"right\"><strong>Tổng cộng:</strong> " + total + "</div></td></tr></tbody></table>	";
          return footer;
      }

      public static string TableOrderDetail(string productname, int amount, string vat, int price)
      {
          string _price = String.Format("{0:0,0}", (price)) + " VNĐ";
          string total = String.Format("{0:0,0}", (amount * price)) + " VNĐ";
          string detail = "<tr>";
          detail += " <td>" + productname + "</td>";
          detail += " <td><div align=\"center\">" + amount + "</div></td>";
          detail += "  <td><div align=\"right\">" + _price + "</div></td>";
          detail += " <td><div align=\"center\">" + vat + "</div></td>";
          detail += "<td><div align=\"right\">" + total + "</div></td>";
          detail += " </tr>";
          return detail;

      }
      #endregion

    }
}
