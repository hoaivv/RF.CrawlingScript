using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.Replace", typeof(Replace))]

namespace RF.CrawlingScript.Functions
{
    public class Replace : TextExpression
    {
        private TextExpression Expression { get; set; }
        private TextExpression Value { get; set; }
        private TextExpression Replacement { get; set; }

        public Replace() { }

        public Replace(TextExpression exp, TextExpression value, TextExpression replacement)
        {
            if ((object)exp == null || (object)value == null || (object)replacement == null) throw new ArgumentNullException();

            Expression = exp;
            Value = value;
            Replacement = replacement;
        }

        public override void Evaluate(Context context, out object result)
        {
            string text, value, replacement;

            Expression.Evaluate(context, out text);
            Value.Evaluate(context, out value);
            Replacement.Evaluate(context, out replacement);

            result = text.Replace(value, replacement);
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
            Script.Serialize(output, Value);
            Script.Serialize(output, Replacement);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as TextExpression;
            Value = Script.Deserialize(input) as TextExpression;
            Replacement = Script.Deserialize(input) as TextExpression;
        }
    }
}
