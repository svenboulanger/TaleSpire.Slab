using System;

namespace TaleSpire.Slab
{
    /// <summary>
    /// A 3-dimensional vector of floats.
    /// </summary>
    public readonly struct Float3 : IEquatable<Float3>
    {
        /// <summary>
        /// Gets the X-coordinate.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// Gets the Y-coordinate.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Gets the Z-coordinate.
        /// </summary>
        public float Z { get; }

        /// <summary>
        /// Creates a new 3-dimensional vector of floats.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="x">The z-coordinate.</param>
        public Float3(float x, float y, float z)
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
            int hash = X.GetHashCode();
            hash = (hash * 1021) ^ Y.GetHashCode();
            hash = (hash * 1021) ^ Z.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Determines whether the vector is equal to an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Returns <c>true</c> if they are equal; otherwise, <c>false</c></returns>
        public override bool Equals(object obj)
        {
            if (obj is Float3 other)
                return Equals(other);
            return false;
        }

        /// <summary>
        /// Determines whether the vector is equal to another vector.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>Returns <c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Float3 other)
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
        /// Rounds the vector to integer values.
        /// </summary>
        /// <returns>The rounded result.</returns>
        public Float3 Round()
        {
            return new Float3(
                (float)Math.Floor(X),
                (float)Math.Floor(Y),
                (float)Math.Floor(Z));
        }

        /// <summary>
        /// Converts the vector to a string.
        /// </summary>
        /// <returns>Returns the vector in string format.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "({0:G3}, {1:G3}, {2:G3})", X, Y, Z);
        }

        /// <summary>
        /// Takes the maximum of each coordinate.
        /// </summary>
        /// <param name="a">The first argument.</param>
        /// <param name="b">The second argument.</param>
        /// <returns>The result.</returns>
        public static Float3 Min(Float3 a, Float3 b)
            => new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));

        /// <summary>
        /// Takes the maximum of each coordinate.
        /// </summary>
        /// <param name="a">The first argument.</param>
        /// <param name="b">The second argument.</param>
        /// <returns>The result.</returns>
        public static Float3 Max(Float3 a, Float3 b)
            => new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>The result.</returns>
        public static Float3 operator +(Float3 left, Float3 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>The result.</returns>
        public static Float3 operator -(Float3 left, Float3 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        /// <summary>
        /// Multiplies the vector with a scalar value.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>The scalar multiplied result.</returns>
        public static Float3 operator *(Float3 left, float right)
            => new(left.X * right, left.Y * right, left.Z * right);

        /// <summary>
        /// Multiplies the vector with a scalar value.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>The scalar multiplied result.</returns>
        public static Float3 operator *(float left, Float3 right)
            => new(left * right.X, left * right.Y, left * right.Z);

        /// <summary>
        /// Divides the vector by a scalar value.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        /// <returns>The result of the division.</returns>
        public static Float3 operator /(Float3 left, float right)
            => new(left.X / right, left.Y / right, left.Z / right);

        /// <summary>
        /// Tests equality between two vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Returns <c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Float3 left, Float3 right)
            => left.Equals(right);

        /// <summary>
        /// Tests inequality between two vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Returns <c>true</c> if they are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Float3 left, Float3 right)
            => !left.Equals(right);

        /// <summary>
        /// Explicitly converts the vector to a vector of integers.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static explicit operator Int3(Float3 vector)
            => new((int)vector.X, (int)vector.Y, (int)vector.Z);
    }
}
