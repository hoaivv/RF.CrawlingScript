using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using RF.CrawlingScript.Functions;
using System.IO;

[assembly: SerializerContract("r.accept", typeof(RequestSetAccept))]

namespace RF.CrawlingScript.Functions
{
    public class RequestSetAccept : Code
    {
        private RequestExpression Request { get; set; }
        private TextExpression Value { get; set; }

        public RequestSetAccept() { }

        internal RequestSetAccept(RequestExpression request, TextExpression value)
        {
            Request = request;
            Value = value;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            RequestData request; Request.Evaluate(context, out request);

            string value;

            Value.Evaluate(context, out value);

            request.Accept = value;

            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Request);
            Script.Serialize(output, Value);
        }

        public override void Deserialize(BinaryReader input)
        {
            Request = Script.Deserialize(input) as RequestExpression;
            Value = Script.Deserialize(input) as TextExpression;
        }
    }
}
