using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("v.d", typeof(DictionaryVariable))]

namespace RF.CrawlingScript.Components
{
    public partial class DictionaryVariable : DictionaryExpression, IVariable<Dictionary<string, string>>
    {
        public int Name { get; private set; }
    }

    partial class DictionaryVariable // contructors
    {       
        public DictionaryVariable() { }

        public DictionaryVariable(int name)
        {
            Name = name;
        }
    }

    partial class DictionaryVariable // IVariable
    {
        public void Set(Context context, object value)
        {
            if (value == null || !(value is Dictionary<string, string>)) throw new InvalidOperationException("could not convert value of type " + (value?.GetType().Name ?? "null") + " to type " + typeof(Dictionary<string, string>).Name);
            context.SetVariable(Name, value);
        }
    }

    partial class DictionaryVariable // override
    { 
        public override void Evaluate(Context context, out object result)
        {
            if (!context.HasVariable(Name)) context.SetVariable(Name, new Dictionary<string, string>());
            result = context.GetVariable(Name);
        }

        public override void Serialize(BinaryWriter output)
        {
            output.Write(Name);
        }

        public override void Deserialize(BinaryReader input)
        {
            Name = input.ReadInt32();
        }

        public override void DoCount(Context context, out int result)
        {
            Dictionary<string, string> self; Evaluate(context, out self);
            result = self.Count;
        }
    }

    partial class DictionaryVariable // common
    {
                       
        public ContainKey ContainsKey(TextExpression key)
        {
            return new ContainKey(key, this);
        }

        public ContainValue ContainsValue(TextExpression value)
        {
            return new ContainValue(value, this);
        }

        public FirstKeyOf FirstKeyOf(TextExpression value)
        {
            return new FirstKeyOf(value, this);
        }

        public Clear Clear()
        {
            return new Clear(this);
        }
    }
}
