using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("f.ContainKey", typeof(ContainKey))]

namespace RF.CrawlingScript.Functions
{
    public class ContainKey : LogicExpression
    {
        private TextExpression Key { get; set; }
        private DictionaryVariable Dictionary { get; set; }

        public ContainKey() { }

        public ContainKey(TextExpression key, DictionaryVariable dict)
        {
            if ((object)key == null || dict == null) throw new ArgumentNullException();

            Key = key;
            Dictionary = dict;
        }

        public override void Evaluate(Context context, out object result)
        {
            string value;
            Dictionary<string, string> dict;

            Key.Evaluate(context, out value);
            Dictionary.Evaluate(context, out dict);

            if (dict == null || value == null) throw new ArgumentNullException();

            result = dict.ContainsKey(value);
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Key);
            Script.Serialize(output, Dictionary);
        }

        public override void Deserialize(BinaryReader input)
        {
            Key = Script.Deserialize(input) as TextExpression;
            Dictionary = Script.Deserialize(input) as DictionaryVariable;
        }
    }
}
