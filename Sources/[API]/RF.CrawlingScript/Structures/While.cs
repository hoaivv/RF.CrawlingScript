using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;
using System.IO;

[assembly: SerializerContract("while", typeof(While))]

namespace RF.CrawlingScript.Structures
{
    public class While : Code
    {
        private LogicExpression Condition { get; set; }
        private ICode Code { get; set; }

        public override void Deserialize(BinaryReader input)
        {
            Condition = Script.Deserialize(input) as LogicExpression;
            Code = Script.Deserialize(input) as ICode;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Condition);
            Script.Serialize(output, Code);
        }

        public While() { }

        public While(LogicExpression condition)
        {
            if ((object)condition == null) throw new ArgumentNullException();

            Condition = condition;
        }

        public void Do(ICode code)
        {
            Code = code;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            isBreaking = false;

            while(true)
            {
                bool test; Condition.Evaluate(context, out test);
                
                if (!test) break;

                Code?.Execute(context, out isBreaking);                

                if (isBreaking) break;
            }

            isBreaking = false;
        }
    }
}
