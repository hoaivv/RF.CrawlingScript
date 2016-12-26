using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.IsNumber", typeof(IsNumber))]

namespace RF.CrawlingScript.Functions
{
    public class IsNumber : LogicExpression
    {
        private TextExpression Expression { get; set; }

        public IsNumber() { }

        public IsNumber(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Evaluate(Context context, out object result)
        {
            string text; Expression.Evaluate(context, out text);

            if (string.IsNullOrEmpty(text))
            {
                result = false;
            }
            else
            {
                decimal dummy;
                result = decimal.TryParse(text, out dummy);
            }
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
