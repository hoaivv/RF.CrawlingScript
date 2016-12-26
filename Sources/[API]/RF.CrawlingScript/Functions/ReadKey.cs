using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ReadKey", typeof(ReadKey))]

namespace RF.CrawlingScript.Functions
{
    public class ReadKey : TextExpression
    {
        private LogicExpression Intercept { get; set; }

        public ReadKey() { }

        public ReadKey(LogicExpression intercept)
        {
            if ((object)intercept == null) throw new ArgumentNullException();

            Intercept = intercept;
        }

        public override void Evaluate(Context context, out object result)
        {
            bool intercept; Intercept.Evaluate(context, out intercept);

            result = Console.ReadKey(intercept);
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Intercept);
        }

        public override void Deserialize(BinaryReader input)
        {
            Intercept = Script.Deserialize(input) as LogicExpression;
        }
    }
}
