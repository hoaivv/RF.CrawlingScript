using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ReadFile", typeof(ReadFile))]

namespace RF.CrawlingScript.Functions
{
    public class ReadFile : DataExpression
    {
        private TextExpression File { get; set; }

        public ReadFile() { }

        public ReadFile(TextExpression file)
        {
            if ((object)file == null) throw new ArgumentNullException();

            File = file;
        }

        public override void Evaluate(Context context, out object result)
        {
            string file; File.Evaluate(context, out file);

            result = System.IO.File.ReadAllBytes(file);
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, File);
        }

        public override void Deserialize(BinaryReader input)
        {
            File = Script.Deserialize(input) as TextExpression;
        }

        public override void DoCount(Context context, out int result)
        {
            byte[] data; Evaluate(context, out data);
            result = data.Length;
        }
    }
}
