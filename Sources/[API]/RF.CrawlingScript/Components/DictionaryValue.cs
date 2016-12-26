using RF.CrawlingScript.Components;
using System.Collections.Generic;
using System.IO;
using System;

[assembly:SerializerContract("d", typeof(DictionaryValue))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a dictionary in RFCScript
    /// </summary>
    public class DictionaryValue : DictionaryExpression
    {
        private Dictionary<string, string> Value { get; set; }

        /// <summary>
        /// Construct an empty <see cref="DictionaryValue"/>. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public DictionaryValue() { }

        /// <summary>
        /// Construct a <see cref="DictionaryValue"/>
        /// </summary>
        /// <param name="value">Value to be represented</param>
        public DictionaryValue(Dictionary<string, string> value)
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
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Value.Count);

            foreach (KeyValuePair<string, string> i in Value)
            {
                output.Write(i.Key);
                output.Write(i.Value);
            }
        }

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            int count = input.ReadInt32();

            Value = new Dictionary<string, string>();

            for(int i = 0; i < count; i++)
            {
                string key = input.ReadString();
                string value = input.ReadString();

                Value[key] = value;
            }
        }

        /// <summary>
        /// Gets the number of data records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public override void DoCount(Context context, out int result)
        {
            result = Value.Count;
        }
    }
}
