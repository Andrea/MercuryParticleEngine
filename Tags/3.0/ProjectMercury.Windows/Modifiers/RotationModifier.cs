namespace ProjectMercury.Modifiers
{
    using System.ComponentModel;

    /// <summary>
    /// Defines a Modifier which alters the rotation of a Particle over its lifetime.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.RotationModifierTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class RotationModifier : Modifier
    {
        /// <summary>
        /// The rate of rotation in radians per second.
        /// </summary>
        public float RotationRate;

        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            particle.Rotation += this.RotationRate * elapsedSeconds;
        }
    }
}