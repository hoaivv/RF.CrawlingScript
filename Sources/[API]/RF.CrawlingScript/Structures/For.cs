using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;
using System.IO;

[assembly: SerializerContract("for", typeof(For))]

namespace RF.CrawlingScript.Structures
{
    public class For : Code
    {
        private Setter Initialization { get; set; }
        private LogicExpression Condition { get; set; }
        private Setter Acceleration { get; set; }
        private ICode Code { get; set; }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Initialization);
            Script.Serialize(output, Condition);
            Script.Serialize(output, Acceleration);
            Script.Serialize(output, Code);
        }

        public override void Deserialize(BinaryReader input)
        {
            Initialization = Script.Deserialize(input) as Setter;
            Condition = Script.Deserialize(input) as LogicExpression;
            Acceleration = Script.Deserialize(input) as Setter;
            Code = Script.Deserialize(input) as ICode;
        }

        public For() { }

        public For(Setter initialization, LogicExpression condition, Setter acceleration)
        {
            if ((object)initialization == null || (object)condition == null || (object)acceleration == null) throw new ArgumentNullException();

            Initialization = initialization;
            Condition = condition;
            Acceleration = acceleration;
        }

        public static For operator >(For self, exec code)
        {
            self.Code = code;
            return self;
        }

        public static For operator <(For self, exec code)
        {
            throw new InvalidOperationException();
        }

        public override void Execute(Context context, out bool isBreaking)
        {

            Initialization.Execute(context, out isBreaking);

            while(true)
            {
                bool test; Condition.Evaluate(context, out test);

                if (!test) break;

                Code?.Execute(context, out isBreaking);

                if (isBreaking) break;

                Acceleration.Execute(context, out isBreaking);
            }

            isBreaking = false;
        }
    }
}
