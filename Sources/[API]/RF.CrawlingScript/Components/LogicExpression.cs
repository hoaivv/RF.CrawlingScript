using RF.CrawlingScript.Definitions;
using System;

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Defines an expression which represent a logical value (boolean)
    /// </summary>
    public abstract partial class LogicExpression : Value<bool> { }

    partial class LogicExpression // convention
    {
        /// <summary>
        /// Convert <see cref="bool"/> value to <see cref="LogicExpression"/>
        /// </summary>
        /// <param name="value">Value to be converted</param>
        public static implicit operator LogicExpression(bool value)
        {
            return new LogicValue(value);
        }
    }

    partial class LogicExpression // logic operators
    {
        /// <summary>
        /// Performs logical and operation
        /// </summary>
        /// <param name="v1">First operand</param>
        /// <param name="v2">Second operand</param>
        /// <returns><see cref="LogicExpression"/> which represent result of the operation</returns>
        public static LogicExpression operator &(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.And);
        }

        /// <summary>
        /// Performs logical or operation
        /// </summary>
        /// <param name="v1">First operand</param>
        /// <param name="v2">Second operand</param>
        /// <returns><see cref="LogicExpression"/> which represent result of the operation</returns>
        public static LogicExpression operator |(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.Or);
        }

        /// <summary>
        /// Performs logical xor operation
        /// </summary>
        /// <param name="v1">First operand</param>
        /// <param name="v2">Second operand</param>
        /// <returns><see cref="LogicExpression"/> which represent result of the operation</returns>
        public static LogicExpression operator ^(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.Xor);
        }

        /// <summary>
        /// Performs logical not operation
        /// </summary>
        /// <param name="v">Operand</param>
        /// <returns><see cref="LogicExpression"/> which represent result of the operation</returns>
        public static LogicExpression operator !(LogicExpression v)
        {
            return new LogicNotExpression(v);
        }
    }

    partial class LogicExpression // comparation operators
    {
        /// <summary>
        /// Compares equality of two <see cref="LogicExpression"/>
        /// </summary>
        /// <param name="v1">First operand</param>
        /// <param name="v2">Second operand</param>
        /// <returns><see cref="LogicExpression"/> which represent result of the operation</returns>
        public static LogicExpression operator ==(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.Equal);
        }

        /// <summary>
        /// Compares inequality of two <see cref="LogicExpression"/>
        /// </summary>
        /// <param name="v1">First operand</param>
        /// <param name="v2">Second operand</param>
        /// <returns><see cref="LogicExpression"/> which represent result of the operation</returns>
        public static LogicExpression operator !=(LogicExpression v1, LogicExpression v2)
        {
            return new LogicCombineExpression(v1, v2, LogicOperations.NotEqual);
        }
    }
}
