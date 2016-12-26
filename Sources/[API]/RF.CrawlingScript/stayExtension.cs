using RF.CrawlingScript.Components;
using RF.CrawlingScript.Utilities;

namespace RF.CrawlingScript
{
    public static class StayExtension
    {
        public static StayWrapper Stay(this LogicExpression exp, bool value)
        {
            return new StayWrapper(exp, value);
        }

        public static StayWrapper Stay(this bool exp, bool value)
        {
            return new StayWrapper(exp, value);
        }
    }
}
