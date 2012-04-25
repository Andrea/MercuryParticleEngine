using Microsoft.Xna.Framework;

using System.ComponentModel;

namespace Mercury
{
    public sealed class SprayEmitter : Emitter
    {
        #region Direction
        private float _direction = 0f;

        /// <summary>
        /// Gets or sets the direction of the particle spray
        /// </summary>
        [DefaultValue(0f)]
        [Description("The direction of the particle spray (in degrees)")]
        [Category("Spray")]
        public float Direction
        {
            get { return MathHelper.ToDegrees(_direction); }
            set { _direction = MathHelper.ToRadians(value); }
        }
        #endregion
        #region Spread
        private float _spread = 0f;

        /// <summary>
        /// Gets or sets the spread of the particle spray
        /// </summary>
        [DefaultValue(0f)]
        [Description("The spread of the particle spray")]
        [Category("Spray")]
        public float Spread
        {
            get { return MathHelper.ToDegrees(_spread); }
            set { _spread = MathHelper.ToRadians(MathHelper.Clamp(value, 0f, 359f)); }
        }
        #endregion

        #region SprayEmitter()
        /// <summary>
        /// Default constructor
        /// </summary>
        public SprayEmitter() : base() { }
        #endregion
        #region Emit()
        /// <summary>
        /// Emits a single particle
        /// </summary>
        /// <param name="init">Particle creation parameters to adjust</param>
        internal override void Emit(ref ParticleInitParams init)
        {
            float angle = _direction + ((float)_rnd.NextDouble() * _spread) - (_spread / 2f);

            init.Angle = angle;
            init.Position = new Vector2(Position.X, Position.Y);
            init.Rotation = angle;
        }
        #endregion
    }
}