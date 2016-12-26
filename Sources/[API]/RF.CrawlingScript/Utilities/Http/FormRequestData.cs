using System.Collections.Generic;
using System.Text;
using System.Web;

namespace RF.CrawlingScript.Utilities.Http
{
    public class FormRequestData : Dictionary<string, object>, IRequestData
	{
        public virtual string Accept { get; set; }

		public virtual string ContentType
		{
			get
			{
				return "application/x-www-form-urlencoded";
			}
		}

		public virtual byte[] EncodedData
		{
			get
			{
				string str = "";
				foreach (KeyValuePair<string, object> keyValuePair in this)
				{
					string str1 = str;
					string[] strArrays = new string[] { str1, "&", HttpUtility.UrlEncode(keyValuePair.Key), "=", HttpUtility.UrlEncode(keyValuePair.Value.ToString()) };
					str = string.Concat(strArrays);
				}
				if (str.Length > 0)
				{
					str = str.Substring(1);
				}
                return Encoding.ASCII.GetBytes(str);
			}
		}

		public FormRequestData()
		{
		}
	}
}