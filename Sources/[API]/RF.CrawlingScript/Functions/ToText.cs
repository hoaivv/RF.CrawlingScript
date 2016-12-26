using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System.IO;
using System.Text;

[assembly: SerializerContract("f.ToText", typeof(ToText))]

namespace RF.CrawlingScript.Functions
{
    public class ToText : TextExpression
    {
        private IExpression Expression { get; set; }

        public ToText() { }

        public ToText(IExpression exp) { Expression = exp; }

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
            object obj;

            Expression.Evaluate(context, out obj);

            result = obj == null ? "" : (obj is byte[] ? Encoding.UTF8.GetString(obj as byte[]) : obj.ToString());
        }
    }
}
