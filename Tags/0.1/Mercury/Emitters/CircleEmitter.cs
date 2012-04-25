using Microsoft.Xna.Framework;

using System;
using System.ComponentModel;

namespace Mercury
{
    public sealed class CircleEmitter : Emitter
    {
        #region Radius
        /// <summary>
        /// The radius of the circle
        /// </summary>
        private float _radius = 50f;

        /// <summary>
        /// The radius of the circle emitter
        /// </summary>
        [Category("Circle")]
        [Description("The radius of the circle emitter")]
        [DefaultValue(50f)]
        public float Radius
        {
            get { return _radius; }
            set { _radius = MathHelper.Clamp(value, 0f, 1000f); }
        }
        #endregion

        #region CircleEmitter()
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CircleEmitter() : base() { }
        #endregion
        #region Emit()
        /// <summary>
        /// Emits a single particle
        /// </summary>
        /// <param name="init">Particle init parameters to adjust</param>
        internal override void Emit(ref ParticleInitParams init)
        {
            float angle = (float)_rnd.NextDouble() * MathHelper.TwoPi;
            float distance = (float)_rnd.NextDouble() * _radius;

            init.Angle = angle;
            init.Position = new Vector2(
                Position.X + (float)Math.Sin(angle) * distance,
                Position.Y + (float)Math.Cos(angle) * distance);
            init.Rotation = angle;
        }
        #endregion
    }
}