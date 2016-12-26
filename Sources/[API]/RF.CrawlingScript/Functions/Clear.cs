using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("f.Clear", typeof(Clear))]

namespace RF.CrawlingScript.Functions
{
    public class Clear : Code
    {
        private DictionaryVariable Dictionary { get; set; }

        public Clear() { }

        public Clear(DictionaryVariable dict)
        {
            if (dict == null) throw new ArgumentNullException();
            Dictionary = dict;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            Dictionary<string, string> dict; Dictionary.Evaluate(context, out dict);

            dict.Clear();
            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Dictionary);
        }

        public override void Deserialize(BinaryReader input)
        {
            Dictionary = Script.Deserialize(input) as DictionaryVariable;
        }
    }
}
