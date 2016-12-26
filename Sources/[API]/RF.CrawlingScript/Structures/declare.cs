using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System;
using System.Collections.Generic;

namespace RF.CrawlingScript.Structures
{
    public class Declare : Code
    {
        private int Name { get; set; }
        private IExpression Value { get; set; }
        private VariableTypes Type { get; set; }

        public Declare(int name, IExpression value, VariableTypes type)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            isBreaking = false;

            if (context.HasVariable(Name)) throw new InvalidOperationException("variable is already declared");

            object value = null;
            Value?.Evaluate(context, out value);

            switch(Type)
            {
                case VariableTypes.Logic: context.SetVariable(Name, value != null && value is bool ? value : false); break;
                case VariableTypes.Number: context.SetVariable(Name, value != null && value is decimal ? value : 0); break;
                case VariableTypes.Text: context.SetVariable(Name, value != null && value is string ? value : ""); break;
                case VariableTypes.Dictionary: context.SetVariable(Name, new Dictionary<string, string>()); break;
            }
        }
    }
}
