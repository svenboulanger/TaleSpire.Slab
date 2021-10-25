using System;

namespace TaleSpire.Slab
{
    /// <summary>
    /// A 3-dimensional vector of integers.
    /// </summary>
    public readonly struct Int3 : IEquatable<Int3>
    {
        /// <summary>
        /// Gets the X-coordinate.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y-coordinate.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets the Z-coordinate.
        /// </summary>
        public int Z { get; }

        /// <summary>
        /// Creates a new vector of integers.
        /// </summary>
        /// <param name="x">The X-coordinate.</param>
        /// <param name="y">The Y-coordinate.</param>
        /// <param name="z">The Z-coordinate.</param>
        public Int3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets a hash code for the vector.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int hash = X;
            hash = (hash * 1021) ^ Y;
            hash = (hash * 1021) ^ Z;
            return hash;
        }


        /// <summary>
        /// Determines whether the vector is equal to an object.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>Returns <c>true</c> if the vectors are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int3 other)
                return Equals(other);
            return false;
        }

        /// <summary>
        /// Determines whether the vector is equal to another vector.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>Returns <c>true</c> if the vectors are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Int3 other)
        {
            if (!X.Equals(other.X))
                return false;
            if (!Y.Equals(other.Y))
                return false;
            if (!Z.Equals(other.Z))
                return false;
            return true;
        }

        /// <summary>
        /// Converts the vector to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "({0}, {1}, {2})", X, Y, Z);
        }

        /// <summary>
        /// Equality operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static bool operator ==(Int3 left, Int3 right)
            => left.Equals(right);

        /// <summary>
        /// Inequality operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static bool operator !=(Int3 left, Int3 right)
            => !left.Equals(right);

        /// <summary>
        /// Bitwise and operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static Int3 operator &(Int3 left, Int3 right)
            => new Int3(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

        /// <summary>
        /// Bitwise or operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static Int3 operator |(Int3 left, Int3 right)
            => new Int3(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

        /// <summary>
        /// Bitwise xor operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static Int3 operator ^(Int3 left, Int3 right)
            => new Int3(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

        /// <summary>
        /// Bitwise and operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static Int3 operator &(Int3 left, int right)
            => new Int3(left.X & right, left.Y & right, left.Z & right);

        /// <summary>
        /// Bitwise or operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static Int3 operator |(Int3 left, int right)
            => new Int3(left.X | right, left.Y | right, left.Z | right);

        /// <summary>
        /// Bitwise xor operator for a vector of integers.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>Returns a 3-dimensional result.</returns>
        public static Int3 operator ^(Int3 left, int right)
            => new Int3(left.X ^ right, left.Y ^ right, left.Z ^ right);


        /// <summary>
        /// Explicit conversion to a vector of floats.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static explicit operator Float3(Int3 vector)
            => new Float3(vector.X, vector.Y, vector.Z);
    }
}
