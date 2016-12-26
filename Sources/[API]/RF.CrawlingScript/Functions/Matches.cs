using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

[assembly: SerializerContract("f.Matches", typeof(Matches))]

namespace RF.CrawlingScript.Functions
{
    public class Matches : ISet
    {
        protected TextExpression Input { get; private set; }
        protected TextExpression Pattern { get; private set; }

        public Matches() { }

        public Matches(TextExpression input, TextExpression pattern)
        {
            if ((object)input == null || (object)pattern == null) throw new ArgumentNullException();

            Input = input;
            Pattern = pattern;
        }

        public void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Input);
            Script.Serialize(output, Pattern);
        }

        public void Deserialize(BinaryReader input)
        {
            Input = Script.Deserialize(input) as TextExpression;
            Pattern = Script.Deserialize(input) as TextExpression;
        }

        public void GetEnumerator(Context context, out IEnumerator enumerator)
        {
            string input, pattern;

            Input.Evaluate(context, out input);
            Pattern.Evaluate(context, out pattern);

            MatchCollection mc = Regex.Matches(input, pattern);

            enumerator = mc.GetEnumerator();
        }

        public object Convert(object obj)
        {
            System.Text.RegularExpressions.Match m = (System.Text.RegularExpressions.Match)obj;

            Dictionary<string, string> result = new Dictionary<string, string>();

            if (m.Success)
            {
                for (int i = 0; i < m.Groups.Count; i++) result[i.ToString()] = m.Groups[i].Value;
            }

            return result;
        }

        public void DoCount(Context context, out int result)
        {
            string input, pattern;

            Input.Evaluate(context, out input);
            Pattern.Evaluate(context, out pattern);

            MatchCollection mc = Regex.Matches(input, pattern);

            result = mc.Count;
        }
    }
}
