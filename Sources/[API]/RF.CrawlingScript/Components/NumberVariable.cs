using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("v.n", typeof(NumberVariable))]

namespace RF.CrawlingScript.Components
{
    public partial class NumberVariable : NumberExpression, IVariable<decimal>
    {
        public int Name { get; private set; }
    }

    partial class NumberVariable // constructor
    {
        public NumberVariable() { }

        public NumberVariable(int name)
        {
            Name = name;
        }
    }

    partial class NumberVariable // override
    {
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Name);
        }

        public override void Deserialize(BinaryReader input)
        {
            Name = input.ReadInt32();
        }

        public override void Evaluate(Context context, out object result)
        {
            if (!context.HasVariable(Name)) context.SetVariable(Name, default(decimal));
            result = context.GetVariable(Name);            
        }

        public void Set(Context context, object value)
        {
            if (value == null || !(value is decimal)) throw new InvalidOperationException("could not convert value of type " + (value?.GetType().Name ?? "null") + " to type " + typeof(decimal).Name);
            context.SetVariable(Name, value);
        }
    }
}
