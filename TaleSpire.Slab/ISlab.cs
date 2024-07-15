using System;
using System.IO;

namespace TaleSpire.Slab
{
    /// <summary>
    /// Represents a slab for TaleSpire.
    /// </summary>
    public interface ISlab : IEquatable<ISlab>
    {
        /// <summary>
        /// Gets the version.
        /// </summary>
        public ushort Version { get; }

        /// <summary>
        /// Writes the slab to a binary writer.
        /// </summary>
        /// <param name="w">The binary writer.</param>
        public void Write(BinaryWriter w);
    }
}
