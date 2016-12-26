using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.ToNumber", typeof(ToNumber))]

namespace RF.CrawlingScript.Functions
{
    public class ToNumber : NumberExpression
    {
        private IExpression Expression { get; set; }

        public ToNumber() { }

        public ToNumber(IExpression exp)
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

            decimal buffer = 0;

            if (obj != null)
            {
                if (obj is bool)
                {
                    buffer = (bool)obj == true ? 1 : 0;
                }
                else if (obj is decimal)
                {
                    buffer = (decimal)obj;
                }
                else
                {
                    if (!decimal.TryParse(obj.ToString(), out buffer))
                    {
                        buffer = 0;
                    }
                }
            }

            result = buffer;
        }
    }
}
