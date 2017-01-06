using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RF.CrawlingScript.Utilities.Http
{
    class MultiPartRequestData : IRequestData
    {
        private List<object[]> Data = new List<object[]>();

        public const string Boundary = "--AaB03x";

        public string ContentType
        {
            get
            {
                return "multipart/form-data; boundary=" + Boundary;
            }
        }

        public string Accept { get; set; }

        public int Count { get { return Data.Count; } }

        public void Add(string name, string fileName, byte[] data)
        {
            Data.Add(new object[] { name, fileName, data });
        }

        public void Add(string name, string value)
        {
            Data.Add(new object[] { name, value });
        }

        public byte[] EncodedData
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                StreamWriter wr = new StreamWriter(ms);

                foreach(object[] i in Data)
                {
                    wr.Write("--" + Boundary + "\r\n");

                    if (i.Length > 2)
                    {
                        
                        wr.Write(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"", i[0], i[1]) + "\r\n");
                        wr.Write("\r\n");

                        wr.Flush();
                        ms.Write(i[2] as byte[], 0, (i[2] as byte[]).Length);
                        ms.Flush();
                        wr.Write("\r\n");
                    }
                    else
                    {
                        wr.Write(string.Format("Content-Disposition: form-data; name=\"{0}\"", i[0]) + "\r\n");
                        wr.Write("\r\n");
                        wr.Write(i[1] as string);
                        wr.Write("\r\n");
                    }
                }

                wr.Write("--" + Boundary + "--" + "\r\n");
                wr.Flush();

                byte[] result = ms.ToArray();

                wr.Close();
                ms.Close();
                
                return result;
            }
        }
    }
}
