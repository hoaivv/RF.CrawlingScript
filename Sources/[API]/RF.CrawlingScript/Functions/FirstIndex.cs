using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.FirstIndex", typeof(FirstIndex))]

namespace RF.CrawlingScript.Functions
{
    public class FirstIndex : NumberExpression
    {
        private TextExpression Value { get; set; }
        private TextExpression Input { get; set; }

        public FirstIndex() { }

        public FirstIndex(TextExpression value, TextExpression input)
        {
            if ((object)input == null || (object)value == null) throw new ArgumentNullException();

            Input = input;
            Value = value;
        }

        public override void Evaluate(Context context, out object result)
        {
            if ((object)Value == null || (object)Input == null) throw new InvalidOperationException();

            string input, value;

            Value.Evaluate(context, out value);
            Input.Evaluate(context, out input);

            if (input == null || value == null) throw new ArgumentNullException();

            result = (decimal)input.IndexOf(value);
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Value);
            Script.Serialize(output, Input);
        }

        public override void Deserialize(BinaryReader input)
        {
            Value = Script.Deserialize(input) as TextExpression;
            Input = Script.Deserialize(input) as TextExpression;
        }
    }
}
