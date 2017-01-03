using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RF.CrawlingScript.Utilities.Http;
using RF.CrawlingScript.Data;
using RF.CrawlingScript.Functions;

[assembly: SerializerContract("r.submit", typeof(RequestSubmit))]


namespace RF.CrawlingScript.Functions
{
    public class RequestSubmit : LogicExpression, ICode
    {
        private RequestExpression Request { get; set; }
        private IVariable Storage { get; set; }
        private LogicExpression Renew { get; set; }

        public RequestSubmit() { }

        public RequestSubmit(RequestExpression request, IVariable storage)
        {
            Request = request;
            Storage = storage;
            Renew = false;
        }

        public RequestSubmit(RequestExpression request, IVariable storage, LogicExpression renew)
        {
            Request = request;
            Storage = storage;
            Renew = renew;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Request);
            Script.Serialize(output, Storage);
            Script.Serialize(output, Renew);
        }

        public override void Deserialize(BinaryReader input)
        {
            Request = Script.Deserialize(input) as RequestExpression;
            Storage = Script.Deserialize(input) as IVariable;
            Renew = Script.Deserialize(input) as LogicExpression;
        }

        public override void Evaluate(Context context, out object result)
        {
            bool renew = false; Renew?.Evaluate(context, out renew);

            if (!context.HasObject("request.transaction") || renew) context.SetObject("request.transaction", new Transaction());

            RequestData request; Request.Evaluate(context, out request);

            result = (context.GetObject("request.transaction") as Transaction).Request(request.Url, request.PostData, request.SaveAsReferer);

            if ((bool)result && (object)Storage != null)
            {
                byte[] data = (context.GetObject("request.transaction") as Transaction).ResponseData;

                Storage.Set(context, Storage is DataVariable ? (object)data : (object)Encoding.UTF8.GetString(data));
            }
        }

        public void Execute(Context context, out bool isBreaking)
        {
            bool result; Evaluate(context, out result);
            isBreaking = false;
        }
    }
}
