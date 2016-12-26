using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("l=l+l", typeof(LogicCombineExpression))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes an expression which is a result of applying a logical combine operation
    /// </summary>
    public partial class LogicCombineExpression : LogicExpression
    {
        private LogicExpression Expression1 { get; set; }
        private LogicExpression Expression2 { get; set; }
        private LogicOperations Operation { get; set; }

        /// <summary>
        /// Construct an empty <see cref="LogicCombineExpression"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public LogicCombineExpression() { }

        /// <summary>
        /// Construct a <see cref="LogicCombineExpression"/>
        /// </summary>
        /// <param name="exp1">First operand of the operation</param>
        /// <param name="exp2">Second operand of the operation</param>
        /// <param name="operation">Operation to be applied</param>
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
        /// <summary>
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression1);
            Script.Serialize(output, Expression2);
            output.Write((byte)Operation);
        }

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Expression1 = Script.Deserialize(input) as LogicExpression;
            Expression2 = Script.Deserialize(input) as LogicExpression;
            Operation = (LogicOperations)input.ReadByte();
        }

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
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
