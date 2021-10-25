using System;

namespace TaleSpire.Slab
{
    /// <summary>
    /// Represents a slab for TaleSpire.
    /// </summary>
    public interface ISlab : IEquatable<ISlab>
    {
        /// <summary>
        /// Exports the slab as a string that can be copied
        /// into TaleSpire.
        /// </summary>
        /// <param name="markdown">If <c>true</c>, the slab is generated surrounded by markdown ``` characters.</param>
        /// <returns>The exported slab data.</returns>
        public string Export(bool markdown = false);
    }
}
