using RF.CrawlingScript;
using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using System;
using System.IO;

[assembly: SerializerContract("r.create", typeof(request))]

namespace RF.CrawlingScript
{
    public class request : RequestExpression
    {
        private TextExpression URL { get; set; }
        private LogicExpression SaveAsReferer { get; set; }

        public request() { }

        public request(TextExpression url, LogicExpression saveAsReferer)
        {
            if ((object)url == null || (object)saveAsReferer == null) throw new ArgumentNullException();

            URL = url;
            SaveAsReferer = saveAsReferer;
        }

        public override void Evaluate(Context context, out object result)
        {
            string url; bool saveAsReferer;

            URL.Evaluate(context, out url);
            SaveAsReferer.Evaluate(context, out saveAsReferer);

            result = new RequestData() { Url = url, SaveAsReferer = saveAsReferer };
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, URL);
            Script.Serialize(output, SaveAsReferer);
        }

        public override void Deserialize(BinaryReader input)
        {
            URL = Script.Deserialize(input) as TextExpression;
            SaveAsReferer = Script.Deserialize(input) as LogicExpression;
        }
    }
}
