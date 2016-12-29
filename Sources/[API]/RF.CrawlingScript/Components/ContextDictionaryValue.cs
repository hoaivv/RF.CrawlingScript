using RF.CrawlingScript.Components;
using System.Collections.Generic;
using System.IO;
using System;

[assembly: SerializerContract("ctx.d", typeof(ContextDictionaryValue))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a dictionary passed on to executing script by its context
    /// </summary>
    public class ContextDictionaryValue : DictionaryExpression
    {
        private TextExpression Name { get; set; }

        /// <summary>
        /// Construct an empty <see cref="ContextDictionaryValue"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public ContextDictionaryValue() { }

        /// <summary>
        /// Construct a <see cref="ContextDictionaryValue"/> expression
        /// </summary>
        /// <param name="name">Name of the data passed on to executing script by its context</param>
        public ContextDictionaryValue(TextExpression name)
        {
            if ((object)name == null) throw new ArgumentNullException();

            Name = name;
        }

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            string name; Name.Evaluate(context, out name);

            name = "custom:" + name;

            if (!context.HasObject(name)) context.SetObject(name, new Dictionary<string, string>());

            result = context.GetObject(name);

            if (!(result is Dictionary<string, string>)) throw new InvalidOperationException();
        }

        /// <summary>
        /// Serialize <see cref="ContextDictionaryValue"/> data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Name);
        }

        /// <summary>
        /// Deserialize <see cref="ContextDictionaryValue"/> data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Name = Script.Deserialize(input) as TextExpression;
        }

        /// <summary>
        /// Gets the number of data records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public override void DoCount(Context context, out int result)
        {
            Dictionary<string, string> dict; Evaluate(context, out dict);

            result = dict.Count;
        }
    }
}
