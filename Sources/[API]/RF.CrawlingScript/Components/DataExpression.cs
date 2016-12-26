using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System.Collections;

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Defines an expression which represent a set of byte
    /// </summary>
    public abstract partial class DataExpression : Value<byte[]>, ISet
    {
        /// <summary>
        /// Convert <see cref="byte[]"/> to <see cref="DataExpression"/>
        /// </summary>
        /// <param name="value"><see cref="byte[]"/> to be converted</param>
        public static implicit operator DataExpression(byte[] value)
        {
            return new DataValue(value);
        }

        /// <summary>
        /// Gets the enumerator for records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="enumerator">Enumerator which could be used to access to records of the set</param>
        public void GetEnumerator(Context context, out IEnumerator result)
        {
            byte[] data; Evaluate(context, out data);
            result = data.GetEnumerator();
        }

        /// <summary>
        /// Converts a record to <see cref="byte"/> to be passed on in RFCScript. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="element">Raw data of a record</param>
        /// <returns>data of type <see cref="byte"/></returns>
        public object Convert(object element)
        {
            return element;
        }

        /// <summary>
        /// Gets the number of bytes contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public abstract void DoCount(Context context, out int result);
    }

    partial class DataExpression
    {
        /// <summary>
        /// Get data at specified index of the set
        /// </summary>
        /// <param name="key">Index of the <see cref="NumberValue"/> to be get</param>
        /// <returns>data at specified index of the set</returns>
        public DataExpressionValue this[int key]
        {
            get
            {
                return new DataExpressionValue(this, key);
            }
        }

        /// <summary>
        /// Get data at specified index of the set
        /// </summary>
        /// <param name="key">Index of the <see cref="NumberValue"/> to be get</param>
        /// <returns>data at specified index of the set</returns>
        public DataExpressionValue this[NumberExpression key]
        {
            get
            {
                return new DataExpressionValue(this, key);
            }
        }

        /// <summary>
        /// Get number of byte contained within the set
        /// </summary>
        public Count Count
        {
            get
            {
                return new Count(this);
            }
        }

    }

}
