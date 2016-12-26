using System.IO;

namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines an expression of RFCScript
    /// </summary>
    public interface IExpression : ISerializable
    {
        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        void Evaluate(Context context, out object result);
    }

    /// <summary>
    /// Defines an expression with specified return type of RFCScript
    /// </summary>
    /// <typeparam name="T">Type of data to be returned by the expression</typeparam>
    public interface IExpression<T> : IExpression
    {
        /// <summary>
        /// Evalutes the expression. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context on which the script is running</param>
        /// <param name="result">Result of the expression evaluation</param>
        void Evaluate(Context context, out T result);
    }
}
