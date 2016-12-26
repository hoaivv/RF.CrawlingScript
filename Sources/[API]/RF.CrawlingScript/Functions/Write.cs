using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.Write", typeof(Write))]

namespace RF.CrawlingScript.Functions
{
    public class Write : Code
    {
        private IExpression Expression { get; set; }

        public Write() { }

        public Write(IExpression expression)
        {
            Expression = expression;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as IExpression;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            object value = null; Expression?.Evaluate(context, out value);

            Console.Write(value);
            isBreaking = false;
        }
    }
}
