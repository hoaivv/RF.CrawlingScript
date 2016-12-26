using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.Read", typeof(Read))]

namespace RF.CrawlingScript.Functions
{
    public class Read : NumberExpression, ICode
    {
        public Read() { }

        public override void Evaluate(Context context, out object result)
        {
            result = (decimal)Console.Read();
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
