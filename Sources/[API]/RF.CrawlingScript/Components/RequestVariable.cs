using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly:SerializerContract("v.r", typeof(RequestVariable))]

namespace RF.CrawlingScript.Components
{
    public class RequestVariable : RequestExpression, IVariable<RequestData>
    {
        private int Name { get; set; }

        public RequestVariable() { }

        internal RequestVariable(int name)
        {
            Name = name;
        }

        public override void Evaluate(Context context, out object result)
        {
            if (!context.HasVariable(Name)) context.SetVariable(Name, new RequestData());
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
        /// Sets value for the variable. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="value">Value to be set to the variable</param>
        public void Set(Context context, object value)
        {
            if (value == null || !(value is RequestData)) throw new InvalidOperationException("could not convert value of type " + (value?.GetType().Name ?? "null") + " to type " + typeof(RequestData).Name);
            context.SetVariable(Name, value);
        }
    }
}
