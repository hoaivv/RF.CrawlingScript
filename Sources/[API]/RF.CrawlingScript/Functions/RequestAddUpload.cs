using RF.CrawlingScript.Components;
using RF.CrawlingScript.Data;
using RF.CrawlingScript.Functions;
using System.IO;

[assembly: SerializerContract("r[t,t]=2", typeof(RequestAddUpload))]

namespace RF.CrawlingScript.Functions
{
    public class RequestAddUpload : Code
    {
        private RequestExpression Request { get; set; }
        private TextExpression Name { get; set; }
        private TextExpression FileName { get; set; }
        private DataExpression FileData { get; set; }

        public RequestAddUpload() { }

        internal RequestAddUpload(RequestExpression request, TextExpression name, TextExpression fileName, DataExpression fileData)
        {
            Request = request;

            Name = name;
            FileName = fileName;
            FileData = fileData;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            RequestData request; Request.Evaluate(context, out request);

            string name, file; byte[] data;

            Name.Evaluate(context, out name);
            FileName.Evaluate(context, out file);
            FileData.Evaluate(context, out data);

            request.Post(name, file, data);

            isBreaking = false;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Request);
            Script.Serialize(output, Name);
            Script.Serialize(output, FileName);
            Script.Serialize(output, FileData);
        }

        public override void Deserialize(BinaryReader input)
        {
            Request = Script.Deserialize(input) as RequestExpression;
            Name = Script.Deserialize(input) as TextExpression;
            FileName = Script.Deserialize(input) as TextExpression;
            FileData = Script.Deserialize(input) as DataExpression;
        }
    }
}
