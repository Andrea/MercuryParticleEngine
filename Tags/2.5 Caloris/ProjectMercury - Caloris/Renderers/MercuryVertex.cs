using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMercury.Renderers
{
    /// <summary>
    /// Represents the vertex structure used by the basic particle renderer.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct MercuryVertex
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="position">The position of the vertex in screen coordinates.</param>
        /// <param name="color">The color of the vertex.</param>
        /// <param name="size">The scale of the point sprite in pixels..</param>
        /// <param name="rotation">The rotation of the point sprite in radians.</param>
        public MercuryVertex(Vector2 position, Vector4 color, float size, float rotation)
        {
            this.Position = position;
            this.Color = color;
            this.Size = size;
            this.Rotation = rotation;
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static MercuryVertex()
        {
            VertexElement posElement = new VertexElement();
            VertexElement colorElement = new VertexElement();
            VertexElement scaleElement = new VertexElement();
            VertexElement rotationElement = new VertexElement();

            VertexElements = new VertexElement[4];

            posElement.VertexElementFormat = VertexElementFormat.Vector2;
            posElement.VertexElementMethod = VertexElementMethod.Default;
            posElement.VertexElementUsage = VertexElementUsage.Position;

            colorElement.Offset = 8;
            colorElement.VertexElementFormat = VertexElementFormat.Vector4;
            colorElement.VertexElementMethod = VertexElementMethod.Default;
            colorElement.VertexElementUsage = VertexElementUsage.Color;

            scaleElement.Offset = 24;
            scaleElement.VertexElementFormat = VertexElementFormat.Single;
            scaleElement.VertexElementMethod = VertexElementMethod.Default;
            scaleElement.VertexElementUsage = VertexElementUsage.PointSize;

            rotationElement.Offset = 28;
            rotationElement.VertexElementFormat = VertexElementFormat.Single;
            rotationElement.VertexElementMethod = VertexElementMethod.Default;
            rotationElement.VertexElementUsage = VertexElementUsage.Color;
            rotationElement.UsageIndex = 1;

            VertexElements[0] = posElement;
            VertexElements[1] = colorElement;
            VertexElements[2] = scaleElement;
            VertexElements[3] = rotationElement;
        }

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public Vector2 Position;    //Position of the vertex.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public Vector4 Color;       //Color of the vertex.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Size;          //Scale of the point sprite.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Rotation;      //Rotation of the point sprite.

        /// <summary>
        /// Gets the vertex elements declaration.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        static public readonly VertexElement[] VertexElements;

        /// <summary>
        /// Presents the vertex definition in text form.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "{{Position:{0} Color:{1} Size:{2} Rotation:{3}}}", this.Position, this.Color, this.Size, this.Rotation);
        }

        /// <summary>
        /// Gets the size of the vertex definition in bytes.
        /// </summary>
        static public int SizeInBytes
        {
            get { return Marshal.SizeOf(typeof(MercuryVertex)); }
        }
    }
}