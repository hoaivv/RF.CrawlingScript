using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;
using System.Web;

[assembly: SerializerContract("f.HtmlDecode", typeof(HtmlDecode))]

namespace RF.CrawlingScript.Functions
{
    public class HtmlDecode : TextExpression
    {
        private TextExpression Expression { get; set; }

        public HtmlDecode() { }
        
        public HtmlDecode(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as TextExpression;
        }

        public override void Evaluate(Context context, out object result)
        {
            string text; Expression.Evaluate(context, out text);

            result = HttpUtility.HtmlDecode(text);   
        }
    }
}
