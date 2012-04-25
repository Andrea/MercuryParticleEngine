namespace ProjectMercury.Emitters
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an Emitter which releases particles in a rectangle shape.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Emitters.RectEmitterTypeConverter, ProjectMercury.Design")]
#endif
    public class RectEmitter : Emitter
    {
#if DEBUG
        private int _width;

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the specified value is less than 1.</exception>
        public int Width
        {
            get { return this._width; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException();

                this._width = value;
            }
        }
#else
        public int Width;
#endif

#if DEBUG
        private int _height;

        /// <summary>
        /// Gets or sets the height of the rectangle.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the specified value is less than 1.</exception>
        public int Height
        {
            get { return this._height; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException();

                this._height = value;
            }
        }
#else
        public int Height;
#endif

        /// <summary>
        /// True if the Particles should be released only from the edge of the rectangle, else false.
        /// </summary>
        public bool Frame;

        /// <summary>
        /// Generates an offset vector for a Particle as it is released.
        /// </summary>
        protected override void GenerateParticleOffset(float totalSeconds, ref Vector2 triggerPosition, out Vector2 offset)
        {
            float halfWidth = this.Width * 0.5f;
            float halfHeight = this.Height * 0.5f;

            offset = new Vector2();

            if (this.Frame)
            {
                if (RandomHelper.NextBool())
                {
                    offset.X = RandomHelper.Choose(-halfWidth, halfWidth);
                    offset.Y = RandomHelper.NextFloat(-halfHeight, halfHeight);
                }
                else
                {
                    offset.X = RandomHelper.NextFloat(-halfWidth, halfWidth);
                    offset.Y = RandomHelper.Choose(-halfHeight, halfHeight);
                }
            }
            else
            {
                offset.X = RandomHelper.NextFloat(-halfWidth, halfWidth);
                offset.Y = RandomHelper.NextFloat(-halfHeight, halfHeight);
            }
        }
    }
}