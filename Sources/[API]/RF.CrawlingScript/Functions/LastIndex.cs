using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.LastIndex", typeof(LastIndex))]

namespace RF.CrawlingScript.Functions
{
    public class LastIndex : NumberExpression
    {
        private TextExpression Value { get; set; }
        private TextExpression Input { get; set; }

        public LastIndex() { }

        public LastIndex(TextExpression value, TextExpression input)
        {
            if ((object)value == null || (object)input == null) throw new ArgumentNullException();

            Input = input;
            Value = value;                 
        }

        public override void Evaluate(Context context, out object result)
        {
            string input, value;

            Value.Evaluate(context, out value);
            Input.Evaluate(context, out input);

            if (input == null || value == null) throw new ArgumentNullException();

            result = (decimal)input.LastIndexOf(value);
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
