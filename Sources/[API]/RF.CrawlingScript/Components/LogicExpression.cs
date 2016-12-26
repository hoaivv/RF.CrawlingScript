using RF.CrawlingScript.Definitions;
using System;

namespace RF.CrawlingScript.Components
{
    public abstract partial class LogicExpression : Value<bool> { }

    partial class LogicExpression // convention
    {
        public static implicit operator LogicExpression(bool value)
        {
            return new LogicValue(value);
        }
    }

    partial class LogicExpression // logic operators
    {
        public static LogicExpression operator &(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.And);
        }

        public static LogicExpression operator |(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.Or);
        }

        public static LogicExpression operator ^(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.Xor);
        }

        public static LogicExpression operator !(LogicExpression v)
        {
            return new LogicNotExpression(v);
        }
    }

    partial class LogicExpression // comparation operators
    { 
        public static LogicExpression operator ==(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.Equal);
        }

        public static LogicExpression operator !=(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.NotEqual);
        }
    }
}
