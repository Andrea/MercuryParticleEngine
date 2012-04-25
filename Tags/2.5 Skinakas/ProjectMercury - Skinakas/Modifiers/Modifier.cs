using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents the abstract base class for all modifiers.
    /// </summary>
    [Serializable]
    abstract public class Modifier
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Modifier()
        {
            _random = new Random(Environment.TickCount);
        }

        /// <summary>
        /// Called after the particle effect has been deserialized.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        internal virtual void AfterImport(Game game) { }

        /// <summary>
        /// The random number generator that will be made available to all modifiers.
        /// </summary>
        static private Random _random;

        /// <summary>
        /// Gets the random number generator.
        /// </summary>
        static protected Random Random { get { return _random; } }

        /// <summary>
        /// Processes a particle when it is released.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle that has been released.</param>
        public virtual void ProcessReleasedParticle(float totalTime, IParticle particle) { }

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="age">The age of the particle in the range of zero to one inclusive.</param>
        public virtual void ProcessActiveParticle(float totalTime, float elapsedTime, IParticle particle, float age) { }

        /// <summary>
        /// Processes a particle when it expires.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="particle">That particle that has been retired.</param>
        public virtual void ProcessRetiredParticle(float totalTime, IParticle particle) { }
    }
}
