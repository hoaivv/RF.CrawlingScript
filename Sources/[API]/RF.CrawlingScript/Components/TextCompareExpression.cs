using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("l=t+t", typeof(TextCompareExpression))]

namespace RF.CrawlingScript.Components
{
    public class TextCompareExpression : LogicExpression
    {
        private TextExpression Expression1 { get; set; }
        private TextExpression Expression2 { get; set; }
        private TextCompareOperations Operation { get; set; }

        public TextCompareExpression() { }

        public TextCompareExpression(TextExpression exp1, TextExpression exp2, TextCompareOperations operation)
        {
            if ((object)exp1 == null || (object)exp2 == null) throw new ArgumentNullException();

            Expression1 = exp1;
            Expression2 = exp2;
            Operation = operation;
        }

        public override void Evaluate(Context context, out object result)
        {
            string left, right;

            Expression1.Evaluate(context, out left);
            Expression2.Evaluate(context, out right);            

            switch (Operation)
            {
                case TextCompareOperations.Equal: result = left == right; break;
                case TextCompareOperations.NotEqual: result = left != right; break;
                case TextCompareOperations.Greater: result = string.Compare(left, right) > 0; break;
                case TextCompareOperations.Lesser: result = string.Compare(left, right) < 0; break;

                default: throw new InvalidOperationException();
            }
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression1);
            Script.Serialize(output, Expression2);
            output.Write((byte)Operation);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression1 = Script.Deserialize(input) as TextExpression;
            Expression2 = Script.Deserialize(input) as TextExpression;
            Operation = (TextCompareOperations)input.ReadByte();
        }
    }
}
