using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using Shark;
using Shark.Utilities;
using System;
using System.IO;

[assembly: SerializerContract("f.Log", typeof(Log))]

namespace RF.CrawlingScript.Functions
{
    public class Log : Code
    {
        private TextExpression Exp { get; set; }

        public Log() { }
        public Log(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();
            Exp = exp;
        }

        public override void Deserialize(BinaryReader input)
        {
            Exp = Script.Deserialize(input) as TextExpression;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Exp);
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            string exp; Exp.Evaluate(context, out exp);
            if (Framework.LogEnabled) Log<Script>.Information(exp);

            isBreaking = false;
        }
    }
}
