using RF.CrawlingScript.Components;
using System;
using System.IO;

[assembly: SerializerContract("l=!l", typeof(LogicNotExpression))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes an expression which represents result of applying logical not operation to a <see cref="LogicExpression"/>
    /// </summary>
    public partial class LogicNotExpression : LogicExpression
    {
        private LogicExpression Expression { get; set; }

        /// <summary>
        /// Construct an empty <see cref="LogicNotExpression"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public LogicNotExpression() { }

        /// <summary>
        /// Construct a <see cref="LogicNotExpression"/>
        /// </summary>
        /// <param name="exp">Expresion to be inverted logically</param>
        public LogicNotExpression(LogicExpression exp)
        {
            Expression = exp;
        }
    }

    partial class LogicNotExpression // override
    {
        /// <summary>
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as LogicExpression;
        }

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            bool exp;

            Expression.Evaluate(context, out exp);

            result = !exp;
        }
    }
}
