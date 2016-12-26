using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ToUpper", typeof(ToUpper))]

namespace RF.CrawlingScript.Functions
{
    public class ToUpper : TextExpression
    {
        private TextExpression Expression { get; set; }

        public ToUpper() { }

        public ToUpper(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Evaluate(Context context, out object result)
        {
            string text; Expression.Evaluate(context, out text);

            result = text.ToUpper();
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
