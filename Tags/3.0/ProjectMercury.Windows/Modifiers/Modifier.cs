namespace ProjectMercury.Modifiers
{
    using System;

    /// <summary>
    /// Defines the abstract base class for a Modifier.
    /// </summary>
    public abstract class Modifier
    {
        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        public abstract void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag);
    }
}