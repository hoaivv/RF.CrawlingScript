using RF.CrawlingScript.Definitions;
using Shark;
using System;

namespace RF.CrawlingScript.Examples
{
    class Example : Script
    {
        public const string test = "<option value = \"1\" > Nh&#224; mat tien</option>";

        public override block Implementation()
        {
            var pair = x.pair;

            return new block
            {
                f.WriteLine(f.HtmlDecode(test).ToASCII()),
                f.ReadLine()
            };
        }

        class Program
        {
            static void Main(string[] args)
            {
                Framework.Start();

                Example e = new Example();

                //e.Run();

                if (!e.Build("D:\\test.rfc")) Console.WriteLine("failed");

                Context context = new Context();

                context.Set("info", new dict()
                {
                    {"a", "1" },
                    {"b", "2" },
                    {"c", "3" },
                    {"d", "5" },
                });

                Script.Run(context, "D:\\test.rfc");
                
                Framework.Stop();
            }
        }
    }
}
