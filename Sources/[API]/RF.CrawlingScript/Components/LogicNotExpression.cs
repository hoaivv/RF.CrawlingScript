using RF.CrawlingScript.Components;
using System;
using System.IO;

[assembly: SerializerContract("l=!l", typeof(LogicNotExpression))]

namespace RF.CrawlingScript.Components
{
    public partial class LogicNotExpression : LogicExpression
    {
        private LogicExpression Expression { get; set; }

        public LogicNotExpression() { }

        public LogicNotExpression(LogicExpression exp)
        {
            Expression = exp;
        }
    }

    partial class LogicNotExpression // override
    {
        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as LogicExpression;
        }

        public override void Evaluate(Context context, out object result)
        {
            bool exp;

            Expression.Evaluate(context, out exp);

            result = !exp;
        }
    }
}
