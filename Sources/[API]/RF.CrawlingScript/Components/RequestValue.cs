using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using System;
using System.IO;

[assembly: SerializerContract("r", typeof(RequestValue))]

namespace RF.CrawlingScript.Components
{
    public class RequestValue : RequestExpression
    {
        private RequestData Request { get; set; }

        public RequestValue() { }

        public RequestValue(RequestData request)
        {
            if ((object)request == null) throw new ArgumentNullException();
            Request = request;
        }

        public override void Evaluate(Context context, out object result)
        {
            result = Request;
        }

        public override void Serialize(BinaryWriter output)
        {
            Request.Serialize(output);
        }

        public override void Deserialize(BinaryReader input)
        {
            Request = new RequestData();
            Request.Deserialize(input);
        }
    }
}
