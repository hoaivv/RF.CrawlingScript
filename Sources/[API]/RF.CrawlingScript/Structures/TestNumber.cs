using RF.CrawlingScript.Components;

namespace RF.CrawlingScript.Structures
{
    public class TestNumber
    {
        private NumberExpression Expression { get; set; }

        public TestNumber(NumberExpression exp)
        {
            Expression = exp;
        }

        public static SwitchNumber operator +(TestNumber test, On<decimal> on)
        {
            return (new SwitchNumber(test.Expression)) + on;
        }
    }
}
