using System;
using System.Collections.Generic;
using System.IO;

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
        
        /// <summary>
        /// Gets the assets in the layout.
        /// </summary>
        public IEnumerable<IAsset> Assets { get; }

        /// <summary>
        /// Writes the layout to a binary writer.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(BinaryWriter w);
    }
}
