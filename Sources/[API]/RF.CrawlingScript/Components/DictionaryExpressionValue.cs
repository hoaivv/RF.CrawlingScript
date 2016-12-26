using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly:SerializerContract("d[]", typeof(DictionaryExpressionValue))]

namespace RF.CrawlingScript.Components
{
    public class DictionaryExpressionValue : TextExpression, IVariable<string>
    {
        private DictionaryExpression Expression { get; set; }
        private TextExpression Key { get; set; }

        public DictionaryExpressionValue() { }

        public DictionaryExpressionValue(DictionaryExpression exp, TextExpression key)
        {
            if ((object)exp == null || (object)key == null) throw new ArgumentNullException();

            Expression = exp;
            Key = key;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
            Script.Serialize(output, Key);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as DictionaryExpression;
            Key = Script.Deserialize(input) as TextExpression;
        }

        public override void Evaluate(Context context, out object result)
        {
            Dictionary<string, string> dict; Expression.Evaluate(context, out dict);
            string key; Key.Evaluate(context, out key);

            if (!dict.ContainsKey(key)) throw new IndexOutOfRangeException();

            result = dict[key];
        }

        public void Set(Context context, object value)
        {
            if (value == null || !(value is string)) throw new InvalidOperationException();

            Dictionary<string, string> dict; Expression.Evaluate(context, out dict);
            string key; Key.Evaluate(context, out key);

            dict[key] = value as string;
        }

    }
}
