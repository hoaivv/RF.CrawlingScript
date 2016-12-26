using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;

namespace RF.CrawlingScript.Utilities
{
    public class OthersWrapper
    {
        public ICode Code { get; private set; }

        internal OthersWrapper(ICode code)
        {
            Code = code;
        }

        public static Switch<decimal> operator ==(Switch<decimal> self, OthersWrapper info)
        {
            if ((object)self == null || (object)info == null) throw new InvalidOperationException();

            self.Others(info.Code);

            return self;
        }

        public static Switch<decimal> operator !=(Switch<decimal> self, OthersWrapper info)
        {
            throw new InvalidOperationException();
        }

        public static Switch<string> operator ==(Switch<string> self, OthersWrapper info)
        {
            if ((object)self == null || (object)info == null) throw new InvalidOperationException();

            self.Others(info.Code);

            return self;
        }

        public static Switch<string> operator !=(Switch<string> self, OthersWrapper info)
        {
            throw new InvalidOperationException();
        }
    }
}
