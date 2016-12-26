using RF.CrawlingScript.Components;
using System.Collections.Generic;
using System.IO;
using System;

[assembly:SerializerContract("d", typeof(DictionaryValue))]

namespace RF.CrawlingScript.Components
{
    public class DictionaryValue : DictionaryExpression
    {
        private Dictionary<string, string> Value { get; set; }

        public DictionaryValue() { }

        public DictionaryValue(Dictionary<string, string> value)
        {
            Value = value;
        }

        public override void Evaluate(Context context, out object result)
        {
            result = Value;
        }

        public override void Serialize(BinaryWriter output)
        {
            output.Write(Value.Count);

            foreach (KeyValuePair<string, string> i in Value)
            {
                output.Write(i.Key);
                output.Write(i.Value);
            }
        }

        public override void Deserialize(BinaryReader input)
        {
            int count = input.ReadInt32();

            Value = new Dictionary<string, string>();

            for(int i = 0; i < count; i++)
            {
                string key = input.ReadString();
                string value = input.ReadString();

                Value[key] = value;
            }
        }

        public override void DoCount(Context context, out int result)
        {
            result = Value.Count;
        }
    }
}
