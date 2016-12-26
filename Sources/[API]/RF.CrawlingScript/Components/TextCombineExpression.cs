using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("t=t+t", typeof(TextCombineExpression))]

namespace RF.CrawlingScript.Components
{
    public class TextCombineExpression : TextExpression
    {
        private TextExpression Expression1 { get; set; }
        private TextExpression Expression2 { get; set; }

        public TextCombineExpression() { }

        public TextCombineExpression(TextExpression exp1, TextExpression exp2)
        {
            if ((object)exp1 == null || (object)exp2 == null) throw new ArgumentNullException();

            Expression1 = exp1;
            Expression2 = exp2;
        }

        public override void Evaluate(Context context, out object result)
        {
            string left, right;

            Expression1.Evaluate(context, out left);
            Expression2.Evaluate(context, out right);            

            result = left + right;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression1);
            Script.Serialize(output, Expression2);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression1 = Script.Deserialize(input) as TextExpression;
            Expression2 = Script.Deserialize(input) as TextExpression;
        }
    }
}
