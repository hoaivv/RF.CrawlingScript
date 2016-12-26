using RF.CrawlingScript.Components;
using System.IO;
using System;

[assembly: SerializerContract("2", typeof(DataValue))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a <see cref="byte"/> array data in RFCScript
    /// </summary>
    public class DataValue : DataExpression
    {
        private byte[] Value { get; set; }

        /// <summary>
        /// Construct an empty <see cref="DataValue"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public DataValue() { }

        /// <summary>
        /// Constrct a <see cref="DataValue"/> expression
        /// </summary>
        /// <param name="value">Data to be represented by the expression</param>
        public DataValue(byte[] value)
        {
            Value = value;
        }

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            result = Value;
        }

        /// <summary>
        /// Gets the number of bytes contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public override void DoCount(Context context, out int result)
        {
            result = Value.Length;
        }

        /// <summary>
        /// Serialize <see cref="DataValue"/> data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Value.Length);
            output.Write(Value);
        }

        /// <summary>
        /// Deserialize <see cref="DataValue"/> data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            int count = input.ReadInt32();
            Value = input.ReadBytes(count);
        }
    }
}
