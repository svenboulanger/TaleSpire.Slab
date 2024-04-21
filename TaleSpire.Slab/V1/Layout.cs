using System;

namespace TaleSpire.Slab.V1
{
    /// <summary>
    /// A layout, defined by layout data, of which assets are instantiated multiple times.
    /// </summary>
    public readonly struct Layout : ILayout, IEquatable<Layout>
    {
        /// <summary>
        /// Gets the layout data.
        /// </summary>
        public LayoutData Data { get; }

        /// <summary>
        /// Gets the assets in the layout data.
        /// </summary>
        public AssetData[] Assets { get; }

        /// <inheritdoc/>
        public Guid AssetKindId => Data.AssetKindId;

        /// <inheritdoc/>
        public int AssetCount => Assets.Length;

        /// <summary>
        /// Creates a new layout.
        /// </summary>
        /// <param name="data">The data.</param>
        public Layout(LayoutData data)
        {
            Data = data;
            Assets = new AssetData[data.AssetCount];
        }

        /// <summary>
        /// Determines whether the layout is identical to another one.
        /// </summary>
        /// <param name="other">The other layout.</param>
        /// <returns>Returns <c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Layout other)
        {
            if (!Data.Equals(other.Data))
                return false;
            if (Assets.Length != other.Assets.Length)
                return false;
            for (int i = 0; i < Assets.Length; i++)
            {
                if (!Assets[i].Equals(other.Assets[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Converts the layout to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
            => Data.ToString();
    }
}
