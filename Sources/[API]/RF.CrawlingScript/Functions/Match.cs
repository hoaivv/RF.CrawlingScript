using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

[assembly: SerializerContract("f.Match", typeof(RF.CrawlingScript.Functions.Match))]

namespace RF.CrawlingScript.Functions
{
    public class Match : LogicExpression, ICode
    {
        protected TextExpression Input { get; private set; }
        protected TextExpression Pattern { get; private set; }
        protected DictionaryVariable Storage { get; private set; }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Input);
            Script.Serialize(output, Pattern);
            Script.Serialize(output, Storage);
        }

        public override void Deserialize(BinaryReader input)
        {
            Input = Script.Deserialize(input) as TextExpression;
            Pattern = Script.Deserialize(input) as TextExpression;
            Storage = Script.Deserialize(input) as DictionaryVariable;
        }

        public Match() { }

        public Match to(DictionaryVariable storage)
        {
            if (storage == null) throw new ArgumentNullException();
            Storage = storage;

            return this;
        }

        public Match(TextExpression input, TextExpression pattern)
        {
            if ((object)input == null || (object)pattern == null) throw new ArgumentNullException();

            Input = input;
            Pattern = pattern;
        }

        public override void Evaluate(Context context, out object result)
        {
            string input, pattern;

            Input.Evaluate(context, out input);
            Pattern.Evaluate(context, out pattern);

            System.Text.RegularExpressions.Match m = Regex.Match(input, pattern);

            if (m.Success)
            {
                result = true;

                if ((object)Storage != null)
                {
                    Dictionary<string, string> storage;

                    Storage.Evaluate(context, out storage);

                    storage.Clear();

                    for (int i = 0; i < m.Groups.Count; i++) storage[i.ToString()] = m.Groups[i].Value;
                }
            }
            else
            {
                result = false;
            }
        }

        public void Execute(Context context, out bool isBreaking)
        {
            object result; Evaluate(context, out result);
            isBreaking = false;
        }
    }
}
