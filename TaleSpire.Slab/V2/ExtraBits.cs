using System;

namespace TaleSpire.Slab.V2
{
    /// <summary>
    /// Extra bits for assets.
    /// </summary>
    public struct ExtraBits
    {
        private const int _size = 5;
        private const int _mask = (1 << _size) - 1;

        /// <summary>
        /// Gets the extra data.
        /// </summary>
        public byte Data { get; }

        /// <summary>
        /// Creates extra bits.
        /// </summary>
        /// <param name="data">The extra bits.</param>
        public ExtraBits(byte data)
        {
            if ((data & ~_mask) != 0)
                throw new ArgumentException("Invalid extra data");
            Data = (byte)(data & _mask);
        }

        /// <summary>
        /// Converts the extra bits to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString() => Data.ToString();
    }
}
