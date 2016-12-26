using System.Text;

namespace RF.CrawlingScript.Utilities.Http
{
    public class TextRequestData : IRequestData
	{
		public string Accept { get; set; }

		public string ContentType { get; set; }

		public string Data { get; set; }

		public TextRequestData(string data, string contentType)
		{
            Data = data;
            ContentType = contentType;            
		}

        public byte[] EncodedData
        {
            get
            {
                return Encoding.UTF8.GetBytes(Data);
            }
        }

        public int Count
        {
            get
            {
                return string.IsNullOrEmpty(Data) ? 0 : 1;
            }
        }

        public TextRequestData() { }
	}
}