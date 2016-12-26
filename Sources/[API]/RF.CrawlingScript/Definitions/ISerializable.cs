using System.IO;

namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines a serializable component of RFCScript
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// Serialize component data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        void Serialize(BinaryWriter output);

        /// <summary>
        /// Deserialize component data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        void Deserialize(BinaryReader input);
    }
}
