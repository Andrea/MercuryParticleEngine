using Microsoft.Xna.Framework;

namespace Mercury
{
    /// <summary>
    /// Emits particles that radiate from a single point
    /// </summary>
    public sealed class PointEmitter : Emitter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PointEmitter() : base() { }

        /// <summary>
        /// Emits a single particle
        /// </summary>
        /// <param name="init">Particle init parameters to modify</param>
        internal override void Emit(ref ParticleInitParams init)
        {
            init.Position.X = Position.X;
            init.Position.Y = Position.Y;

            float angle = (float)_rnd.NextDouble() * MathHelper.TwoPi;

            init.Angle = angle;
            init.Rotation = angle;
        }
    }
}