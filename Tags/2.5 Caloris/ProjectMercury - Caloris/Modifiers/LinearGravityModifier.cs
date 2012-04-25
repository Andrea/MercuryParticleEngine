using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents gravity that will affect particles in a linear manner.
    /// </summary>
    [Serializable]
    public class LinearGravityModifier : Modifier
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="gravity">Gravity force.</param>
        /// <param name="strength">The strength of the gravity source in pixels
        /// per second.</param>
        public LinearGravityModifier(ref Vector2 gravity, float strength)
        {
            if (strength < 0f) { throw new ArgumentOutOfRangeException("strength"); }

            this._gravity = Vector2.Normalize(gravity);
            
            this._strength = strength;
        }

        private Vector2 _gravity;   //Gravity direction as a unit vector.
        private float _strength;    //Gravity strength in pixels per second.

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be updated.</param>
        /// <param name="age">The age of the particle in the range of zero to one inclusive.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, ref Particle particle, float age)
        {
            Vector2 force = this._gravity;

            Vector2.Multiply(ref force, this._strength * elapsedTime, out force);

            particle.ApplyForce(ref force);
        }
    }
}
