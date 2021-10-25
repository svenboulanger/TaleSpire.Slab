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
        private readonly ulong _data0;
        [FieldOffset(8)]
        private readonly ulong _data1;

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
        /// Creates an NGuid from a regular guid.
        /// </summary>
        /// <param name="guid">The guid.</param>
        public NGuid(Guid guid)
            : this()
        {
            var bytes = guid.ToByteArray();
            _a = (bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0];
            _b = (short)((bytes[5] << 8) | bytes[4]);
            _c = (short)((bytes[7] << 8) | bytes[6]);
            _d = bytes[8];
            _e = bytes[9];
            _f = bytes[10];
            _g = bytes[11];
            _h = bytes[12];
            _i = bytes[13];
            _j = bytes[14];
            _k = bytes[15];
        }

        /// <summary>
        /// Creates a new ID.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public NGuid(BinaryReader reader)
            : this()
        {
            _data0 = reader.ReadUInt64();
            _data1 = reader.ReadUInt64();
        }

        /// <summary>
        /// Creates a hash code for the GUID.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int hash = _data0.GetHashCode();
            hash = (hash * 1021) ^ _data1.GetHashCode();
            return hash;
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
        }

        /// <summary>
        /// Converts the ID to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0:x8}-{1:x4}-{2:x4}-{3:x4}-{3:x12}",
                _a, _b, _c, (_data1 >> 48) & 0x0ffff, _data1 & 0x0ffffffffffff);
        }
    }
}
