using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ReadLine", typeof(ReadLine))]

namespace RF.CrawlingScript.Functions
{
    public class ReadLine : TextExpression, ICode
    {
        public ReadLine() { }

        public override void Evaluate(Context context, out object result)
        {
            result = Console.ReadLine();
        }

        public void Execute(Context context, out bool isBreaking)
        {
            object dummy; Evaluate(context, out dummy);
            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
        }

        public override void Deserialize(BinaryReader input)
        {
        }
    }
}
