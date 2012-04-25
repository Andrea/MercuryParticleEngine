using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Emitters
{
    /// <summary>
    /// Represents a particle emitter that releases particles in a circle shape.
    /// </summary>
    [Serializable]
    public class CircleEmitter<T> : Emitter<T> where T : IParticle, new()
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="budget">The number of particles that will be available to the emitter.</param>
        /// <param name="term">The lifetime of particles in whole and fractional second.</param>
        /// <param name="radius">The radius of the circle emitter.</param>
        /// <param name="ring">True if particles will only be released from the edge of the circle.</param>
        public CircleEmitter(int budget, float term, float radius, bool ring)
            : base(budget, term)
        {
            if (radius <= 0f) { throw new ArgumentOutOfRangeException("radius"); }

            this._radius = radius;
            this._ring = ring;
        }

        private float _radius;      //The radius of the circle.
        private bool _ring;         //True to only release particles on the edge.

        /// <summary>
        /// Generates offset and orientation vectors for a released particle.
        /// </summary>
        /// <param name="offset">The offset of the particle from the trigger position.</param>
        /// <param name="orientation">The orientation of the particle as a unit vector.</param>
        protected override void GenerateParticleOffsetAndOrientation(out Vector2 offset, out Vector2 orientation)
        {
            float rads = (float)Random.NextDouble() * MathHelper.TwoPi;

            float distance = this._radius;

            if (!this._ring) { distance *= (float)Random.NextDouble(); }

            orientation = new Vector2((float)Math.Sin(rads), (float)Math.Cos(rads));

            Vector2.Multiply(ref orientation, distance, out offset);
        }
    }
}
