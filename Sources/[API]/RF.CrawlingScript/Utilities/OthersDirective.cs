using System;

namespace RF.CrawlingScript.Utilities
{
    public class OthersDirective
    {
        internal OthersDirective() { }

        public static OthersWrapper operator >(OthersDirective info, exec code)
        {
            return new OthersWrapper(code);
        }

        public static OthersWrapper operator <(OthersDirective info, exec code)
        {
            throw new InvalidOperationException();
        }

    }
}
