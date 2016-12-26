namespace RF.CrawlingScript.Definitions
{
    /// <summary>
    /// Defines text comparation operations supported by RFCScript
    /// </summary>
    public enum TextCompareOperations : byte
    {
        /// <summary>
        /// Two texts are equal
        /// </summary>
        Equal,

        /// <summary>
        /// Tow texts are not equal
        /// </summary>
        NotEqual,

        /// <summary>
        /// First text is greater than the second one
        /// </summary>
        Greater,

        /// <summary>
        /// First text is lesser than the second one
        /// </summary>
        Lesser
    }
}
