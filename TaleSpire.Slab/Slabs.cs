using System;
using System.IO;
using System.IO.Compression;

namespace TaleSpire.Slab
{
    /// <summary>
    /// Helper methods and properties for TaleSpire slabs.
    /// </summary>
    public static class Slabs
    {
        /// <summary>
        /// The magical hex number for identifying slabs.
        /// </summary>
        public const uint MagicHex = 0xD1CEFACE;

        /// <summary>
        /// A brace for Markdown code.
        /// </summary>
        public const string MarkdownCodeBrace = "```";

        /// <summary>
        /// Gets or sets the maximum size of slabs (after decompression).
        /// </summary>
        public static uint MaximumSize { get; set; } = 30 * 1024;

        /// <summary>
        /// Imports a slab from a slab string.
        /// </summary>
        /// <param name="maybeSlabString">The slab string.</param>
        /// <returns>The imported slab.</returns>
        public static ILayoutSlab Import(string maybeSlabString)
        {
            // Get rid of possible whitespace characters at the end and start
            maybeSlabString = maybeSlabString.Trim();

            // Strip markdown characters if necessary
            if (maybeSlabString.StartsWith(MarkdownCodeBrace))
                maybeSlabString = maybeSlabString.Substring(3, maybeSlabString.Length - MarkdownCodeBrace.Length * 2);

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

        /// <summary>
        /// Exports a slab string from a slab.
        /// </summary>
        /// <param name="slab">The slab to export.</param>
        /// <param name="markdown">If <c>true</c>, the slab string is enclosed in Markdown code braces '```'.</param>
        /// <returns>Returns the string.</returns>
        public static string Export(this ILayoutSlab slab, bool markdown = false)
        {
            using var output = new MemoryStream();
            using var gzip = new GZipStream(output, CompressionLevel.Optimal);
            using var w = new BinaryWriter(gzip);

            // Common header
            w.Write(MagicHex);
            w.Write(slab.Version);

            // Write the slab
            slab.Write(w);

            // Close the streams
            w.Close();

            // Return the result
            // return the result, optionally for markdown
            string result = Convert.ToBase64String(output.ToArray(), Base64FormattingOptions.None);
            return markdown ? $"{MarkdownCodeBrace}{result}{MarkdownCodeBrace}" : result;
        }
    }
}
