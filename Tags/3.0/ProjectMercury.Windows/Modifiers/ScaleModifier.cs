namespace ProjectMercury.Modifiers
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a Modifier which adjusts the scale of a Particle over its lifetime.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.ScaleModifierTypeConverter, ProjectMercury.Design")]
#endif
    public class ScaleModifier : Modifier
    {
        /// <summary>
        /// The initial scale of the Particle in pixels.
        /// </summary>
        public float InitialScale;

        /// <summary>
        /// The ultimate scale of the Particle in pixels.
        /// </summary>
        public float UltimateScale;

        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        /// <param name="totalSeconds"></param>
        /// <param name="elapsedSeconds"></param>
        /// <param name="particle"></param>
        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            particle.Scale = MathHelper.Lerp(this.InitialScale, this.UltimateScale, particle.Age);
        }
    }
}