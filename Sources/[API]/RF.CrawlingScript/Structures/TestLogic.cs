using RF.CrawlingScript.Components;
using System;

namespace RF.CrawlingScript.Structures
{
    public class TestLogic
    {
        private LogicExpression Expression { get; set; }

        public TestLogic(LogicExpression exp)
        {
            Expression = exp;
        }

        public static If operator > (TestLogic test, On<bool> on)
        {
            return (new If(test.Expression)) + on;
        }

        public static Until operator > (TestLogic test, loop loop)
        {
            return new Until(test.Expression, loop);
        }

        public static If operator <(TestLogic test, On<bool> on)
        {
            throw new InvalidOperationException();
        }

        public static Until operator <(TestLogic test, loop loop)
        {
            throw new InvalidOperationException();
        }
    }
}
