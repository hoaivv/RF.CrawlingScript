using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("v.2", typeof(DataVariable))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a variable which represent a <see cref="byte[]"/> in RFCScript
    /// </summary>
    public partial class DataVariable : DataExpression, IVariable<byte[]>
    {
        /// <summary>
        /// Variable's identifier
        /// </summary>
        private int Name { get; set; }
    }

    partial class DataVariable // contructors
    {
        /// <summary>
        /// Construct an empty <see cref="DataVariable"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public DataVariable() { }

        /// <summary>
        /// Construct a <see cref="DataVariable"/> expression.
        /// </summary>
        /// <param name="name">Variable's identifier</param>
        public DataVariable(int name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the number of bytes contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public override void DoCount(Context context, out int result)
        {
            byte[] data; Evaluate(context, out data);
            result = data.Length;
        }
    }

    partial class DataVariable // IVariable
    {
        /// <summary>
        /// Sets value for the variable. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="value">Value to be set to the variable</param>
        public void Set(Context context, object value)
        {
            if (value == null || !(value is byte[])) throw new InvalidOperationException("could not convert value of type " + (value?.GetType().Name ?? "null") + " to type " + typeof(byte[]).Name);
            context.SetVariable(Name, value);
        }
    }

    partial class DataVariable // override
    {
        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            if (!context.HasVariable(Name)) context.SetVariable(Name, new byte[0]);
            result = context.GetVariable(Name);
        }

        /// <summary>
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Name);
        }

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            Name = input.ReadInt32();
        }
    }
}
