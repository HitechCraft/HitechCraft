namespace DAL.Entity
{
    internal class BaseEntity<TIdentifier> where TIdentifier : new()
    {
        /// <summary>
        /// Gets or sets the Identifier.
        /// </summary>
        internal virtual TIdentifier Id { get; set; }
    }
}
