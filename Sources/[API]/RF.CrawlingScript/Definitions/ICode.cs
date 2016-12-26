using System.IO;

namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines RFCScript executable code
    /// </summary>
    public interface ICode : ISerializable
    {
        /// <summary>
        /// Execute a code. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="isBreaking">Indicates whether the code is demanded to be broken half way</param>
        void Execute(Context context, out bool isBreaking);
    }
}
