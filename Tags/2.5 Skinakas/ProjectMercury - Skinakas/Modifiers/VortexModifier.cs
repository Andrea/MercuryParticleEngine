using System;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents a modifier that applies a swirling force to particles within a given radius.
    /// </summary>
    [Serializable]
    public class VortexModifier : Modifier
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="position">The position of the vortex in screen coordinates..</param>
        /// <param name="radius">The radius of the vortex in pixels.</param>
        /// <param name="strength">The strength of the vortex in pixels per second.</param>
        /// <param name="clockwise">True if the vortex spins clockwise, else false.</param>
        public VortexModifier(Vector2 position, float radius, float vorticity, bool clockwise)
        {
            this.Position = position;
            this.Radius = radius;
            this.Vorticity = vorticity;
            this.Clockwise = clockwise;
        }

        private Vector2 _position;      //Position of the vortex.
        private float _radius;          //Radius of the vortex.
        private float _radiusSq;        //Radius squared.
        private float _vorticity;       //Vorticity strength.
        private bool _clockwise;        //True if the vortex is spinning clockwise.
        private Matrix _rotation;       //The rotation matrix applied to particle forces.

        /// <summary>
        /// Gets or sets the position of the vortex modifier in screen coordinates.
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        /// <summary>
        /// Gets or sets the radius of the vortex in pixels.
        /// </summary>
        public float Radius
        {
            get { return this._radius; }
            set
            {
                this._radius = value;
                this._radiusSq = value * value;
            }
        }

        /// <summary>
        /// Gets or sets the strength of the vortex in pixels per second.
        /// </summary>
        public float Vorticity
        {
            get { return this._vorticity; }
            set { this._vorticity = value; }
        }

        /// <summary>
        /// Gets or sets wether or not the vortex spins clockwise or anti-clockwise.
        /// </summary>
        public bool Clockwise
        {
            get { return this._clockwise; }
            set
            {
                this._clockwise = value;
                this._rotation = (value ? Matrix.CreateRotationZ(-MathHelper.PiOver2) : Matrix.CreateRotationZ(MathHelper.PiOver2));
            }
        }

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle which is ready to be processed.</param>
        /// <param name="age">The age of the particle in the range 0-1.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, IParticle particle, float age)
        {
            float distance;

            Vector2 particlePosition = particle.Position;

            Vector2.DistanceSquared(ref this._position, ref particlePosition, out distance);

            if (distance < this._radiusSq)
            {
                float effect = 1f - (distance / this._radiusSq);

                Vector2 force;

                Vector2.Subtract(ref this._position, ref particlePosition, out force);

                Vector2.Normalize(ref force, out force);

                Vector2.Transform(ref force, ref this._rotation, out force);

                Vector2.Multiply(ref force, effect * elapsedTime * this._vorticity, out force);

                particle.ApplyForce(ref force);
            }
        }
    }
}
