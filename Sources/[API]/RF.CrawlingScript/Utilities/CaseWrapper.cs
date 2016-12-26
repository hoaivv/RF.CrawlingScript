using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;

namespace RF.CrawlingScript.Utilities
{
    public class CaseWrapper<T>
    {
        public T Value { get; private set; }
        public ICode Code { get; private set; }

        internal CaseWrapper(T value, ICode code)
        {
            if ((object)value == null || (object)code == null) throw new ArgumentNullException();

            Value = value;
            Code = code;
        }

        public static Switch<T> operator ==(IExpression<T> value, CaseWrapper<T> info)
        {
            if (typeof(T) == typeof(bool)) return new SwitchLogic(value as LogicExpression) as Switch<T> == info;
            if (typeof(T) == typeof(string)) return new SwitchText(value as TextExpression) as Switch<T> == info;
            if (typeof(T) == typeof(decimal)) return new SwitchNumber(value as NumberExpression) as Switch<T> == info;

            throw new InvalidOperationException();
        }

        public static Switch<T> operator !=(IExpression<T> value, CaseWrapper<T> info)
        {
            throw new InvalidOperationException();
        }

        public static Switch<T> operator ==(Switch<T> self, CaseWrapper<T> info)
        {
            if ((object)self == null || (object)info == null) throw new InvalidOperationException();

            self.Case(info.Value, info.Code);

            return self;
        }

        public static Switch<T> operator !=(Switch<T> self, CaseWrapper<T> info)
        {
            throw new InvalidOperationException();
        }
    }
}
