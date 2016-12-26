using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("f.ContainValue", typeof(ContainValue))]

namespace RF.CrawlingScript.Functions
{
    public class ContainValue : LogicExpression
    {
        private TextExpression Value { get; set; }
        private DictionaryVariable Dictionary { get; set; }

        public ContainValue() { }

        public ContainValue(TextExpression value, DictionaryVariable dict)
        {
            if ((object)value == null || dict == null) throw new ArgumentNullException();

            Value = value;
            Dictionary = dict;
        }

        public override void Evaluate(Context context, out object result)
        {
            string value;
            Dictionary<string, string> dict;

            Value.Evaluate(context, out value);
            Dictionary.Evaluate(context, out dict);

            if (dict == null || value == null) throw new ArgumentNullException();

            result = dict.ContainsValue(value);
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Value);
            Script.Serialize(output, Dictionary);
        }

        public override void Deserialize(BinaryReader input)
        {
            Value = Script.Deserialize(input) as TextExpression;
            Dictionary = Script.Deserialize(input) as DictionaryVariable;
        }
    }
}
