using RF.CrawlingScript.Definitions;
using System;

namespace RF.CrawlingScript.Components
{
    public abstract class OneOperandExpression<TOperand, TOperation, TResult> : Value<TResult> 
    {
        protected TOperand Operand { get; private set; }
        protected TOperation Operation { get; private set; }

        protected OneOperandExpression(TOperand operand, TOperation operation)
        {
            if (operand == null || operation == null) throw new ArgumentNullException();

            Operand = operand;
            Operation = operation;
        }
    }
}
