namespace ProjectMercury.Emitters
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an Emitter which releases Particles in a circle or ring shape.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Emitters.CircleEmitterTypeConverter, ProjectMercury.Design")]
#endif
    public class CircleEmitter : Emitter
    {
#if DEBUG
        private float _radius;

        /// <summary>
        /// Defines the radius of the circle.
        /// </summary>
        public float Radius
        {
            get { return this._radius; }
            set
            {
                if (value <= float.Epsilon)
                    throw new ArgumentOutOfRangeException();

                this._radius = value;
            }
        }
#else
        public float Radius;
#endif
        /// <summary>
        /// True if particles should be spawned only on the edge of the circle, else false.
        /// </summary>
        public bool Ring;

        /// <summary>
        /// True if particles should radiate away from the center of the circle, else false.
        /// </summary>
        public bool Radiate;

        /// <summary>
        /// Generates the offset vector for a Particle as it is released.
        /// </summary>
        protected override void GenerateParticleOffset(float totalSeconds, ref Vector2 triggerPosition, out Vector2 offset)
        {
            float rads = RandomHelper.NextFloat(MathHelper.TwoPi);

            offset = new Vector2((float)Math.Cos(rads) * this.Radius, (float)Math.Sin(rads) * this.Radius);

            if (!this.Ring)
                Vector2.Multiply(ref offset, RandomHelper.NextFloat(), out offset);
        }

        /// <summary>
        /// Generates a normalised force vector for a Particle as it is released.
        /// </summary>
        protected override void GenerateParticleForce(ref Vector2 offset, out Vector2 force)
        {
            if (this.Radiate)
                Vector2.Normalize(ref offset, out force);
            else
                base.GenerateParticleForce(ref offset, out force);
        }
    }
}