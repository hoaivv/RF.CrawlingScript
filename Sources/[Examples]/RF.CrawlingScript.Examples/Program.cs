using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Utilities.Http;
using Shark;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tesseract;

namespace RF.CrawlingScript.Examples
{

    class Program
    {
        public static string DeCaptcha(Image source)
        {
            string str = "0123456789";

            try
            {

                TesseractEngine tesseract = new TesseractEngine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata"), "eng");
                tesseract.SetVariable("tessedit_char_whitelist", str);

                Page page = tesseract.Process((Bitmap)source);

                string result = Regex.Replace(page.GetText(), "([^0-9])", "");

                tesseract.Dispose();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }



        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Transaction trans = new Transaction();
                trans.Request("http://123nhadatviet.com/CaptchaGenerator.aspx", false);

                File.WriteAllBytes("D:\\captcha.png", trans.ResponseData);

                Image img = Image.FromFile("D:\\captcha.png");
                Image thumb = img.GetThumbnailImage(img.Width * 4, img.Height * 5, null, IntPtr.Zero);

                thumb.Save("D:\\resized.png", System.Drawing.Imaging.ImageFormat.Png);

                img.Dispose();
                thumb.Dispose();

                img = Image.FromFile("D:\\resized.png");

                string result = DeCaptcha(img);

                img.Dispose();

                File.WriteAllBytes("D:\\" + result + ".png", trans.ResponseData);
            }
        }
    }

}
