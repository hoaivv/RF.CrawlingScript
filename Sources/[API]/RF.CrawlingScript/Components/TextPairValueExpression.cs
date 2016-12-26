using RF.CrawlingScript.Components;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("t=t2.v", typeof(TextPairValueExpression))]

namespace RF.CrawlingScript.Components
{
    public class TextPairValueExpression : TextExpression
    {
        private TextPairExpression Expression { get; set; }

        public TextPairValueExpression() { }

        public TextPairValueExpression(TextPairExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as TextPairExpression;
        }

        public override void Evaluate(Context context, out object result)
        {
            KeyValuePair<string, string> pair; Expression.Evaluate(context, out pair);

            result = pair.Value;
        }
    }
}
