using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using RF.CrawlingScript.Structures;
using System.Collections;
using System.Collections.Generic;

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Define an expression which represent a dictionary
    /// </summary>
    public abstract partial class DictionaryExpression : Value<Dictionary<string, string>>, ISet
    {
        /// <summary>
        /// Convert a dictionary to <see cref="DictionaryExpression"/>
        /// </summary>
        /// <param name="value">Dictionary to be converted</param>
        public static implicit operator DictionaryExpression(Dictionary<string, string> value)
        {
            return new DictionaryValue(value);
        }

        /// <summary>
        /// Gets the enumerator for records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="enumerator">Enumerator which could be used to access to records of the set</param>
        public void GetEnumerator(Context context, out IEnumerator enumerator)
        {
            Dictionary<string, string> dict; Evaluate(context, out dict);
            enumerator = dict.GetEnumerator();
        }

        /// <summary>
        /// Converts a record to a valid form to be passed on in RFCScript. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="element">Raw data of a record</param>
        /// <returns>Valid form of record's data to be passed on</returns>
        public object Convert(object element)
        {
            return element;
        }
    }

    partial class DictionaryExpression // implement [] operator
    {
        /// <summary>
        /// Gets value of an entry, associated with a specified key within current dictionary
        /// </summary>
        /// <param name="key">Key of an entry, value of which to be returned</param>
        /// <returns></returns>
        public DictionaryExpressionValue this[int key]
        {
            get
            {
                return new DictionaryExpressionValue(this, key.ToString());
            }
        }

        /// <summary>
        /// Gets value of an entry, associated with a specified key within current dictionary
        /// </summary>
        /// <param name="key">Key of an entry, value of which to be returned</param>
        /// <returns></returns>
        public DictionaryExpressionValue this[TextExpression key]
        {
            get
            {
                return new DictionaryExpressionValue(this, key);
            }
        }


        /// <summary>
        /// Get the number of entry contained within current dictionary
        /// </summary>
        public Count Count
        {
            get
            {
                return new Count(this);
            }
        }

        /// <summary>
        /// Gets the number of data records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        public abstract void DoCount(Context context, out int result);
    }

}
