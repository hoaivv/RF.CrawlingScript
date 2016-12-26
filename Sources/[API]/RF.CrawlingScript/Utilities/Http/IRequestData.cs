using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF.CrawlingScript.Utilities.Http
{
    public interface IRequestData
    {
        string Accept { get; set; }
        string ContentType { get; }
        byte[] EncodedData { get; }
        int Count { get; }
    }
}
