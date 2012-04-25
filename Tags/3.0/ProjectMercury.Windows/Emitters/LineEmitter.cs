namespace ProjectMercury.Emitters
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an Emitter which released Particles at a random point along a line.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Emitters.LineEmitterTypeConverter, ProjectMercury.Design")]
#endif
    public class LineEmitter : Emitter
    {
        private Matrix RotationMatrix = Matrix.CreateRotationZ(0f);
#if DEBUG
        private int _length;

        /// <summary>
        /// Gets or sets the length of the line.
        /// </summary>
        public int Length
        {
            get { return this._length; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException();

                this._length = value;
            }
        }
#else
        public int Length;
#endif
        /// <summary>
        /// Gets or sets the rotation of the line around its middle point.
        /// </summary>
        public float Angle
        {
            get { return (float)Math.Atan2(this.RotationMatrix.M12, this.RotationMatrix.M11); }
            set { this.RotationMatrix = Matrix.CreateRotationZ(value); }
        }

        public bool Rectilinear;

        public bool EmitBothWays;

        /// <summary>
        /// Generates an offset vector for a Particle as it is released.
        /// </summary>
        protected override void GenerateParticleOffset(float totalSeconds, ref Vector2 triggerPosition, out Vector2 offset)
        {
            float halfLength = this.Length * 0.5f;

            offset = new Vector2
            {
                X = RandomHelper.NextFloat(-halfLength, halfLength),
                Y = 0f
            };

            Vector2.Transform(ref offset, ref this.RotationMatrix, out offset);
        }

        private bool flip;

        /// <summary>
        /// Generates a normalised force vector for a Particle as it is released.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="force"></param>
        protected override void GenerateParticleForce(ref Vector2 offset, out Vector2 force)
        {
            if (this.Rectilinear)
            {
                force = new Vector2
                {
                    X = this.RotationMatrix.Up.X,
                    Y = this.RotationMatrix.Up.Y

                };

                if (EmitBothWays)
                {
                    if (flip)
                    {
                        force.X *= -1;
                        force.Y *= -1;
                    }

                    flip = !flip;
                }
            }
            else
            {
                base.GenerateParticleForce(ref offset, out force);
            }
        }
    }
}