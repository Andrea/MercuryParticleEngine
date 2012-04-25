namespace ProjectMercury
{
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [StructLayout(LayoutKind.Sequential)]
    public struct Particle
    {
        static Particle()
        {
            // Position vertex element...
            VertexElement positionElement       = new VertexElement();
            positionElement.VertexElementFormat = VertexElementFormat.Vector2;
            positionElement.VertexElementUsage  = VertexElementUsage.Position;

            // Scale vertex element...
            VertexElement scaleElement          = new VertexElement();
            scaleElement.Offset                 = 8;
            scaleElement.VertexElementFormat    = VertexElementFormat.Single;
            scaleElement.VertexElementUsage     = VertexElementUsage.PointSize;

            // Rotation vertex element...
            VertexElement rotationElement       = new VertexElement();
            rotationElement.Offset              = 12;
            rotationElement.VertexElementFormat = VertexElementFormat.Single;
            rotationElement.VertexElementUsage  = VertexElementUsage.TextureCoordinate;

            // Color vertex element...
            VertexElement colourElement         = new VertexElement();
            colourElement.Offset                = 16;
            colourElement.VertexElementFormat   = VertexElementFormat.Vector4;
            colourElement.VertexElementUsage    = VertexElementUsage.Color;

            // Vertex element array...
            Particle.VertexElements = new VertexElement[]
            {
                positionElement,
                scaleElement,
                rotationElement,
                colourElement
            };
        }

        /// <summary>
        /// Contains the vertex element data for a Particle.
        /// </summary>
        static public readonly VertexElement[] VertexElements;

        /// <summary>
        /// Gets the size of a Particle structure in bytes.
        /// </summary>
        static public int SizeInBytes { get { return 56; } }

        // Members used by the shader to draw the particles...
        public Vector2 Position;
        public float Scale;
        public float Rotation;
        public Vector4 Colour;

        // Members needed only for the simulation...
        public Vector2 Momentum;
        public Vector2 Velocity;
        public float Inception;
        public float Age;

        /// <summary>
        /// Applies a force to the particle.
        /// </summary>
        /// <param name="force">A vector describing the force and direction.</param>
        public void ApplyForce(ref Vector2 force)
        {
            Vector2.Add(ref this.Velocity, ref force, out this.Velocity);
        }

        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since the last update.</param>
        public void Update(float elapsedSeconds)
        {
            // Add velocity to momentum...
            Vector2.Add(ref this.Velocity, ref this.Momentum, out this.Momentum);

            // Set velocity back to zero...
            this.Velocity = Vector2.Zero;

            Vector2 deltaMomentum;

            // Calculate momentum for this time-step...
            Vector2.Multiply(ref this.Momentum, elapsedSeconds, out deltaMomentum);

            // Add momentum to the particles position...
            Vector2.Add(ref this.Position, ref deltaMomentum, out this.Position);
        }
    }
}