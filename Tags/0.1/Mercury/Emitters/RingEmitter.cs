using Microsoft.Xna.Framework;

using System;
using System.ComponentModel;

namespace Mercury
{
    public sealed class RingEmitter : Emitter
    {
        #region Radius
        /// <summary>
        /// The radius of the ring
        /// </summary>
        private float _radius = 50f;

        /// <summary>
        /// Gets or sets the radius of the ring
        /// </summary>
        [Category("Ring")]
        [Description("Radius of the ring emitter")]
        [DefaultValue(50f)]
        public float Radius
        {
            get { return _radius; }
            set { _radius = MathHelper.Clamp(value, 0f, 1000f); }
        }
        #endregion

        #region RingEmitter()
        /// <summary>
        /// Default constructor
        /// </summary>
        public RingEmitter() : base() { }
        #endregion
        #region Emit()
        /// <summary>
        /// Emits a single particle
        /// </summary>
        /// <param name="init">Particle init parameters to modify</param>
        internal override void Emit(ref ParticleInitParams init)
        {
            float angle = (float)_rnd.NextDouble() * MathHelper.TwoPi;

            init.Angle = angle;
            init.Position = new Vector2(
                Position.X + (float)Math.Sin(angle) * _radius,
                Position.Y + (float)Math.Cos(angle) * _radius);
            init.Rotation = angle;
        }
        #endregion
    }
}