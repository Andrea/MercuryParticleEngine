namespace ProjectMercury.Emitters
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an Emitter which releases particles in a beam which gradually becomes wider.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Emitters.ConeEmitterTypeConverter, ProjectMercury.Design")]
#endif
    public class ConeEmitter : Emitter
    {
        /// <summary>
        /// The angle (in radians) that the SpotEmitters beam is facing.
        /// </summary>
        public float Direction;

        /// <summary>
        /// The angle (in radians) from edge to edge of the SpotEmitters beam.
        /// </summary>
        public float ConeAngle;

        /// <summary>
        /// Generates a normalised force vector for a Particle as it is released.
        /// </summary>
        protected override void GenerateParticleForce(ref Vector2 offset, out Vector2 force)
        {
            float radians = RandomHelper.NextFloat(this.Direction - (this.ConeAngle * 0.5f),
                                                   this.Direction + (this.ConeAngle * 0.5f));
            force = new Vector2
            {
                X = (float)Math.Cos(radians),
                Y = (float)Math.Sin(radians)
            };
        }
    }
}