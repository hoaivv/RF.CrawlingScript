using System;
using System.IO;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Components;

[assembly: SerializerContract("v.l", typeof(LogicVariable))]

namespace RF.CrawlingScript.Components
{
    public partial class LogicVariable : LogicExpression, IVariable<bool>
    {
        public int Name { get; private set; }

        public LogicVariable() { }

        public LogicVariable(int name)
        {
            Name = name;
        }
    }

    partial class LogicVariable // override
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
            if (!context.HasVariable(Name)) context.SetVariable(Name, false);
            result = context.GetVariable(Name);
        }
    }

    partial class LogicVariable
    {
        public void Set(Context context, object value)
        {
            if (value == null || !(value is bool)) throw new InvalidOperationException("could not convert value of type " + (value?.GetType().Name ?? "null") + " to type " + typeof(bool).Name);
            context.SetVariable(Name, value);
        }
    }
}
