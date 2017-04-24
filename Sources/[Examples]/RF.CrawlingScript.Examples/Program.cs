using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Utilities.Http;
using Shark;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Tesseract;

namespace RF.CrawlingScript.Examples
{

    class Program
    {
        static void Main(string[] args)
        {            Console.WriteLine(Regex.Replace("The pi is \\u03a0", @"\\u([a-zA-Z0-9]{4})", m => 
        ((char)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber)).ToString()));
            Console.ReadLine();
        }
    }

}
