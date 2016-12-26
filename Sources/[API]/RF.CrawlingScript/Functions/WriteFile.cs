using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.WriteFile", typeof(WriteFile))]

namespace RF.CrawlingScript.Functions
{
    public class WriteFile : Code
    {
        private TextExpression FileName { get; set; }
        private DataExpression Expression { get; set; }

        public WriteFile() { }

        public WriteFile(TextExpression fileName, DataExpression expression)
        {
            if ((object)fileName == null || (object)expression == null) throw new ArgumentNullException();

            FileName = fileName;
            Expression = expression;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, FileName);
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            FileName = Script.Deserialize(input) as TextExpression;
            Expression = Script.Deserialize(input) as DataExpression;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            string name; FileName.Evaluate(context, out name);
            byte[] value; Expression.Evaluate(context, out value);

            File.WriteAllBytes(name, value);
            isBreaking = false;
        }
    }
}
