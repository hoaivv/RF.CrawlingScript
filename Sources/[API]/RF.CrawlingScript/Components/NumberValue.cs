using RF.CrawlingScript.Components;
using System;
using System.IO;

[assembly: SerializerContract("n", typeof(NumberValue))]

namespace RF.CrawlingScript.Components
{
    public partial class NumberValue : NumberExpression
    {
        private decimal Value { get; set; }

        public NumberValue() { }

        public NumberValue(decimal value)
        {
            Value = value;
        }
    }

    partial class NumberValue // override
    {
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Value);
        }

        public override void Deserialize(BinaryReader input)
        {
            Value = input.ReadDecimal();
        }

        public override void Evaluate(Context context, out object result)
        {
            result = Value;
        }
    }
}
