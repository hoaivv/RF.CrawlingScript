using System;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;

namespace RF.CrawlingScript.Components
{
    public abstract partial class TextExpression : Value<string> { }
    
    partial class TextExpression // convention
    {
        public static implicit operator TextExpression(string str)
        {
            return new TextValue(str);
        }
    }

    partial class TextExpression // text operators
    {
        public static TextExpression operator +(TextExpression s1, TextExpression s2)
        {
            return new TextCombineExpression(s1, s2);
        }
    }

    partial class TextExpression // logic operators
    { 
        public static LogicExpression operator >(TextExpression s1, TextExpression s2)
        {
            return new TextCompareExpression(s1, s2, TextCompareOperations.Greater);
        }

        public static LogicExpression operator <(TextExpression s1, TextExpression s2)
        {
            return new TextCompareExpression(s1, s2, TextCompareOperations.Lesser);
        }

        public static LogicExpression operator ==(TextExpression s1, TextExpression s2)
        {
            return new TextCompareExpression(s1, s2, TextCompareOperations.Equal);
        }

        public static LogicExpression operator !=(TextExpression s1, TextExpression s2)
        {
            return new TextCompareExpression(s1, s2, TextCompareOperations.NotEqual);
        }
    }

    partial class TextExpression // common
    {
        public ToLower ToLower()
        {
            return new ToLower(this);
        }

        public ToUpper ToUpper()
        {
            return new ToUpper(this);
        }

        public ToASCII ToASCII()
        {
            return new ToASCII(this);
        }

        public Replace Replace(TextExpression oldValue, TextExpression newValue)
        {
            return new Replace(this, oldValue, newValue);
        }

        public FirstIndex IndexOf(TextExpression value)
        {
            return new FirstIndex(value, this);
        }

        public LastIndex LastIndexOf(TextExpression value)
        {
            return new LastIndex(value, this);
        }

        public Length Length
        {
            get
            {
                return new Length(this);
            }
        }

        public SubString SubString(NumberExpression start)
        {
            return new SubString(this, start);
        }

        public SubString SubString(NumberExpression start, NumberExpression length)
        {
            return new SubString(this, start, length);
        }

        public IsNumber IsNumber
        {
            get
            {
                return new IsNumber(this);
            }
        }
    }
}
