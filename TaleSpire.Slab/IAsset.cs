namespace TaleSpire.Slab
{
    /// <summary>
    /// Represents an asset.
    /// </summary>
    public interface IAsset
    {
        /// <summary>
        /// Gets the position of the asset.
        /// </summary>
        public Float3 Position { get; }

        /// <summary>
        /// Gets the rotation of the asset in radians.
        /// </summary>
        public float Rotation { get; }
    }
}
