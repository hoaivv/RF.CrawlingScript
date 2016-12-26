using RF.CrawlingScript.Components;
using System;
using System.IO;

[assembly: SerializerContract("l", typeof(LogicValue))]

namespace RF.CrawlingScript.Components
{
    public partial class LogicValue : LogicExpression
    {
        private bool Value { get; set; }

        public LogicValue() { }

        public LogicValue(bool value)
        {
            Value = value;
        }
    }

    partial class LogicValue // override
    {
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Value);
        }

        public override void Deserialize(BinaryReader input)
        {
            Value = input.ReadBoolean();
        }

        public override void Evaluate(Context context, out object result)
        {
            result = Value;
        }
    }
}
