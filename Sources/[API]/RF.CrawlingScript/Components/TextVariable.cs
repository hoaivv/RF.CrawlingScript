using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("v.t", typeof(TextVariable))]

namespace RF.CrawlingScript.Components
{
    public partial class TextVariable : TextExpression, IVariable<string>
    {
        public int Name { get; private set; }
    }

    partial class TextVariable // constructor
    {
        public TextVariable() { }

        public TextVariable(int name)
        {
            Name = name;
        }
    }

    partial class TextVariable // IVariable
    {
        public void Set(Context context, object value)
        {
            context.SetVariable(Name, value?.ToString() ?? string.Empty);
        }
    }     

    partial class TextVariable // override
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
            if (!context.HasVariable(Name)) context.SetVariable(Name, string.Empty);
            result = context.GetVariable(Name);
        }
    }
}
