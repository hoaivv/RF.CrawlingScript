using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("v.d", typeof(DictionaryVariable))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes a variable which represent a dictionary in RFCSCript
    /// </summary>
    public partial class DictionaryVariable : DictionaryExpression, IVariable<Dictionary<string, string>>
    {
        private int Name { get; set; }
    }

    partial class DictionaryVariable // contructors
    {
        /// <summary>
        /// Construct an empty <see cref="DictionaryVariable"/> expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public DictionaryVariable() { }

        /// <summary>
        /// Construct a <see cref="DictionaryVariable"/>
        /// </summary>
        /// <param name="name">Variable's identifier</param>
        public DictionaryVariable(int name)
        {
            Name = name;
        }
    }

    partial class DictionaryVariable // IVariable
    {
        /// <summary>
        /// Sets value for the variable. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="value">Value to be set to the variable</param>
        public void Set(Context context, object value)
        {
            if (value == null || !(value is Dictionary<string, string>)) throw new InvalidOperationException("could not convert value of type " + (value?.GetType().Name ?? "null") + " to type " + typeof(Dictionary<string, string>).Name);
            context.SetVariable(Name, value);
        }
    }

    partial class DictionaryVariable // override
    {
        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public override void Evaluate(Context context, out object result)
        {
            if (!context.HasVariable(Name)) context.SetVariable(Name, new Dictionary<string, string>());
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

        /// <summary>
        /// Gets the number of data records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public override void DoCount(Context context, out int result)
        {
            Dictionary<string, string> self; Evaluate(context, out self);
            result = self.Count;
        }
    }

    partial class DictionaryVariable // common
    {
        /// <summary>
        /// Indicates whether the dictionary contains provided key or not
        /// </summary>
        /// <param name="key">key to be tested</param>
        /// <returns>instance of <see cref="Functions.ContainKey"/></returns>
        public ContainKey ContainsKey(TextExpression key)
        {
            return new ContainKey(key, this);
        }

        /// <summary>
        /// Indicates whether the dictionary contains provided value or not
        /// </summary>
        /// <param name="value">Value to be tested</param>
        /// <returns>instance of <see cref="Functions.ContainValue"/></returns>
        public ContainValue ContainsValue(TextExpression value)
        {
            return new ContainValue(value, this);
        }

        /// <summary>
        /// Get the first key of provided value within the dictionary
        /// </summary>
        /// <param name="value">value, key of which to be returned</param>
        /// <returns>instance of <see cref="Functions.FirstKeyOf"/></returns>
        public FirstKeyOf FirstKeyOf(TextExpression value)
        {
            return new FirstKeyOf(value, this);
        }

        /// <summary>
        /// Clear the dictionary
        /// </summary>
        /// <returns>instance of <see cref="Functions.Clear"/></returns>
        public Clear Clear()
        {
            return new Clear(this);
        }
    }
}
