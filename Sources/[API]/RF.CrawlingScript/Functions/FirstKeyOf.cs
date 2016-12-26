using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("f.FirstKeyOf", typeof(FirstKeyOf))]

namespace RF.CrawlingScript.Functions
{
    public class FirstKeyOf : TextExpression
    {
        private TextExpression Value { get; set; }
        private DictionaryVariable Dictionary { get; set; }

        public FirstKeyOf() { }

        public FirstKeyOf(TextExpression value, DictionaryVariable dict)
        {
            if ((object)value == null || dict == null) throw new ArgumentNullException();

            Dictionary = dict;
            Value = value;
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

        public override void Evaluate(Context context, out object result)
        {
            if ((object)Value == null || (object)Dictionary == null) throw new ArgumentNullException();

            string value;
            Dictionary<string, string> dict;

            Value.Evaluate(context, out value);
            Dictionary.Evaluate(context, out dict);

            if (dict == null || value == null) throw new ArgumentNullException();

            result = string.Empty;

            foreach (KeyValuePair<string, string> i in dict)
            {
                if (i.Value == value)
                {
                    result = i.Key;
                    break;
                }
            }
        }
    }
}
