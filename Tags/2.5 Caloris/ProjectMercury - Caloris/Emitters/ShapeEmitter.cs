using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Emitters
{
    /// <summary>
    /// Represents an emitter that releases particles from a random position on the edge of a shape.
    /// </summary>
    [Serializable]
    public class ShapeEmitter : Emitter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="budget">The number of particles that will be available to the emitter.</param>
        /// <param name="term">The life time of released particles in whole and fractional seconds.</param>
        /// <param name="points">A collection of points which make up the two dimensional shape.</param>
        public ShapeEmitter(int budget, float term, IEnumerable<Vector2> points)
            : base(budget, term)
        {
            this._points = new List<Vector2>(points);
        }

        private List<Vector2> _points;      //The points that define the shape.

        /// <summary>
        /// Gets the list of points that define the shape.
        /// </summary>
        public ICollection<Vector2> Points
        {
            get { return this._points as ICollection<Vector2>; }
        }

        /// <summary>
        /// Generates the offset and orientation for a particle when it is released.
        /// </summary>
        /// <param name="offset">The offset of the particle from the trigger position.</param>
        /// <param name="orientation">The orientation of the particle as a unit vector.</param>
        protected override void GenerateParticleOffsetAndOrientation(out Vector2 offset, out Vector2 orientation)
        {
            if (this._points.Count < 2)
                throw new InvalidOperationException("Need at least two control points in the shape!");

            int rndPoint = Random.Next(0, this._points.Count);

            Vector2 a = this._points[rndPoint];
            Vector2 b = this._points[(rndPoint + 1) % this._points.Count];

            float rndPos = (float)Random.NextDouble();

            offset = new Vector2(MathHelper.Lerp(a.X, b.X, rndPos), MathHelper.Lerp(a.Y, b.Y, rndPos));

            float rads = (float)Random.NextDouble() * MathHelper.TwoPi;

            orientation = new Vector2((float)Math.Sin(rads), (float)Math.Cos(rads));
        }
    }
}
