using System.IO;

namespace TaleSpire.Slab.V1
{
    /// <summary>
    /// The header data for a layout.
    /// </summary>
    public readonly struct LayoutData
    {
        /// <summary>
        /// Gets the kind of asset.
        /// </summary>
        public NGuid AssetKindId { get; }

        /// <summary>
        /// Gets the number of assets in the layout.
        /// </summary>
        public ushort AssetCount { get; }

        /// <summary>
        /// Creates a new layout header structure.
        /// </summary>
        /// <param name="assetKindId">The kind of asset.</param>
        /// <param name="assetCount">The number of assets.</param>
        public LayoutData(NGuid assetKindId, ushort assetCount)
        {
            AssetKindId = assetKindId;
            AssetCount = assetCount;
        }

        /// <summary>
        /// Creates a new layout data.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public LayoutData(BinaryReader reader)
        {
            AssetKindId = new NGuid(reader);
            AssetCount = reader.ReadUInt16();

            // The docs say reserved, but we know this is because of memory alignment: C# likes to splice in 2 more bytes...
            _ = reader.ReadUInt16();
        }

        /// <summary>
        /// Writes the layout data.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
        {
            AssetKindId.Write(writer);
            writer.Write(AssetCount);
            writer.Write((ushort)0); // reserved
        }

        /// <summary>
        /// Converts the layout data to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0} ({1} layouts)",
                AssetKindId, AssetCount);
        }
    }
}
