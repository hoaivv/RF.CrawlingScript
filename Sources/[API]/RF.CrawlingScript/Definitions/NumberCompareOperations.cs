namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines numberic comparation operations supported by RFCScript
    /// </summary>
    public enum NumberCompareOperations : byte
    {
        /// <summary>
        /// First operand is greater than the second operand
        /// </summary>
        Greater,

        /// <summary>
        /// First operand is lesser than the second operand
        /// </summary>
        Lesser,

        /// <summary>
        /// First operand is greater or equal the second operand
        /// </summary>
        GreaterOrEqual,

        /// <summary>
        /// First operand is lesser or equal the second operand
        /// </summary>
        LesserOrEqual,

        /// <summary>
        /// First operand is equal to the second operand
        /// </summary>
        Equal,

        /// <summary>
        /// First operand is not equal to the second operand
        /// </summary>
        NotEqual
    }
}
