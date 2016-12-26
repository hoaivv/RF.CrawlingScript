using System.Collections;

namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines a set of data of RFCScript
    /// </summary>
    public interface ISet : ISerializable
    {
        /// <summary>
        /// Gets the number of data records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="result">Number of data records contained within the set</param>
        void DoCount(Context context, out int result);

        /// <summary>
        /// Gets the enumerator for records contained within the set. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="enumerator">Enumerator which could be used to access to records of the set</param>
        void GetEnumerator(Context context, out IEnumerator enumerator);

        /// <summary>
        /// Converts a record to a valid form to be passed on in RFCScript. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="element">Raw data of a record</param>
        /// <returns>Valid form of record's data to be passed on</returns>
        object Convert(object element);
    }
}
