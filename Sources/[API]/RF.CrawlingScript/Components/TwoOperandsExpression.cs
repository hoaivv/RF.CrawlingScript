using RF.CrawlingScript.Definitions;
using System;
using System.IO;

namespace RF.CrawlingScript.Components
{
    public abstract class TwoOperandsExpression<TOperand, TOperation, TResult> : Value<TResult>
    {
        protected TOperand Operand1 { get; private set; }
        protected TOperand Operand2 { get; private set; }
        protected TOperation Operation { get; private set; }

        public TwoOperandsExpression(TOperand operand1, TOperand operand2, TOperation operation)
        {
            if (operand1 == null || operand2 == null || operation == null) throw new ArgumentNullException();

            Operand1 = operand1;
            Operand2 = operand2;
            Operation = operation;
        }
    }
}
