using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.IO;

[assembly: SerializerContract("set", typeof(Setter))]

namespace RF.CrawlingScript.Components
{
    public class Setter : Code
    {
        private IVariable Variable { get; set; }
        private IExpression Expression { get; set; }

        public Setter() { }

        public Setter(IVariable var, IExpression exp)
        {
            if (var == null || exp == null) throw new ArgumentNullException();

            Variable = var;
            Expression = exp;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            object value;

            Expression.Evaluate(context, out value);
            Variable.Set(context, value);

            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Variable);
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Variable = Script.Deserialize(input) as IVariable;
            Expression = Script.Deserialize(input) as IExpression;
        }
    }
}
