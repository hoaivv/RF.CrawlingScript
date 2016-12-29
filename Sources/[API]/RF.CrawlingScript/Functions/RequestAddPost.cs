using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using RF.CrawlingScript.Functions;
using System.IO;

[assembly:SerializerContract("r[t]=t", typeof(RequestAddPost))]

namespace RF.CrawlingScript.Functions
{
    public class RequestAddPost : Code
    {
        private RequestExpression Request { get; set; }
        private TextExpression Name { get; set; }
        private TextExpression Value { get; set; }

        public RequestAddPost() { }

        internal RequestAddPost(RequestExpression request, TextExpression name, TextExpression value)
        {
            Request = request;
            Name = name;
            Value = value;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            RequestData request; Request.Evaluate(context, out request);

            string name, value;

            Name.Evaluate(context, out name);
            Value.Evaluate(context, out value);

            request.Post(name, value);

            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Request);
            Script.Serialize(output, Name);
            Script.Serialize(output, Value);
        }

        public override void Deserialize(BinaryReader input)
        {
            Request = Script.Deserialize(input) as RequestExpression;
            Name = Script.Deserialize(input) as TextExpression;
            Value = Script.Deserialize(input) as TextExpression;
        }
    }
}
