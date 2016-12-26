using RF.CrawlingScript.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: SerializerContract("t2", typeof(TextPairValue))]

namespace RF.CrawlingScript.Components
{
    public class TextPairValue : TextPairExpression
    {
        private KeyValuePair<string, string> Data { get; set; }

        public TextPairValue() { }

        public TextPairValue(KeyValuePair<string, string> value)
        {
            if ((object)value == null) throw new ArgumentNullException();

            Data = value;
        }

        public override void Evaluate(Context context, out object result)
        {
            result = Data;
        }

        public override void Serialize(BinaryWriter output)
        {
            output.Write(Data.Key);
            output.Write(Data.Value);
        }

        public override void Deserialize(BinaryReader input)
        {
            string key = input.ReadString();
            string value = input.ReadString();

            Data = new KeyValuePair<string, string>(key, value);
        }
    }
}
