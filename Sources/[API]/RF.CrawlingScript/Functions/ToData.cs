using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;
using System.Text;

[assembly: SerializerContract("f.ToData", typeof(ToData))]

namespace RF.CrawlingScript.Functions
{
    public class ToData : DataExpression
    {
        private IExpression Expression { get; set; }

        public ToData() { }

        public ToData(IExpression exp)
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

            byte[] buffer = new byte[0];

            if (obj != null)
            {
                if (obj is bool)
                {
                    buffer = new byte[] { (bool)obj ? (byte)1 : (byte)0 };
                }
                else if (obj is decimal)
                {
                    buffer = new byte[] { (byte)(decimal)obj };
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(obj.ToString());
                }
            }

            result = buffer;
        }

        public override void DoCount(Context context, out int result)
        {
            byte[] data; Evaluate(context, out data);

            result = data.Length;
        }
    }
}
