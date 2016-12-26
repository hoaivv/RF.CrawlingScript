using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Define an expression which represent data of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Type of data to be represented</typeparam>
    public abstract partial class Value<T> : IExpression<T> { }

    partial class Value<T> // IExpression<T>
    {
        private static Value<T> CurrentSettingVariable = null;

        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public void Evaluate(Context context, out T result)
        {
            object obj; Evaluate(context, out obj);

            if (!(obj is T)) throw new InvalidOperationException("could not convert value of type " + (obj?.GetType().Name ?? "null") + " to type " + typeof(T).Name);

            result = (T)obj;
        }
    }

    partial class Value<T> // operations
    { 
        /// <summary>
        /// Set current value to a variable, registered using implicit convention to <see cref="int"/>
        /// </summary>
        /// <param name="value">Value to be set to a variable</param>
        /// <param name="variable">Variable to be registered using implicit convention to <see cref="int"/></param>
        /// <returns>Instance of <see cref="Setter"/></returns>
        public static Setter operator >>(Value<T> value, int variable)
        {
            if ((object)CurrentSettingVariable == null || !(CurrentSettingVariable is IVariable<T>)) throw new InvalidOperationException();
            if ((object)value == null) throw new ArgumentNullException();

            return new Setter(CurrentSettingVariable as IVariable<T>, value);
        }

        /// <summary>
        /// Register an instance of <see cref="Value{T}"/> (which must implement <see cref="IVariable{T}"/>) for setting operation
        /// </summary>
        /// <param name="value">Variable to be registered</param>
        public static implicit operator int(Value<T> value)
        {
            if ((object)value == null || !(value is IVariable<T>)) throw new InvalidOperationException();

            CurrentSettingVariable = value;
            return 0;
        }
    }

    partial class Value<T> // abstract
    {
        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        public abstract void Evaluate(Context context, out object result);

        /// <summary>
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public abstract void Serialize(BinaryWriter output);

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public abstract void Deserialize(BinaryReader input);
    }

    partial class Value<T> // common
    {
        /// <summary>
        /// Represent current value as a <see cref="TextExpression"/>
        /// </summary>
        public ToText Text
        {
            get
            {
                return new ToText(this);
            }
        }

        /// <summary>
        /// Represent current value as a <see cref="NumberExpression"/>
        /// </summary>
        public ToNumber Number
        {
            get
            {
                return new ToNumber(this);
            }
        }

        /// <summary>
        /// Represent current value as a <see cref="LogicExpression"/>
        /// </summary>
        public ToLogic Logic
        {
            get
            {
                return new ToLogic(this);
            }
        }

        /// <summary>
        /// Represent current value as a <see cref="DataExpression"/> 
        /// </summary>
        public ToData Data
        {
            get
            {
                return new ToData(this);
            }
        }
    }

}
