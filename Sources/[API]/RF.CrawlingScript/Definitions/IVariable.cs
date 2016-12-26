namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines RFCScript's variable
    /// </summary>
    public interface IVariable : IExpression
    {
        /// <summary>
        /// Sets value for the variable. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="value">Value to be set to the variable</param>
        void Set(Context context, object value);
    }

    /// <summary>
    /// Defines RFCScript's variable with specified base data type
    /// </summary>
    /// <typeparam name="T">Type of base data of the variable</typeparam>
    public interface IVariable<T> : IVariable, IExpression<T> { }
}
