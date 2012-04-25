namespace ProjectMercury.Modifiers
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a collection of Modifiers.
    /// </summary>
    public class ModifierCollection : List<Modifier>
    {
        /// <summary>
        /// Causes all Modifiers in the collection to process the specified Particle.
        /// </summary>
        public void RunProcessors(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            for (int i = 0; i < base.Count; i++)
            {
                this[i].Process(totalSeconds, elapsedSeconds, ref particle, tag);
            }
        }
    }
}