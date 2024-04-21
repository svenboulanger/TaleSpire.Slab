using System;

namespace TaleSpire.Slab
{
    /// <summary>
    /// A layout in TaleSpire.
    /// </summary>
    public interface ILayout
    {
        /// <summary>
        /// Gets the identifier of the layout.
        /// </summary>
        public Guid AssetKindId { get; }

        /// <summary>
        /// Gets the number of assets.
        /// </summary>
        public int AssetCount { get; }
    }
}
