using System;
using Microsoft.Xna.Framework;

namespace ProjectMercury
{
    /// <summary>
    /// Defines the minimum interface that must be exposed by all particle structures.
    /// </summary>
    public interface IParticle
    {
        /// <summary>
        /// The game time at which the particles was released, in whole and fractional seconds.
        /// </summary>
        float Inception { get; set; }

        /// <summary>
        /// The current position of the particle.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// The current momentum of the particle.
        /// </summary>
        Vector2 Momentum { get; set; }

        /// <summary>
        /// The current acceleration of the particle, that is, the sum of all forces applied this frame.
        /// </summary>
        Vector2 Acceleration { get; set; }

        /// <summary>
        /// The current color of the particle.
        /// </summary>
        Vector3 Color { get; set; }

        /// <summary>
        /// The current opacity of the particle.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        /// The current scale of the particle in pixels.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// The current rotation of the particle in raidians.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Applies a force to the particle.
        /// </summary>
        /// <param name="force">The force to be applied in pixels per second.</param>
        void ApplyForce(ref Vector2 force);

        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="elapsed">Elapsed game time in whole and fractional seconds.</param>
        void Update(float elapsed);
    }
}
