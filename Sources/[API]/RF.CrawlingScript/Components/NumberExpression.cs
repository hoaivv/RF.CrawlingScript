using RF.CrawlingScript.Definitions;

namespace RF.CrawlingScript.Components
{
    public abstract partial class NumberExpression : Value<decimal> { }

    partial class NumberExpression // convention
    {
        public static implicit operator NumberExpression(decimal value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(byte value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(sbyte value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(int value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(uint value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(long value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(ulong value)
        {
            return new NumberValue(value);
        }

        public static implicit operator NumberExpression(float value)
        {
            return new NumberValue((decimal)value);
        }

        public static implicit operator NumberExpression(double value)
        {
            return new NumberValue((decimal)value);
        }
    }

    partial class NumberExpression // number operators
    {
        public static NumberExpression operator +(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCombineExpression(d1, d2, NumberOperations.Add);
        }

        public static NumberExpression operator -(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCombineExpression(d1, d2, NumberOperations.Subtract);
        }

        public static NumberExpression operator *(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCombineExpression(d1, d2, NumberOperations.Multiply);
        }

        public static NumberExpression operator /(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCombineExpression(d1, d2, NumberOperations.Divide);
        }

        public static NumberExpression operator %(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCombineExpression(d1, d2, NumberOperations.Module);
        }
    }

    partial class NumberExpression // comparation operators
    { 
        public static LogicExpression operator >(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCompareExpression(d1, d2, NumberCompareOperations.Greater);
        }

        public static LogicExpression operator <(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCompareExpression(d1, d2, NumberCompareOperations.Lesser);
        }

        public static LogicExpression operator >=(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCompareExpression(d1, d2, NumberCompareOperations.GreaterOrEqual);
        }

        public static LogicExpression operator <=(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCompareExpression(d1, d2, NumberCompareOperations.LesserOrEqual);
        }

        public static LogicExpression operator ==(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCompareExpression(d1, d2, NumberCompareOperations.Equal);
        }
        public static LogicExpression operator !=(NumberExpression d1, NumberExpression d2)
        {
            return new NumberCompareExpression(d1, d2, NumberCompareOperations.NotEqual);
        }
    }
}
