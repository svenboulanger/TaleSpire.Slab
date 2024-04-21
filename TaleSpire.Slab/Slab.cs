using System;
using System.IO;
using System.IO.Compression;

namespace TaleSpire.Slab
{
    /// <summary>
    /// Methods involving slabs for TaleSpire.
    /// </summary>
    public static class Slab
    {
        /// <summary>
        /// The magical hex number for identifying slabs.
        /// </summary>
        public const uint MagicHex = 0xD1CEFACE;
        private const string _markdownCodeBrace = "```";

        /// <summary>
        /// Gets or sets the maximum size of slabs (after decompression).
        /// </summary>
        public static uint MaximumSize { get; set; } = 30 * 1024;

        /// <summary>
        /// Imports a slab from a slab string.
        /// </summary>
        /// <param name="maybeSlabString">The slab string.</param>
        /// <returns>The imported slab.</returns>
        public static ISlab Import(string maybeSlabString)
        {
            // Get rid of possible whitespace characters at the end and start
            maybeSlabString = maybeSlabString.Trim();

            // Strip markdown characters if necessary
            if (maybeSlabString.StartsWith(_markdownCodeBrace))
                maybeSlabString = maybeSlabString.Substring(3, maybeSlabString.Length - _markdownCodeBrace.Length * 2);

            // Decode the slab
            byte[] decoded = Convert.FromBase64String(maybeSlabString);
            if (decoded.Length > MaximumSize)
                throw new ArgumentException("Slab is too large");
            
            // Decompress the data
            using var inputStream = new MemoryStream(decoded);
            using var gzip = new GZipStream(inputStream, CompressionMode.Decompress);
            using var r = new BinaryReader(gzip);

            // Validate with the magical hex number
            if (MagicHex != r.ReadUInt32())
                throw new ArgumentException("Invalid slab");

            // Return a slab based on the version
            var version = r.ReadUInt16();
            return version switch
            {
                1 => V1.Slab.Import(r),
                2 => V2.Slab.Import(r),
                _ => throw new ArgumentException($"Invalid version: v{version}"),
            };
        }
    }
}
