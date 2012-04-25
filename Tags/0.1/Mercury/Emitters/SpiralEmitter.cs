using Microsoft.Xna.Framework;

using System;
using System.ComponentModel;

namespace Mercury
{
    /// <summary>
    /// Emits particles in a spiral pattern
    /// </summary>
    public sealed class SpiralEmitter : Emitter
    {
        #region Radius
        /// <summary>
        /// Radius of the spiral
        /// </summary>
        private float _radius = 50f;

        /// <summary>
        /// Gets or sets the radius of the spiral
        /// </summary>
        [Category("Spiral")]
        [Description("The radius of the spiral emitter")]
        [DefaultValue(50f)]
        public float Radius
        {
            get { return _radius; }
            set { _radius = MathHelper.Clamp(value, 0f, 1000f); }
        }
        #endregion
        #region Direction
        /// <summary>
        /// Direction to travel around the spiral
        /// </summary>
        private SpiralDirection _dir;

        /// <summary>
        /// Gets or sets the direction of the spiral
        /// </summary>
        [Category("Spiral")]
        [Description("The direction to travel around the spiral")]
        public SpiralDirection Direction
        {
            get { return _dir; }
            set { _dir = value; }
        }
        #endregion
        #region Segments
        /// <summary>
        /// Number of segments in the spiral
        /// </summary>
        private byte _segments = 32;

        /// <summary>
        /// Gets or sets the number of segments in the spiral
        /// </summary>
        [Category("Spiral")]
        [Description("The number of segments in the spiral")]
        [DefaultValue(32)]
        public byte Segments
        {
            get { return _segments; }
            set { _segments = value; }
        }
        #endregion
        #region CurrentSegment
        /// <summary>
        /// Current segment in the spiral
        /// </summary>
        private byte _currentSegment = 0;
        #endregion

        #region SpiralEmitter()
        /// <summary>
        /// Default constructor
        /// </summary>
        public SpiralEmitter() : base() { }
        #endregion
        #region Emit()
        /// <summary>
        /// Emits a single particle
        /// </summary>
        /// <param name="createParams">Particle init parameters to modify</param>
        internal override void Emit(ref ParticleInitParams init)
        {
            //Go to the next segment in the spiral
            if (_dir == SpiralDirection.AntiClockwise)
            {
                _currentSegment++;
                if (_currentSegment >= _segments) { _currentSegment = 0; }
            }
            else
            {
                _currentSegment--;
                if (_currentSegment <= 0) { _currentSegment = _segments; }
            }

            float angle = (MathHelper.TwoPi / _segments) * _currentSegment;

            init.Angle = angle;
            init.Position = new Vector2(
                Position.X + (float)Math.Sin(angle) * _radius,
                Position.Y + (float)Math.Cos(angle) * _radius);
            init.Rotation = angle;
        }
        #endregion
    }

    /// <summary>
    /// Spiral directions
    /// </summary>
    public enum SpiralDirection
    {
        Clockwise,
        AntiClockwise
    }
}