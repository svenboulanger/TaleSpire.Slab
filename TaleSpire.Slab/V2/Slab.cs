using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaleSpire.Slab.V2
{
    /// <summary>
    /// A slab using version 2.
    /// </summary>
    public class Slab : ILayoutSlab, IEquatable<Slab>
    {
        private readonly Layout[] _layouts;
        
        /// <inheritdoc />
        public ushort Version => 2;

        /// <summary>
        /// Gets or sets the maximum decompressed size.
        /// </summary>
        public static uint MaximumSize { get; set; } = 30 * 1024;
        
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
            ushort creatureCount = r.ReadUInt16();
            if (creatureCount != 0)
                throw new ArgumentException("Creature count in v2 should always be 0");

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
            return slab;
        }

        /// <inheritdoc />
        public void Write(BinaryWriter w)
        {
            // Write the layout information
            w.Write((ushort)_layouts.Length);
            w.Write((ushort)0); // No creatures yet

            // Write the layout headers
            for (int i = 0; i < _layouts.Length; i++)
                _layouts[i].Data.Write(w);

            // Write the layouts
            for (int i = 0; i < _layouts.Length; i++)
                _layouts[i].Write(w);

            // This is undocumented, kind of thinking of a count of something else?
            w.Write((ushort)0);
        }
    }
}
