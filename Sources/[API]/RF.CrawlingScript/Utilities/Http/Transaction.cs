using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Linq;

namespace RF.CrawlingScript.Utilities.Http
{
    public class Transaction
	{
        static Transaction()
        {
            ServicePointManager.DefaultConnectionLimit = 999999;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

		public CookieContainer Cookies = new CookieContainer();

		public string Referer;

		private byte[] responseData = new byte[0];

		public WebProxy Proxy;

		private HttpStatusCode responseStatusCode = HttpStatusCode.Unused;

        private string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.80 Safari/537.36";

		public byte[] ResponseData
		{
			get
			{
				return this.responseData;
			}
		}

		public HttpStatusCode ResponseStatusCode
		{
			get
			{
				return this.responseStatusCode;
			}
		}

		public Transaction()
		{
		}

		public Transaction Clone()
		{
			Transaction transaction = new Transaction()
			{
				Cookies = this.Cookies,
				Referer = this.Referer,
				Proxy = this.Proxy
			};
			return transaction;
		}

		public bool Request(string uri)
		{
			return this.Request(uri, null, true);
		}

		public bool Request(string uri, bool saveAsReferer)
		{
			return this.Request(uri, null, saveAsReferer);
		}

		public bool Request(string uri, IRequestData postData)
		{
			return this.Request(uri, postData, true);
		}

		public bool Request(string uri, IRequestData postData, bool saveAsReferer)
		{
			HttpWebRequest referer;
			HttpWebResponse response;
			
			try
			{
				referer = (HttpWebRequest)WebRequest.Create(uri);
                
			}
			catch (UriFormatException)
			{
				this.responseStatusCode = HttpStatusCode.NotFound;

                return false;
			}

			referer.Referer = this.Referer;
			referer.UserAgent = this.UserAgent;
			referer.Proxy = this.Proxy;
			referer.CookieContainer = this.Cookies;
            referer.AllowAutoRedirect = false;

			if (postData == null)
			{
				referer.Method = "GET";
				referer.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*.q=0.8";
			}
			else if (postData.Count <= 0)
			{
				referer.Method = "GET";
				referer.Accept = postData.Accept == null ? "text/html,application/xhtml+xml,application/xml;q=0.9,*/*.q=0.8" : postData.Accept; ;
			}
			else
			{
				referer.Method = "POST";
				referer.Accept = postData.Accept == null ? "text/html,application/xhtml+xml,application/xml;q=0.9,*/*.q=0.8" : postData.Accept;
				byte[] bytes = postData.EncodedData;
				referer.ContentType = postData.ContentType;
				referer.ContentLength = (long)((int)bytes.Length);
				try
				{
					Stream requestStream = referer.GetRequestStream();
					requestStream.Write(bytes, 0, (int)bytes.Length);
					requestStream.Close();
				}
				catch (WebException)
				{
					this.responseStatusCode = HttpStatusCode.RequestTimeout;

                    return false;
				}
			}
			try
			{
				response = (HttpWebResponse)referer.GetResponse();
			}
			catch (WebException exp)
			{
				response = (HttpWebResponse)exp.Response;
			}
			try
			{
				this.responseStatusCode = response.StatusCode;
			}
			catch (NullReferenceException)
			{
				this.responseStatusCode = HttpStatusCode.BadRequest;

                return false;
			}

            if (response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                if (response.Headers["Location"].IndexOf("http") == 0)
                {
                    return Request(response.Headers["Location"], postData, saveAsReferer);
                }
                else
                {
                    return Request(response.ResponseUri.Scheme + "://" + response.ResponseUri.Host + response.Headers["Location"], postData, saveAsReferer);
                }
            }

			if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.ServiceUnavailable)
			{
				return false;
			}
			if (saveAsReferer)
			{
				this.Referer = response.ResponseUri.OriginalString;
			}
			try
			{
				Stream responseStream = response.GetResponseStream();
				MemoryStream memoryStream = new MemoryStream();
				byte[] numArray = new byte[16384];
				while (true)
				{
					int num = responseStream.Read(numArray, 0, (int)numArray.Length);
					int num1 = num;
					if (num <= 0)
					{
						break;
					}
					memoryStream.Write(numArray, 0, num1);
					Thread.Sleep(10);
				}
				this.responseData = memoryStream.ToArray();
				memoryStream.Close();
				responseStream.Close();
				response.Close();
				
                return this.responseStatusCode == HttpStatusCode.OK;
			}
			catch (WebException webException2)
			{
				this.responseStatusCode = HttpStatusCode.BadRequest;

                return false;
			}
			catch (IOException oException)
			{
				this.responseStatusCode = HttpStatusCode.BadRequest;

                return false;
			}
		}
    }
}