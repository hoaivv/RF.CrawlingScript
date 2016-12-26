using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly:SerializerContract("d[]", typeof(DictionaryExpressionValue))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes an expression which represent value of an entry of a dictionary
    /// </summary>
    public class DictionaryExpressionValue : TextExpression, IVariable<string>
    {
        private DictionaryExpression Expression { get; set; }
        private TextExpression Key { get; set; }

        /// <summary>
        /// Construct an empty <see cref="DataExpressionValue"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public DictionaryExpressionValue() { }

        /// <summary>
        /// Construct a <see cref="DataExpressionValue"/> expression
        /// </summary>
        /// <param name="exp">Dictionary, which contains the entry</param>
        /// <param name="key">Key associated with the entry</param>
        public DictionaryExpressionValue(DictionaryExpression exp, TextExpression key)
        {
            if ((object)exp == null || (object)key == null) throw new ArgumentNullException();

            Expression = exp;
            Key = key;
        }

        /// <summary>
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
            Script.Serialize(output, Key);
        }

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as DictionaryExpression;
            Key = Script.Deserialize(input) as TextExpression;
        }

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            Dictionary<string, string> dict; Expression.Evaluate(context, out dict);
            string key; Key.Evaluate(context, out key);

            if (!dict.ContainsKey(key)) throw new IndexOutOfRangeException();

            result = dict[key];
        }

        /// <summary>
        /// Sets value for the variable. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="value">Value to be set to the variable</param>
        public void Set(Context context, object value)
        {
            if (value == null || !(value is string)) throw new InvalidOperationException();

            Dictionary<string, string> dict; Expression.Evaluate(context, out dict);
            string key; Key.Evaluate(context, out key);

            dict[key] = value as string;
        }

    }
}
