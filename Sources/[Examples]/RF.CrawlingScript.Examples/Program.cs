using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using Shark;
using System;
using System.IO;

namespace RF.CrawlingScript.Examples
{
    class Example : Script
    {
        public const string test = "<option value = \"1\" > Nh&#224; mat tien</option>";

        private block post(TextExpression t1, TextExpression t2)
        {
            return new block
            {
                
            };
        }

        public override block Implementation()
        {
            var pair = x.pair;
            var data = x.data;
            var r = x.request;
            var i = x.number;

            return new block
            {
                new request("http://localhost/index.php",false) >> r,

                r.Post("a","b"),
                r.Post("a","b.jpg", new byte[] {1,2,3}),

                x[0] >> i, (i < Context.GetNumber("image-count")).Stay(true) > new exec
                {
                    r.Post("a" + i.Text, "b"+i.Text+".jpg", Context.GetData("image-" + i.Text + "-data")),

                    i + 1 >> i,
                },

                r.Submit(data),                

                f.WriteFile("D:\\data.txt",data)
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

                context.Set("image-2-data", new byte[1]);
                context.Set("image-1-data", new byte[2]);
                context.Set("image-0-data", new byte[3]);

                context.Set("image-count", 3);

                Script.Run(context, "D:\\test.rfc");
                
                Framework.Stop();
            }
        }
    }
}
