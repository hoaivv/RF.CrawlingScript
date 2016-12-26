using RF.CrawlingScript.Definitions;
using System.IO;

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Defines RFCScript executable code
    /// </summary>
    public abstract class Code : ICode
    {
        /// <summary>
        /// Execute a code. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="isBreaking">Indicates whether the code is demanded to be broken half way</param>
        public abstract void Execute(Context context, out bool isBreaking);

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
}
