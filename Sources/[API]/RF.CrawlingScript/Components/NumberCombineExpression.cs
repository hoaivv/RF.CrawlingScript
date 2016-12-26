using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("n=n+n", typeof(NumberCombineExpression))]

namespace RF.CrawlingScript.Components
{
    public partial class NumberCombineExpression : NumberExpression
    {
        private NumberExpression Expression1 { get; set; }
        private NumberExpression Expression2 { get; set; }
        private NumberOperations Operation { get; set; }
    }

    partial class NumberCombineExpression // contructor
    {
        public NumberCombineExpression() { }

        public NumberCombineExpression(NumberExpression exp1, NumberExpression exp2, NumberOperations operation)
        {
            if ((object)exp1 == null || (object)exp2 == null) throw new ArgumentNullException();

            Expression1 = exp1;
            Expression2 = exp2;
            Operation = operation;
        }
    }

    partial class NumberCombineExpression // override
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
            Operation = (NumberOperations)input.ReadByte();
        }

        public override void Evaluate(Context context, out object result)
        {
            decimal left, right;

            Expression1.Evaluate(context, out left);
            Expression2.Evaluate(context, out right);

            switch (Operation)
            {
                case NumberOperations.Add: result = left + right; break;
                case NumberOperations.Subtract: result = left - right; break;
                case NumberOperations.Multiply: result = left * right; break;
                case NumberOperations.Divide: result = left / right; break;
                case NumberOperations.Module: result = left % right; break;

                default: throw new InvalidOperationException();
            }
        }
    }
}
