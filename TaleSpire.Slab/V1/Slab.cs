using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace TaleSpire.Slab.V1
{
    /// <summary>
    /// A slab using version 1.
    /// </summary>
    public class Slab : ILayoutSlab, IEquatable<Slab>
    {
        private readonly Layout[] _layouts;
        private const uint _magicHex = 0xD1CEFACE;
        private const ushort _currentVersion = 1;

        /// <summary>
        /// Gets a list of layouts.
        /// </summary>
        public IReadOnlyList<ILayout> Layouts => new List<ILayout>(_layouts.Cast<ILayout>());

        /// <summary>
        /// Creates a new slab.
        /// </summary>
        /// <param name="count">The number of layouts.</param>
        protected Slab(int count)
        {
            _layouts = new Layout[count];
        }

        /// <summary>
        /// Creates a new slab.
        /// </summary>
        /// <param name="layouts">The layouts.</param>
        public Slab(IEnumerable<Layout> layouts)
        {
            _layouts = layouts.ToArray();
        }

        /// <summary>
        /// Determines whether the slab is equal to another slab.
        /// </summary>
        /// <param name="other">The other slab.</param>
        /// <returns>Returns <c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(ISlab other)
        {
            if (other is not Slab slab)
                return false;
            return Equals(slab);
        }

        /// <summary>
        /// Determines whether the slab is equal to another slab.
        /// </summary>
        /// <param name="other">The other slab.</param>
        /// <returns>Returns <c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Slab other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;
            if (_layouts.Length != other._layouts.Length)
                return false;
            for (int i = 0; i < _layouts.Length; i++)
            {
                if (!_layouts[i].Equals(other._layouts[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Imports a slab from a binary reader.
        /// </summary>
        /// <param name="r">The binary reader.</param>
        /// <returns>The imported slab.</returns>
        public static Slab Import(BinaryReader r)
        {
            // Get the number of layouts
            ushort layoutsCount = r.ReadUInt16();

            // First all layout headers are read
            var slab = new Slab(layoutsCount);
            for (int i = 0; i < layoutsCount; i++)
                slab._layouts[i] = new Layout(new LayoutData(r));

            // Read each layout asset data
            for (int i = 0; i < layoutsCount; i++)
            {
                var layout = slab._layouts[i];
                for (int j = 0; j < layout.Data.AssetCount; j++)
                    layout.Assets[j] = new AssetData(r);
            }

            // Technically, we still have data for the overall center and size
            // but this data is already available in the other data
            _ = r.ReadSingle(); // center x
            _ = r.ReadSingle(); // center y
            _ = r.ReadSingle(); // center z
            _ = r.ReadSingle(); // size x
            _ = r.ReadSingle(); // size y
            _ = r.ReadSingle(); // size z

            return slab;
        }

        /// <inheritdoc/>
        public string Export(bool markdown = false)
        {
            using var output = new MemoryStream();
            using var gzip = new GZipStream(output, CompressionLevel.Optimal);
            using var w = new BinaryWriter(gzip);

            // Write the magic identifier and the current version
            w.Write(_magicHex);
            w.Write(_currentVersion);

            // Write the layout information
            w.Write((ushort)_layouts.Length);

            // Write the layout headers
            for (int i = 0; i < _layouts.Length; i++)
                _layouts[i].Data.Write(w);

            // Write the layouts
            Float3 minBound = new(float.MaxValue, float.MaxValue, float.MaxValue);
            Float3 maxBound = new(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < _layouts.Length; i++)
            {
                var layout = _layouts[i];
                for (int j = 0; j < layout.Assets.Length; j++)
                {
                    layout.Assets[j].Write(w);
                    minBound = Float3.Min(minBound, layout.Assets[j].Position - layout.Assets[j].Size);
                    maxBound = Float3.Max(maxBound, layout.Assets[j].Position + layout.Assets[j].Size);
                }
            }

            // Write the combined results
            var center = (minBound + maxBound) * 0.5f;
            w.Write(center.X);
            w.Write(center.Y);
            w.Write(center.Z);
            var size = (minBound + maxBound) * 0.5f;
            w.Write(size.X);
            w.Write(size.Y);
            w.Write(size.Z);

            // Close the streams, which is necessary for GZip to complete the conversion
            w.Close();

            // Return the result, optionally for markdown
            string result = Convert.ToBase64String(output.ToArray(), Base64FormattingOptions.None);
            return markdown ? $"```{result}```" : result;
        }
    }
}
