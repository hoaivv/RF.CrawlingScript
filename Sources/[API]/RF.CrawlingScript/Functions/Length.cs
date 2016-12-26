using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.Length", typeof(Length))]

namespace RF.CrawlingScript.Functions
{
    public class Length : NumberExpression
    {
        private TextExpression Expression { get; set; }

        public Length() { }

        public Length(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Evaluate(Context context, out object result)
        {
            string value; Expression.Evaluate(context, out value);

            if (value == null) throw new ArgumentNullException();

            result = (decimal)value.Length;
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
