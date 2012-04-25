namespace ProjectMercury.Modifiers
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a Modifier which gradually changes the colour of a Particle over the course of its lifetime.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.ColorModifierTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class ColorModifier : Modifier
    {
        /// <summary>
        /// The initial colour of Particles when they are released.
        /// </summary>
        public Vector3 InitialColour;

        /// <summary>
        /// The ultimate colour of Particles when they are retired.
        /// </summary>
        public Vector3 UltimateColour;

        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            particle.Colour.X = MathHelper.Lerp(this.InitialColour.X, this.UltimateColour.X, particle.Age);
            particle.Colour.Y = MathHelper.Lerp(this.InitialColour.Y, this.UltimateColour.Y, particle.Age);
            particle.Colour.Z = MathHelper.Lerp(this.InitialColour.Z, this.UltimateColour.Z, particle.Age);
        }
    }
}
