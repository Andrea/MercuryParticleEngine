namespace ProjectMercury.Modifiers
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a Modifier which gradually changes the opacity of a Particle over its lifetime.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.OpacityModifierTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class OpacityModifier : Modifier
    {
        /// <summary>
        /// Gets or sets the initial opacity of Particles as they are released.
        /// </summary>
        public float Initial;

        /// <summary>
        /// Gets or sets the ultimate opacity of Particles as they are retired.
        /// </summary>
        public float Ultimate;

        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            particle.Colour.W = MathHelper.Lerp(this.Initial, this.Ultimate, particle.Age);
        }
    }
}