using RF.CrawlingScript.Data;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF.CrawlingScript.Components
{
    public abstract class RequestExpression : Value<RequestData>
    {
        public static implicit operator RequestExpression(RequestData value)
        {
            return new RequestValue(value);
        }

        public RequestAddUpload Post(TextExpression name, TextExpression fileName, DataExpression fileData)
        {
            return new RequestAddUpload(this, name, fileName, fileData);
        }
        public RequestSetPayload Payload(TextExpression contentType, TextExpression data)
        {
            return new RequestSetPayload(this, contentType, data);
        }

        public RequestAddPost Post(TextExpression name, TextExpression value)
        {
            return new RequestAddPost(this, name, value);
        }

        public RequestSetAccept Accept(TextExpression value)
        {
            return new RequestSetAccept(this, value);
        }

        public RequestSubmit Submit()
        {
            return new RequestSubmit(this, null);
        }

        public RequestSubmit Submit(LogicExpression renew)
        {
            return new RequestSubmit(this, null, renew);
        }

        public RequestSubmit Submit(TextVariable storage)
        {
            return new RequestSubmit(this, storage);
        }
        public RequestSubmit Submit(DataVariable storage)
        {
            return new RequestSubmit(this, storage);
        }

        public RequestSubmit Submit(TextVariable storage, LogicExpression renew)
        {
            return new RequestSubmit(this, storage, renew);
        }

        public RequestSubmit Submit(DataVariable storage, LogicExpression renew)
        {
            return new RequestSubmit(this, storage, renew);
        }
    }
}
