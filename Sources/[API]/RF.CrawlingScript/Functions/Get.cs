using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using Shark;
using Shark.Components;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("f.Get", typeof(Get))]

namespace RF.CrawlingScript.Functions
{
    public class Get : LogicExpression
    {
        private DictionaryExpression Request { get; set; }
        private TextExpression Service { get; set; }
        private IVariable Storage { get; set; }

        public Get() { }

        public Get(TextExpression service, DictionaryExpression request, DataVariable storage)
        {
            if ((object)service == null || (object)storage == null) throw new ArgumentNullException();

            Service = service;
            Storage = storage;
            Request = request;
        }

        public Get(TextExpression service, DictionaryExpression request, TextVariable storage)
        {
            if ((object)service == null || (object)storage == null) throw new ArgumentNullException();

            Service = service;
            Storage = storage;
            Request = request;
        }

        public Get(TextExpression service, DictionaryExpression request, LogicVariable storage)
        {
            if ((object)service == null || (object)storage == null) throw new ArgumentNullException();

            Service = service;
            Storage = storage;
            Request = request;
        }

        public Get(TextExpression service, DictionaryExpression request, NumberVariable storage)
        {
            if ((object)service == null || (object)storage == null) throw new ArgumentNullException();

            Service = service;
            Storage = storage;
            Request = request;
        }

        public Get(TextExpression service, DictionaryExpression request, DictionaryVariable storage)
        {
            if ((object)service == null || (object)storage == null) throw new ArgumentNullException();

            Service = service;
            Storage = storage;
            Request = request;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Service);
            Script.Serialize(output, Storage);
        }

        public override void Deserialize(BinaryReader input)
        {
            Service = Script.Deserialize(input) as TextExpression;
            Storage = Script.Deserialize(input) as IVariable;
        }

        public override void Evaluate(Context context, out object result)
        {
            string name; Service.Evaluate(context, out name);
            Service service = Services.Get<Dictionary<string, string>>(name);

            if (service == null)
            {
                result = false;
                return;
            }

            Dictionary<string, string> request = null; Request?.Evaluate(context, out request);

            Storage.Set(context, service.Process(new ServiceRequestInfo<Dictionary<string, string>>(request)));

            result = true;
        }
    }
}
