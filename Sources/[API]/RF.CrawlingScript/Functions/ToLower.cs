using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ToLower", typeof(ToLower))]

namespace RF.CrawlingScript.Functions
{
    public class ToLower : TextExpression
    {
        private TextExpression Expression { get; set; }

        public ToLower() { }

        public ToLower(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Evaluate(Context context, out object result)
        {
            string text; Expression.Evaluate(context, out text);

            result = text.ToLower();
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as TextExpression;
        }
    }
}
