using RF.CrawlingScript.Components;
using System;
using System.IO;

[assembly: SerializerContract("ctx.n", typeof(ContextNumberValue))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a number value passed on to executing script by its context
    /// </summary>
    public class ContextNumberValue : NumberExpression
    {
        private TextExpression Name { get; set; }

        /// <summary>
        /// Construct an empty <see cref="ContextNumberValue"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public ContextNumberValue() { }

        /// <summary>
        /// Construct a <see cref="ContextNumberValue"/> expression
        /// </summary>
        /// <param name="name">Name of the data passed on to executing script by its context</param>
        public ContextNumberValue(TextExpression name)
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

            if (!context.HasObject(name)) context.SetObject(name, default(decimal));

            result = context.GetObect(name);

            if (!(result is decimal)) throw new InvalidOperationException();
        }

        /// <summary>
        /// Serialize <see cref="ContextNumberValue"/> data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Name);
        }

        /// <summary>
        /// Deserialize <see cref="ContextNumberValue"/> data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Name = Script.Deserialize(input) as TextExpression;
        }
    }
}
