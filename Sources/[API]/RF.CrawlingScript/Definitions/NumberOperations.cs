namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines numberic operations supported by RFCScript
    /// </summary>
    public enum NumberOperations : byte
    {
        /// <summary>
        /// Adding to numberic value together
        /// </summary>
        Add,
        /// <summary>
        /// Substract first operand to second operand
        /// </summary>
        Subtract,

        /// <summary>
        /// Multiply first operand to second operand
        /// </summary>
        Multiply,

        /// <summary>
        /// Divide first operand to second operand
        /// </summary>
        Divide,

        /// <summary>
        /// Get module of first operand to second operand
        /// </summary>
        Module
    }
}
