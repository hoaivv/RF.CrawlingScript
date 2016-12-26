using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ToLogic", typeof(ToLogic))]

namespace RF.CrawlingScript.Functions
{
    public class ToLogic : LogicExpression
    {
        private IExpression Expression { get; set; }

        public ToLogic() { }

        public ToLogic(IExpression exp)
        {
            if (exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as IExpression;
        }

        public override void Evaluate(Context context, out object result)
        {
            object obj; Expression.Evaluate(context, out obj);

            bool buffer = false;

            if (obj != null)
            {
                if (obj is bool)
                {
                    buffer = (bool)obj;
                }
                else if (obj is decimal)
                {
                    buffer = (decimal)obj == 0;
                }
                else
                {
                    if (!bool.TryParse(obj.ToString(), out buffer))
                    {
                        buffer = false;
                    }
                }
            }

            result = buffer;
        }
    }
}
