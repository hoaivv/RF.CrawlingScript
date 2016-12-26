using RF.CrawlingScript.Components;

namespace RF.CrawlingScript.Structures
{
    public class TestText
    {
        private TextExpression Expression { get; set; }

        public TestText(TextExpression exp)
        {
            Expression = exp;
        }

        public static SwitchText operator +(TestText test, On<string> on)
        {
            return (new SwitchText(test.Expression)) + on;
        }
    }
}
