using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Helper
{
   public static class ImageHelper
    {
       public static string ScropImage(string filename, string path, int width , int height)
       {
           Image img = Image.FromFile(filename);
           Image thumb = img.GetThumbnailImage(width, height, null, IntPtr.Zero);
           thumb.Save(path);
           img.Dispose();
           thumb.Dispose();
           
           return System.IO.Path.GetFileName(path);
       }
       public static string UpImage(string filename, string path)
       {
           Image img = Image.FromFile(filename);
           img.Save(path);
           img.Dispose();
           //thumb.Dispose();
           GC.Collect();
           return System.IO.Path.GetFileName(path);
       }

       /// <summary>
       /// WaterMark is text
       /// </summary>
       /// <param name="path"></param>
       /// <param name="text"></param>
       /// <returns></returns>
       public static string CreateWaterMark(string path, string text)
       {
           var _path = Path.GetDirectoryName(path)+"\\"+Path.GetFileNameWithoutExtension(path)+Guid.NewGuid().ToString().Substring(0,4)+Path.GetExtension(path);

           var file = ImageHelper.WaterMarkToImage(path, text);

           file.Save(_path);
           file.Dispose();
           GC.Collect();
           try
           {
               File.Delete(path);
           }
           catch { }
           return System.IO.Path.GetFileName(_path);
          
       }

       public static void DeleteImage(string path)
       {
           try
           {
               File.Delete(path);
           }
           catch { }
       }

       static Bitmap WaterMarkToImage(string ImagePath, string watermark)
       {
           Bitmap bmp;
           bmp = new Bitmap(ImagePath);

           Graphics graphicsObject;
           int x, y;
           try
           {
               //create graphics object from bitmap
               graphicsObject = Graphics.FromImage(bmp);
           }
           catch (Exception e)
           {
               Bitmap bmpNew = new Bitmap(bmp.Width, bmp.Height);
               graphicsObject = Graphics.FromImage(bmpNew);

               graphicsObject.DrawImage(bmp, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
               bmp = bmpNew;
           }

           int startsize = (bmp.Width / watermark.Length);//get the font size with respect to length of the string
           //x and y cordinates to draw a string
           x = 0;
           y = bmp.Height / 2;
           //System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical); -> draws a vertical string for watermark
           System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.NoWrap);
           //drawing string on Image
           graphicsObject.DrawString(watermark, new Font("Verdana", startsize, FontStyle.Bold), new SolidBrush(Color.FromArgb(60, 255, 255, 255)), x, y, drawFormat);
           //return a water marked image
           return (bmp);
       }



       /// <summary>
       /// watermark is image
       /// </summary>
       /// <param name="ImagePath"></param>
       /// <param name="watermark"></param>
       /// <returns></returns>
      public static string CreateImageWaterMark(string ImagePath, string watermark)
       {
           var _path = Path.GetDirectoryName(ImagePath)+"\\"+Path.GetFileNameWithoutExtension(ImagePath)+Guid.NewGuid().ToString().Substring(0,4)+Path.GetExtension(ImagePath);
          Bitmap bmp = new Bitmap(ImagePath);
          Graphics g = Graphics.FromImage(bmp);
          //g.SmoothingMode = SmoothingMode.HighQuality;
          Bitmap bmpWM = (Bitmap)Bitmap.FromFile(watermark);
          int x = 2;
          int y = (bmp.Height / 2) -(bmpWM.Height/2);
          g.DrawImage(bmpWM, new Point(x, y));  
           bmp.Save(_path);
           bmp.Dispose();
           bmpWM.Dispose();
           g.Flush();
           try
           {
               GC.Collect();
               try
               {
                   File.Delete(ImagePath);
               }
               catch { }
              
           }
           catch { }
           return System.IO.Path.GetFileName(_path);
       }



      private static ImageCodecInfo GetCodecInfo(string mimeType)
      {
          foreach (ImageCodecInfo encoder in ImageCodecInfo.GetImageEncoders())
              if (encoder.MimeType == mimeType)
                  return encoder;
          throw new ArgumentOutOfRangeException(
              string.Format("'{0}' not supported", mimeType));
      }

      public static string SaveCompressed(string filename, string path,
      long quality)
      {
          Image img = Image.FromFile(filename);
          EncoderParameters parameters = new EncoderParameters(1);
          parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
          img.Save(path, GetCodecInfo("image/jpeg"), parameters);
          img.Dispose();
          DeleteImage(filename);
          return System.IO.Path.GetFileName(path);
      }



    }
}
