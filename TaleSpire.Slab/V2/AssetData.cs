using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TaleSpire.Slab.V2
{
    /// <summary>
    /// Asset instantiation data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct AssetData : IAsset, IAssetExtra<ExtraBits>
    {
        private const int _bitsPerCoordinate = 18;
        private const int _bitsForRotation = 5;
        private const int _bitsExtra = 5;

        private const int _coordinateMask = (1 << _bitsPerCoordinate) - 1;
        private const int _yBitsOffset = _bitsPerCoordinate;
        private const int _zBitsOffset = _yBitsOffset + _bitsPerCoordinate;

        private const int _rotationMask = (1 << _bitsForRotation) - 1;
        private const int _rotationBitsOffset = _zBitsOffset + _bitsPerCoordinate;
        private const float _rotationStep = 360.0f / 24.0f;

        private const int _extraBitsMask = (1 << _bitsExtra) - 1;
        private const int _extraBitsOffset = _rotationBitsOffset + _bitsForRotation;

        // Everything fits in these 64 bits!
        private readonly ulong _encoded;

        /// <inheritdoc/>
        public Float3 Position
        {
            get
            {
                return new Float3(
                    (int)(_encoded & _coordinateMask),
                    (int)((_encoded >> _yBitsOffset) & _coordinateMask),
                    (int)((_encoded >> _zBitsOffset) & _coordinateMask)) / 100.0f;
            }
        }

        /// <inheritdoc/>
        public float Rotation => ((_encoded >> _rotationBitsOffset) & _rotationMask) * _rotationStep;

        /// <inheritdoc/>
        public ExtraBits Extra => new((byte)(_encoded >> _extraBitsOffset));

        /// <summary>
        /// Creates a new asset data structure.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public AssetData(BinaryReader reader) => _encoded = reader.ReadUInt64();

        /// <summary>
        /// Creates a new assert data structure.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation in degrees.</param>
        /// <param name="extra">Extra five bits that can be used.</param>
        public AssetData(Float3 position, float rotation, ExtraBits extra)
        {
            var scaledPosition = (Int3)(position * 100).Round();

            if (scaledPosition.X < 0 || scaledPosition.Y < 0 || scaledPosition.Z < 0)
                throw new ArgumentOutOfRangeException(nameof(scaledPosition), "Scaled origin was negative");
            if (scaledPosition.X > _coordinateMask || scaledPosition.Y > _coordinateMask || scaledPosition.Z > _coordinateMask)
                throw new ArgumentOutOfRangeException(nameof(scaledPosition), "Scaled origin out of bounds");
            // Note that these checks will also guarantee that the location is within 18 bits!

            var rotationMasked = (sbyte)Math.Round(rotation / _rotationStep, 0, MidpointRounding.AwayFromZero) & _rotationMask;
            _encoded = (ulong)scaledPosition.X;
            _encoded |= (ulong)scaledPosition.Y << _yBitsOffset;
            _encoded |= (ulong)scaledPosition.Z << _zBitsOffset;
            _encoded |= (ulong)rotationMasked << _rotationBitsOffset;
            _encoded |= (ulong)extra.Data << _extraBitsOffset;
        }

        /// <summary>
        /// Writes the asset data.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
            => writer.Write(_encoded);

        /// <summary>
        /// Converts the asset data to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "(pos {0}, rot {1}, bits {2:x2})",
                Position.ToString(), Rotation.ToString(), Extra);
        }
    }
}
