using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TaleSpire.Slab
{
    /// <summary>
    /// An identifier for layouts.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public readonly struct NGuid : IEquatable<NGuid>
    {
        /// <summary>
        /// An empty NGuid.
        /// </summary>
        public static readonly NGuid Empty = new();

        [FieldOffset(0)]
        private readonly int _a;
        [FieldOffset(4)]
        private readonly short _b;
        [FieldOffset(6)]
        private readonly short _c;
        [FieldOffset(8)]
        private readonly byte _d;
        [FieldOffset(9)]
        private readonly byte _e;
        [FieldOffset(10)]
        private readonly byte _f;
        [FieldOffset(11)]
        private readonly byte _g;
        [FieldOffset(12)]
        private readonly byte _h;
        [FieldOffset(13)]
        private readonly byte _i;
        [FieldOffset(14)]
        private readonly byte _j;
        [FieldOffset(15)]
        private readonly byte _k;

        // Long version
        [FieldOffset(0)]
        private readonly uint _data0;
        [FieldOffset(4)]
        private readonly uint _data1;
        [FieldOffset(8)]
        private readonly uint _data2;
        [FieldOffset(12)]
        private readonly uint _data3;

        /// <summary>
        /// Creates a new ID.
        /// </summary>
        public NGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
            : this()
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
            _e = e;
            _f = f;
            _g = g;
            _h = h;
            _i = i;
            _j = j;
            _k = k;
        }

        /// <summary>
        /// Creates a new ID.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public NGuid(BinaryReader reader)
            : this()
        {
            _data0 = reader.ReadUInt32();
            _data1 = reader.ReadUInt32();
            _data2 = reader.ReadUInt32();
            _data3 = reader.ReadUInt32();
        }

        public override int GetHashCode()
        {
            uint hash = _data0;
            hash = (hash * 1021) ^ _data1;
            hash = (hash * 1021) ^ _data2;
            hash = (hash * 1021) ^ _data3;
            return (int)hash;
        }

        /// <summary>
        /// Determines whether the ID is equal to an object.
        /// </summary>
        /// <param name="other">The other ID.</param>
        /// <returns>Returns <c>true</c> if the ID's are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is NGuid other)
                return Equals(other);
            return false;
        }

        /// <summary>
        /// Determines whether the ID is equal to another one.
        /// </summary>
        /// <param name="other">The other ID.</param>
        /// <returns>Returns <c>true</c> if the ID's are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(NGuid other)
        {
            if (_data0 != other._data0)
                return false;
            if (_data1 != other._data1)
                return false;
            if (_data2 != other._data2)
                return false;
            if (_data3 != other._data3)
                return false;
            return true;
        }

        /// <summary>
        /// Writes the ID.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(_data0);
            writer.Write(_data1);
            writer.Write(_data2);
            writer.Write(_data3);
        }

        /// <summary>
        /// Converts the ID to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0:x8}-{1:x4}-{2:x4}={3:x4}-{3:x4}{4:x8}",
                _a, _b, _c, (_data2 >> 16) & 0x0ffff, _data2 & 0x0ffff, _data3);
        }
    }
}
