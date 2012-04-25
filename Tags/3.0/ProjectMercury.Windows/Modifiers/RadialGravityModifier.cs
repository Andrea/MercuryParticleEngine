namespace ProjectMercury.Modifiers
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a Modifier which pulls Particles towards it.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.RadialGravityModifierTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class RadialGravityModifier : Modifier
    {
        /// <summary>
        /// The position of the gravity well.
        /// </summary>
        public Vector2 Position;

        private float _radius;

        /// <summary>
        /// The radius of the gravity well.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the specified value is negetive or zero.
        public float Radius
        {
            get { return this._radius; }
            set
            {
#if DEBUG
                if (value <= float.Epsilon)
                    throw new ArgumentOutOfRangeException();
#endif
                this._radius = value;

                this.RadiusSquared = value * value;
            }
        }

        private float RadiusSquared;

        /// <summary>
        /// The strength of the gravity well.
        /// </summary>
        public float Strength;

        /// <summary>
        /// Processes the specified Particle.
        /// </summary>
        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            Vector2 distance;

            // Calculate the distance between the Particle and the gravity well...
            Vector2.Subtract(ref this.Position, ref particle.Position, out distance);

            // Check to see if the Particle is within range of the gravity well...
            if (distance.LengthSquared() < this.RadiusSquared)
            {
                Vector2 force;

                // We can re-use the distance vector, and normalise it as the force vector...
                Vector2.Normalize(ref distance, out force);

                // Adjust the force vector based on the strength of the gravity well and the time delta...
                Vector2.Multiply(ref force, this.Strength * elapsedSeconds, out force);

                particle.ApplyForce(ref force);
            }
        }
    }
}