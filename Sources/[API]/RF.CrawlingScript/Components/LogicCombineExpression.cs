using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("l=l+l", typeof(LogicCombineExpression))]

namespace RF.CrawlingScript.Components
{
    public partial class LogicCombineExpression : LogicExpression
    {
        private LogicExpression Expression1 { get; set; }
        private LogicExpression Expression2 { get; set; }
        private LogicOperations Operation { get; set; }

        public LogicCombineExpression() { }

        public LogicCombineExpression(LogicExpression exp1, LogicExpression exp2, LogicOperations operation)
        {
            if ((object)exp1 == null || (object)exp2 == null) throw new ArgumentNullException();

            Expression1 = exp1;
            Expression2 = exp2;
            Operation = operation;
        }
    }

    partial class LogicCombineExpression // override
    {
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression1);
            Script.Serialize(output, Expression2);
            output.Write((byte)Operation);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression1 = Script.Deserialize(input) as LogicExpression;
            Expression2 = Script.Deserialize(input) as LogicExpression;
            Operation = (LogicOperations)input.ReadByte();
        }

        public override void Evaluate(Context context, out object result)
        {
            bool left, right;

            Expression1.Evaluate(context, out left);
            Expression2.Evaluate(context, out right);

            switch (Operation)
            {
                case LogicOperations.And: result = left & right; break;
                case LogicOperations.Or: result = left | right; break;
                case LogicOperations.Xor: result = left ^ right; break;
                case LogicOperations.Equal: result = left == right; break;
                case LogicOperations.NotEqual: result = left != right; break;

                default: throw new InvalidOperationException();
            }
        }
    }
}
