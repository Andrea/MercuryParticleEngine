namespace ProjectMercury.Modifiers
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a Modifier that applies a constant force vector to Particles over their lifetime.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.LinearGravityModifierTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class LinearGravityModifier : Modifier
    {
        /// <summary>
        /// Gets or sets the gravity vector.
        /// </summary>
        public Vector2 Gravity;

        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            Vector2 deltaGrav;

            Vector2.Multiply(ref this.Gravity, elapsedSeconds, out deltaGrav);

            particle.ApplyForce(ref deltaGrav);
        }
    }
}