using RF.CrawlingScript.Components;
using System;
using System.IO;

[assembly: SerializerContract("t", typeof(TextValue))]

namespace RF.CrawlingScript.Components
{
    public partial class TextValue : TextExpression
    {
        private string Value { get; set; }
    }

    partial class TextValue // constructor
    {
        public TextValue() { }

        public TextValue(string value)
        {
            Value = value;
        }
    }

    partial class TextValue // override
    {
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Value);
        }

        public override void Deserialize(BinaryReader input)
        {
            Value = input.ReadString();
        }

        public override void Evaluate(Context context, out object result)
        {
            result = Value;
        }    
    }
}
