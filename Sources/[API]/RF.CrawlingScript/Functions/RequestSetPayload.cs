using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using RF.CrawlingScript.Functions;
using System.IO;

[assembly: SerializerContract("r[]=t", typeof(RequestSetPayload))]

namespace RF.CrawlingScript.Functions
{
    public class RequestSetPayload : Code
    {
        private RequestExpression Request { get; set; }
        private TextExpression ContentType { get; set; }
        private TextExpression Data { get; set; }

        public RequestSetPayload() { }

        internal RequestSetPayload(RequestExpression request, TextExpression contentType, TextExpression data)
        {
            Request = request;
            ContentType = contentType;
            Data = data;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            RequestData request; Request.Evaluate(context, out request);

            string contentType, data;

            ContentType.Evaluate(context, out contentType);
            Data.Evaluate(context, out data);

            request.Payload(contentType, data);

            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Request);
            Script.Serialize(output, ContentType);
            Script.Serialize(output, Data);
        }

        public override void Deserialize(BinaryReader input)
        {
            Request = Script.Deserialize(input) as RequestExpression;
            ContentType = Script.Deserialize(input) as TextExpression;
            Data = Script.Deserialize(input) as TextExpression;
        }
    }
}
