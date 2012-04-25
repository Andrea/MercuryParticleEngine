using System;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents a radial gravity source.
    /// </summary>
    [Serializable]
    public class RadialGravityModifier : Modifier
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="position">The position of the gravity source in screen space.</param>
        /// <param name="range">The range of the gravity source in pixels.</param>
        /// <param name="strength">The strength multiplier of the gravity source.</param>
        public RadialGravityModifier(Vector2 position, float range, float strength)
        {
            if (range <= 0f) { throw new ArgumentOutOfRangeException("range"); }

            this.Position = position;
            this.Range = range;
            this._strength = strength;
        }

        private Vector2 _position;  //The position of the gravity source.
        private float _range;       //The range of the gravity source.
        private float _rangeSq;     //The range of the gravity source squared.
        private float _strength;    //The strength multiplier of the gravity source.

        /// <summary>
        /// Gets or sets the position of the gravity source in screen space.
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        /// <summary>
        /// Gets or sets the range of the gravity source in pixels.
        /// </summary>
        public float Range
        {
            get { return this._range; }
            set
            {
                this._range = value;
                this._rangeSq = value * value;
            }
        }

        /// <summary>
        /// Gets or sets the strength multiplier of the gravity source.
        /// </summary>
        public float Strength
        {
            get { return this._strength; }
            set { this._strength = value; }
        }

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be processed.</param>
        /// <param name="age">The age of the particle in the range 0-1.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, ref Particle particle, float age)
        {
            float distance;

            Vector2.DistanceSquared(ref this._position, ref particle.Position, out distance);

            if (distance < this._rangeSq)
            {
                float effect = 1f - (distance / this._rangeSq);

                Vector2 force;

                Vector2.Subtract(ref this._position, ref particle.Position, out force);

                Vector2.Normalize(ref force, out force);

                Vector2.Multiply(ref force, effect * elapsedTime * this._strength, out force);

                particle.ApplyForce(ref force);
            }
        }
    }
}
