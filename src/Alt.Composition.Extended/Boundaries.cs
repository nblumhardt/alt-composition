namespace Alt.Composition
{
    /// <summary>
    /// Well-known sharing boundary names.
    /// </summary>
    public static class Boundaries
    {
        /// <summary>
        /// The sharing boundary within which a single HttpContext is visible.
        /// </summary>
        public const string HttpContext = "HttpContext";

        /// <summary>
        /// The sharing boundary within which a consistent view of persisted data is
        /// available. Corresponds to a database transaction or similar unit-of-work.
        /// </summary>
        public const string DataConsistency = "DataConsistency";

        /// <summary>
        /// The sharing boundary within which actions can be attributed to a single
        /// application user.
        /// </summary>
        public const string UserIdentity = "UserIdentity";
    }
}
