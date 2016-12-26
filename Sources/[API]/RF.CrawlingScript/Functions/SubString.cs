using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.SubString", typeof(SubString))]

namespace RF.CrawlingScript.Functions
{
    public class SubString : TextExpression
    {
        private TextExpression Input { get; set; }
        private NumberExpression Start { get; set; } = 0;
        private NumberExpression Count { get; set; } = -1;

        public SubString() { }

        public SubString(TextExpression input, NumberExpression start)
        {
            if ((object)input == null || (object)start == null) throw new ArgumentNullException();

            Input = input;
            Start = start;
            Count = null;
        }

        public SubString(TextExpression input, NumberExpression start, NumberExpression length)
        {
            if ((object)input == null || (object)start == null || (object)length == null) throw new ArgumentNullException();

            Input = input;
            Start = start;
            Count = length;
        }

        public override void Evaluate(Context context, out object result)
        {
            string str; Input.Evaluate(context, out str);

            if ((object)Count == null)
            {
                decimal start; Start.Evaluate(context, out start);
                result = str.Substring((int)start);
            }
            else
            {
                decimal start, length;

                Start.Evaluate(context, out start);
                Count.Evaluate(context, out length);

                result = str.Substring((int)start,(int)length);
            }
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Input);
            Script.Serialize(output, Start);
            Script.Serialize(output, Count);
        }

        public override void Deserialize(BinaryReader input)
        {
            Input = Script.Deserialize(input) as TextExpression;
            Start = Script.Deserialize(input) as NumberExpression;
            Count = Script.Deserialize(input) as NumberExpression;
        }
    }
}
