using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using RF.CrawlingScript.Utilities.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

[assembly: SerializerContract("f.Request", typeof(Request))]

namespace RF.CrawlingScript.Functions
{
    public class Request : LogicExpression, ICode
    {
        private TextExpression Url { get; set; }
        private TextExpression Accept { get; set; }

        private List<TextExpression> PostFormName { get; set; } = new List<TextExpression>();
        private List<TextExpression> PostDataName { get; set; } = new List<TextExpression>();
        private List<IExpression> PostDataValue { get; set; } = new List<IExpression>();

        private LogicExpression SaveAsReferer { get; set; }
        private IVariable Storage { get; set; }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Url);
            Script.Serialize(output, Accept);
            Script.Serialize(output, SaveAsReferer);
            Script.Serialize(output, Storage);

            output.Write(PostFormName.Count);

            for(int i = 0; i < PostFormName.Count; i++)
            {
                Script.Serialize(output, PostFormName[i]);
                Script.Serialize(output, PostDataName[i]);
                Script.Serialize(output, PostDataValue[i]);
            }
        }

        public override void Deserialize(BinaryReader input)
        {
            Url = Script.Deserialize(input) as TextExpression;
            Accept = Script.Deserialize(input) as TextExpression;
            SaveAsReferer = Script.Deserialize(input) as LogicExpression;
            Storage = Script.Deserialize(input) as IVariable;

            int count = input.ReadInt32();

            PostFormName.Clear();
            PostDataName.Clear();
            PostDataValue.Clear();

            for(int i = 0; i < count; i++)
            {
                PostFormName.Add(Script.Deserialize(input) as TextExpression);
                PostDataName.Add(Script.Deserialize(input) as TextExpression);
                PostDataValue.Add(Script.Deserialize(input) as IExpression);
            }
        }

        public Request() { }

        public Request(TextExpression url, LogicExpression saveAsReferer)
        {
            if ((object)url == null || (object)saveAsReferer == null) throw new ArgumentNullException();

            Url = url;
            SaveAsReferer = false;
        }

        public Request To(TextVariable storage)
        {
            if ((object)storage == null) throw new ArgumentNullException();
            Storage = storage;

            return this;
        }


        public Request To(DataVariable storage)
        {
            if ((object)storage == null) throw new ArgumentNullException();
            Storage = storage;

            return this;
        }

        public Request Post(TextExpression formName, TextExpression formData)
        {
            if ((object)formName == null || (object)formData == null) throw new ArgumentNullException();

            if (PostFormName.Count > 0 && (object)PostFormName[0] == null) throw new InvalidOperationException();

            PostFormName.Add(formName);
            PostDataName.Add(null);
            PostDataValue.Add(formData);

            return this;
        }

        public Request Payload(TextExpression contentType, TextExpression data)
        {
            if ((object)contentType == null || (object)data == null) throw new ArgumentNullException();

            if (PostFormName.Count > 0) throw new InvalidOperationException();

            PostFormName.Add(null);
            PostDataName.Add(contentType);
            PostDataValue.Add(data);

            return this;
        }

        public Request Post(TextExpression formName, TextExpression fileName, DataExpression data)
        {
            if ((object)formName == null || (object)data == null || (object)fileName == null) throw new ArgumentNullException();

            if (PostFormName.Count > 0 && (object)PostFormName[0] == null) throw new InvalidOperationException();

            PostFormName.Add(formName);
            PostDataName.Add(fileName);
            PostDataValue.Add(data);

            return this;
        }

        public Request Receive(TextExpression accept)
        {
            if ((object)accept == null) throw new ArgumentNullException();

            Accept = accept;

            return this;
        }

        public override void Evaluate(Context context, out object result)
        {
            string url; Url.Evaluate(context, out url);
            bool saveAsReferer; SaveAsReferer.Evaluate(context, out saveAsReferer);

            IRequestData post = null;

            if (!context.HasObject("request.transaction")) context.SetObject("request.transaction", new Transaction());

            if (PostDataValue.Count > 0)
            {
                bool fileMode = false;

                foreach (IExpression i in PostDataValue)
                {
                    if ((object)i != null && i is DataExpression)
                    {
                        fileMode = true;
                        break;
                    }
                }

                bool textMode = !fileMode && PostDataValue.Count == 1 && (object)PostFormName[0] == null;


                if (textMode)
                {
                    string type, text;

                    PostDataName[0].Evaluate(context, out type);
                    (PostDataValue[0] as TextExpression).Evaluate(context, out text);

                    post = new TextRequestData(text, type);
                }
                else
                {
                    post = fileMode ? new MultiPartRequestData() as IRequestData : new FormRequestData() as IRequestData;

                    for(int i = 0; i < PostDataValue.Count; i++)
                    {
                        string formName = null;
                        string dataT = null;
                        byte[] dataB = null;

                        PostFormName[i].Evaluate(context, out formName);
                        PostDataName[i]?.Evaluate(context, out dataT);

                        if (dataT == null)
                        {
                            (PostDataValue[i] as TextExpression).Evaluate(context, out dataT);


                            if (fileMode)
                            {
                                (post as MultiPartRequestData).Add(formName, dataT);
                            }
                            else
                            {
                                (post as FormRequestData)[formName] = dataT;
                            }
                        }
                        else
                        {
                            (PostDataValue[i] as DataExpression).Evaluate(context, out dataB);

                            (post as MultiPartRequestData).Add(formName, dataT, dataB);
                        }
                    }
                }

                if ((object)Accept != null)
                {
                    string accept; Accept.Evaluate(context, out accept);
                    post.Accept = accept;
                }
            }

            result = (context.GetObect("request.transaction") as Transaction).Request(url, post, saveAsReferer);

            if ((bool)result && (object)Storage != null)
            {
                byte[] data = (context.GetObect("request.transaction") as Transaction).ResponseData;

                Storage.Set(context, Storage is DataVariable ? (object)data : (object)Encoding.UTF8.GetString(data));
            }
        }

        public void Execute(Context context, out bool isBreaking)
        {
            object value; Evaluate(context, out value);
            isBreaking = false;            
        }
    }
}
