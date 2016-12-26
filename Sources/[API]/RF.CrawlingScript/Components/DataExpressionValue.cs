using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("2[]", typeof(DataExpressionValue))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a data at a speficied index of a <see cref="DataExpression"/>
    /// </summary>
    public class DataExpressionValue : NumberExpression, IVariable<decimal>
    {
        private DataExpression Expression { get; set; }
        private NumberExpression Key { get; set; }

        /// <summary>
        /// Construct an empty <see cref="DataExpressionValue"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public DataExpressionValue() { }

        /// <summary>
        /// Construct a <see cref="DataExpressionValue"/> expression
        /// </summary>
        /// <param name="exp">Data expression, value of which would be extracted</param>
        /// <param name="key">Index at with data would be extracted</param>
        public DataExpressionValue(DataExpression exp, NumberExpression key)
        {
            if ((object)exp == null || (object)key == null) throw new ArgumentNullException();

            Expression = exp;
            Key = key;
        }

        /// <summary>
        /// Serialize <see cref="DataExpressionValue"/> data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
            Script.Serialize(output, Key);
        }

        /// <summary>
        /// Deserialize <see cref="DataExpressionValue"/> data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as DataExpression;
            Key = Script.Deserialize(input) as NumberExpression;
        }

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            byte[] data; Expression.Evaluate(context, out data);
            decimal key; Key.Evaluate(context, out key);

            if (key < 0 || key > data.Length) throw new IndexOutOfRangeException();

            result = (decimal)data[(int)key];
        }

        /// <summary>
        /// Sets value for the variable. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="value">Value to be set to the variable</param>
        public void Set(Context context, object value)
        {
            if (value == null || !(value is decimal)) throw new InvalidOperationException();

            byte[] data; Expression.Evaluate(context, out data);
            decimal key; Key.Evaluate(context, out key);

            data[(int)key] = (byte)(decimal)value;
        }

    }
}
