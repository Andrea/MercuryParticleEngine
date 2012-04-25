namespace ProjectMercury
{
    public interface ITagBinder
    {
        /// <summary>
        /// Generates a custom data tag for the specified Particle.
        /// </summary>
        object GetTag(ref Particle particle);

        /// <summary>
        /// Called when a custom data tag is no longer needed because the Particle has been retired.
        /// </summary>
        void DisposeTag(object tag);
    }
}