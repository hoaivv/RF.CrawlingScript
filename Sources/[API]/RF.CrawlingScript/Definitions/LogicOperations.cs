namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines logical operations supported by RFCScript
    /// </summary>
    public enum LogicOperations : byte
    {
        /// <summary>
        /// Logical and operation
        /// </summary>
        And,

        /// <summary>
        /// Logical or operation
        /// </summary>
        Or,

        /// <summary>
        /// Logical xor operation
        /// </summary>
        Xor,

        /// <summary>
        /// Logical not operation
        /// </summary>
        Not,

        /// <summary>
        /// Logical equal operation
        /// </summary>
        Equal,

        /// <summary>
        /// Logical not equal operation
        /// </summary>
        NotEqual
    }
}
