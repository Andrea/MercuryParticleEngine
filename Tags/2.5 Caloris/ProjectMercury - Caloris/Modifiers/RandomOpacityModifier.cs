using System;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Applies random opacity to particles upon release.
    /// </summary>
    [Serializable]
    public class RandomOpacityModifier : Modifier
    {
        /// <summary>
        /// Default constructor. Applies ranom opacity to particles.
        /// </summary>
        public RandomOpacityModifier() : this(.5f, 1f) { }

        /// <summary>
        /// Constructor specifying a weight opacity and variation.
        /// </summary>
        /// <param name="weight">The weight opacity.</param>
        /// <param name="range">The range around the weight opacity.</param>
        public RandomOpacityModifier(float weight, float range)
        {
            this._weight = weight;
            this._variation = range;
        }

        private float _weight;      //Weight opacity.
        private float _variation;   //Range around weight.

        /// <summary>
        /// Processes a particle as it is released.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="particle">The particles which has been released.</param>
        public override void ProcessReleasedParticle(float totalTime, ref Particle particle)
        {
            float op = this._weight + (((float)Random.NextDouble() * this._variation) - (this._variation / 2f));

            particle.Opacity = MathHelper.Clamp(op, 0f, 1f);
        }
    }
}
