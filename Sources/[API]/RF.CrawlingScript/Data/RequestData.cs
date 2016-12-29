using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Utilities.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace RF.CrawlingScript.Data
{
    public class RequestData : ISerializable
    {
        private List<string> PostFormName { get; set; } = new List<string>();
        private List<string> PostDataName { get; set; } = new List<string>();
        private List<object> PostDataValue { get; set; } = new List<object>();

        public string Url { get; set; } = null;
        public string Accept { get; set; } = null;
        public bool SaveAsReferer { get; set; } = false;

        public void Serialize(BinaryWriter output)
        {
            output.Write(Url == null ? string.Empty : Url);
            output.Write(Accept == null ? string.Empty : Accept);
            output.Write(SaveAsReferer);

            output.Write(PostFormName.Count);

            for(int i = 0; i < PostFormName.Count; i++)
            {
                output.Write(PostFormName[i] == null ? (byte)0 : (byte)1);
                if (PostFormName[i] != null) output.Write(PostFormName[i]);

                output.Write(PostDataName[i] == null ? (byte)0 : (byte)1);
                if (PostDataName[i] != null) output.Write(PostDataName[i]);

                output.Write(PostDataValue[i] == null ? (int)0 : (PostDataValue[i] is string ? (int)-1 : (PostDataValue[i] as byte[]).Length));
                if (PostDataValue[i] != null)
                {
                    if (PostDataValue[i] is string)
                    {
                        output.Write(PostDataValue[i] as string);
                    }
                    else
                    {
                        output.Write(PostDataValue[i] as byte[], 0, (PostDataValue[i] as byte[]).Length);
                    }
                }
            }
        }

        public void Deserialize(BinaryReader input)
        {
            Url = input.ReadString(); Url = string.IsNullOrEmpty(Url) ? null : Url;
            Accept = input.ReadString(); Accept = string.IsNullOrEmpty(Accept) ? null : Accept;
            SaveAsReferer = input.ReadBoolean();

            int count = input.ReadInt32();

            PostFormName.Clear();
            PostDataName.Clear();
            PostDataValue.Clear();

            for(int i = 0; i < count; i++)
            {
                byte check1 = input.ReadByte();
                
                PostFormName[i] = check1 == 0 ? null : input.ReadString();

                check1 = input.ReadByte();

                PostDataName[i] = check1 == 0 ? null : input.ReadString();

                int check2 = input.ReadInt32();

                PostDataValue[i] = check2 > 0 ? (object)input.ReadBytes(check2) : (check2 < 0 ? input.ReadString() : null);
            }
        }

        public void Post(string formName, string formData)
        {
            if (formName == null || formData == null) throw new ArgumentNullException();

            if (PostFormName.Count > 0 && (object)PostFormName[0] == null) throw new InvalidOperationException();

            PostFormName.Add(formName);
            PostDataName.Add(null);
            PostDataValue.Add(formData);
        }

        public void Payload(string contentType, string data)
        {
            if ((object)contentType == null || (object)data == null) throw new ArgumentNullException();

            if (PostFormName.Count > 0) throw new InvalidOperationException();

            PostFormName.Add(null);
            PostDataName.Add(contentType);
            PostDataValue.Add(data);
        }

        public void Post(string formName, string fileName, byte[] data)
        {
            if ((object)formName == null || (object)data == null || (object)fileName == null) throw new ArgumentNullException();

            if (PostFormName.Count > 0 && (object)PostFormName[0] == null) throw new InvalidOperationException();

            PostFormName.Add(formName);
            PostDataName.Add(fileName);
            PostDataValue.Add(data);
        }

        public IRequestData PostData
        {
            get
            {
                if (PostDataValue.Count > 0)
                {
                    bool fileMode = false;

                    foreach (object i in PostDataValue)
                    {
                        if (i != null && i is byte[])
                        {
                            fileMode = true;
                            break;
                        }
                    }

                    bool textMode = !fileMode && PostDataValue.Count == 1 && (object)PostFormName[0] == null;

                    if (textMode)
                    {
                        return new TextRequestData(PostDataName[0], (string)PostDataValue[0]);
                    }
                    else
                    {
                        IRequestData post = fileMode ? new MultiPartRequestData() as IRequestData : new FormRequestData() as IRequestData;

                        for (int i = 0; i < PostDataValue.Count; i++)
                        {
                            if (PostDataName[i] == null)
                            {
                                if (fileMode)
                                {
                                    (post as MultiPartRequestData).Add(PostFormName[i], PostDataValue[i] as string);
                                }
                                else
                                {
                                    (post as FormRequestData)[PostFormName[i]] = PostDataValue[i] as string;
                                }
                            }
                            else
                            {
                                (post as MultiPartRequestData).Add(PostFormName[i], PostDataName[i], PostDataValue[i] as byte[]);
                            }
                        }

                        if (Accept != null)
                        {
                            post.Accept = Accept;
                        }

                        return post;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
