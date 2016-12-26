using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;

namespace RF.CrawlingScript.Utilities
{
    public class ExpressionWrapper<T>
    {
        public IExpression<T> Expression { get; private set; }

        internal ExpressionWrapper(IExpression<T> expression)
        {
            Expression = expression;
        }

        public static Setter operator <=(IVariable<T> var, ExpressionWrapper<T> wrapper)
        {
            return new Setter(var, wrapper.Expression);
        }

        public static Setter operator >=(IVariable<T> var, ExpressionWrapper<T> wrapper)
        {
            throw new InvalidOperationException();
        }

        public static Switch<T> operator ==(ExpressionWrapper<T> wrapper, CaseWrapper<T> info)
        {
            if (typeof(T) == typeof(bool)) return (new SwitchLogic(wrapper.Expression as LogicExpression) == (info as CaseWrapper<bool>)) as Switch<T>;
            if (typeof(T) == typeof(string)) return (new SwitchText(wrapper.Expression as TextExpression) == (info as CaseWrapper<string>)) as Switch<T>;
            if (typeof(T) == typeof(decimal)) return (new SwitchNumber(wrapper.Expression as NumberExpression) == (info as CaseWrapper<decimal>)) as Switch<T>;

            throw new InvalidOperationException();

            
        }

        public static Switch<T> operator !=(ExpressionWrapper<T> wrapper, CaseWrapper<T> info)
        {
            throw new InvalidOperationException();
        }
    }
}
