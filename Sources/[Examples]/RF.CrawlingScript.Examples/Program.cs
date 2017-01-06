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
            Console.WriteLine(WebUtility("<option value=\"1\">C?n b&#225;n</option>"));
            Console.ReadLine();
        }
    }

}
