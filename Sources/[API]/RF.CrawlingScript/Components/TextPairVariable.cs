using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("v.t2", typeof(TextPairVariable))]

namespace RF.CrawlingScript.Components
{
    public partial class TextPairVariable : TextPairExpression, IVariable<KeyValuePair<string, string>>
    {
        public int Name { get; private set; }
    }

    partial class TextPairVariable // constructors
    {
        public TextPairVariable() { }

        public TextPairVariable(int name)
        {
            Name = name;
        }
    }

    partial class TextPairVariable // override
    {
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Name);
        }

        public override void Deserialize(BinaryReader input)
        {
            Name = input.ReadInt32();
        }

        public void Set(Context context, object value)
        {
            if (value == null && !(value is KeyValuePair<string, string>)) throw new InvalidOperationException();

            context.SetVariable(Name, value);
        }
        
        public override void Evaluate(Context context, out object result)
        {
            if (!context.HasVariable(Name)) context.SetVariable(Name, new KeyValuePair<string, string>());

            result = context.GetVariable(Name);
        }
    }
}
