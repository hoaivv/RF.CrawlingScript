using RF.CrawlingScript.Definitions;
using System;

namespace RF.CrawlingScript.Components
{
    public abstract partial class Variable<T> : Value<T>, IVariable, IExpression<T>
    {
        public int Name { get; private set; }
    }

    partial class Variable<T> // constructors
    { 
        public Variable(int name)
        {
            Name = name;
        }
    }

    partial class Variable<T> // virtual
    { 
        public virtual object DefaultValue { get { return default(T); } }


        public virtual void Evaluate(Context context, out T result)
        {
            object obj;
            Evaluate(context, out obj);

            if (!(obj is T)) throw new InvalidOperationException("variable of type " + obj.GetType() + " could not be converted to " + typeof(T));

            result = (T)obj;
        }

        public virtual void Set(Context context, object value)
        {
            if (!(value is T)) throw new InvalidOperationException("variable of type " + (value?.GetType().Name.ToString() ?? "null") + " could not be converted to " + typeof(T).Name);

            context.SetVariable(Name, value);
        }        
    }

    partial class Variable<T> // implementation
    {
        public void Evaluate(Context context, out object result)
        {
            T t; Evaluate(context, out t);
            result = t;
        }
    }
}
