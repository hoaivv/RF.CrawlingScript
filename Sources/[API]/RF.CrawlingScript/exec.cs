using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;
using RF.CrawlingScript.Utilities;
using System;

namespace RF.CrawlingScript
{
    public class exec : CodeBlock
    {
        public static CaseWrapper<bool> operator >(bool value, exec code)
        {
            return new CaseWrapper<bool>(value, code);
        }

        public static CaseWrapper<bool> operator <(bool value, exec code)
        {
            throw new InvalidOperationException();
        }

        public static CaseWrapper<string> operator >(string value, exec code)
        {
            return new CaseWrapper<string>(value, code);
        }

        public static CaseWrapper<string> operator <(string value, exec code)
        {
            throw new InvalidOperationException();
        }

        public static CaseWrapper<decimal> operator >(decimal value, exec code)
        {
            return new CaseWrapper<decimal>(value, code);
        }

        public static CaseWrapper<decimal> operator <(decimal value, exec code)
        {
            throw new InvalidOperationException();
        }

        public static Switch<decimal> operator !=(Switch<decimal> self, exec info)
        {
            self.Others(info);

            return self;
        }

        public static Switch<decimal> operator ==(Switch<decimal> self, exec info)
        {
            throw new InvalidOperationException();
        }

        public static Switch<string> operator !=(Switch<string> self, exec info)
        {
            self.Others(info);

            return self;
        }

        public static Switch<string> operator ==(Switch<string> self, exec info)
        {
            throw new InvalidOperationException();
        }

    }
}
