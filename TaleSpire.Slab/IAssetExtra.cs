namespace TaleSpire.Slab
{
    /// <summary>
    /// Represents an asset that also provides some extra data.
    /// </summary>
    public interface IAssetExtra<T> : IAsset
    {
        /// <summary>
        /// Gets a byte of extra data.
        /// </summary>
        public T Extra { get; }
    }
}
