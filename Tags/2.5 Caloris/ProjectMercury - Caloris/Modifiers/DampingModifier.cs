using System;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents a modifier that applies a damping force to a particles momentum.
    /// </summary>
    [Serializable]
    public class DampingModifier : Modifier
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="coefficient">Damping coefficient.</param>
        public DampingModifier(float coefficient)
            : base()
        {
            this._coefficient = coefficient;
        }

        private float _coefficient;     //The damping coefficient.

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be processed.</param>
        /// <param name="age">The age of the particle in the range 0-1.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, ref Particle particle, float age)
        {
            Vector2 momentum = particle.Momentum;

            Vector2.Multiply(ref momentum, this._coefficient, out momentum);

            Vector2.Negate(ref momentum, out momentum);

            particle.ApplyForce(ref momentum);
        }
    }
}