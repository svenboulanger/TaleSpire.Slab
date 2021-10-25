namespace TaleSpire.Slab
{
    /// <summary>
    /// Represents an asset that also provides size information.
    /// </summary>
    public interface IAssetSize : IAsset
    {
        /// <summary>
        /// Gets the size of the asset.
        /// </summary>
        public Float3 Size { get; }
    }
}
