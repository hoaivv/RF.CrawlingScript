using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;
using System;

namespace RF.CrawlingScript.Utilities
{
    public class StayWrapper
    {
        private LogicExpression Expression { get; set; }

        internal StayWrapper(LogicExpression exp, bool value)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp == value;
        }

        public static While operator >(StayWrapper wrapper, exec code)
        {
            if ((object)code == null) throw new ArgumentNullException();

            While result = new While(wrapper.Expression);

            result.Do(code);

            return result;
        }

        public static While operator <(StayWrapper wrapper, exec code)
        {
            throw new InvalidOperationException();
        }

    }
}
