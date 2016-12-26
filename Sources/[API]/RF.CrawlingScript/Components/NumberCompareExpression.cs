using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("l=n+n", typeof(NumberCompareExpression))]

namespace RF.CrawlingScript.Components
{
    public partial class NumberCompareExpression : LogicExpression
    {
        private NumberExpression Expression1 { get; set; }
        private NumberExpression Expression2 { get; set; }
        private NumberCompareOperations Operation { get; set; }
    }

    partial class NumberCompareExpression // constructor
    {
        public NumberCompareExpression() { }

        public NumberCompareExpression(NumberExpression exp1, NumberExpression exp2, NumberCompareOperations operation)
        {
            if ((object)exp1 == null || (object)exp2 == null) throw new ArgumentNullException();

            Expression1 = exp1;
            Expression2 = exp2;
            Operation = operation;
        }
    }

    partial class NumberCompareExpression // override
    {
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression1);
            Script.Serialize(output, Expression2);
            output.Write((byte)Operation);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression1 = Script.Deserialize(input) as NumberExpression;
            Expression2 = Script.Deserialize(input) as NumberExpression;
            Operation = (NumberCompareOperations)input.ReadByte();
        }

        public override void Evaluate(Context context, out object result)
        {
            decimal left, right;

            Expression1.Evaluate(context, out left);
            Expression2.Evaluate(context, out right);
            
            switch (Operation)
            {
                case NumberCompareOperations.Greater: result = left > right; break;
                case NumberCompareOperations.Lesser: result = left < right; break;
                case NumberCompareOperations.GreaterOrEqual: result = left >= right; break;
                case NumberCompareOperations.LesserOrEqual: result = left <= right; break;
                case NumberCompareOperations.Equal: result = left == right; break;
                case NumberCompareOperations.NotEqual: result = left != right; break;

                default: throw new InvalidOperationException();
            }
        }
    }
}
