using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TaleSpire.Slab.V1
{
    /// <summary>
    /// Asset instantiation data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct AssetData : IAsset, IAssetSize
    {
        private readonly Float3 _position;
        private readonly Float3 _size;
        private readonly byte _rotation;

        private const float _stepRotation = 360.0f / 24.0f;

        /// <inheritdoc/>
        public Float3 Position => _position;

        /// <inheritdoc/>
        public Float3 Size => _size;

        /// <inheritdoc/>
        public float Rotation => _rotation * _stepRotation;

        /// <summary>
        /// Creates a new asset data structure.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public AssetData(BinaryReader reader)
        {
            _position = new(
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle());
            _size = new(
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle());
            _rotation = reader.ReadByte();

            // Extra bits due to memory alignment
            _ = reader.ReadByte();
            _ = reader.ReadUInt16();
        }

        /// <summary>
        /// Creates a new assert data structure.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size of the tile.</param>
        /// <param name="rotation">The rotation.</param>
        public AssetData(Float3 position, Float3 size, float rotation)
        {
            _position = position;
            _size = size;
            _rotation = (byte)Math.Round(rotation / _stepRotation, 0, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Writes the asset data.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(_position.X);
            writer.Write(_position.Y);
            writer.Write(_position.Z);
            writer.Write(_size.X);
            writer.Write(_size.Y);
            writer.Write(_size.Z);
            writer.Write(_rotation);

            // Alignment (TaleSpire is doing memory copying, and combining this with the memory alignment
            // we need 3 more unused bytes here for that)
            writer.Write((byte)0);
            writer.Write((short)0);
        }

        /// <summary>
        /// Converts the asset data to a string.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "(pos {0}, size {1}, rot {2})",
                Position.ToString(), Size.ToString(), Rotation.ToString());
        }
    }
}
