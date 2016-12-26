using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using RF.CrawlingScript.Utilities;

namespace RF.CrawlingScript
{
    public static class EachExtension
    {
        public static EachWrapper Each(this DictionaryExpression set, TextPairVariable var)
        {
            return new EachWrapper(set, var);
        }

        public static EachWrapper Each(this Matches set, DictionaryVariable var)
        {
            return new EachWrapper(set, var);
        }
    }
}
