using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;

namespace ProjectMercury
{
    public struct Particle
    {
        internal float Inception;       //Time at which the particle was last released.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public Vector2 Position;        //Position of the particle.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public Vector2 Momentum;        //Momentum of the particle.

        private Vector2 Acceleration;  //Acceleration of the particle (forces applied this update).

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public Vector3 Color;           //Current color of the particle.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Opacity;           //Current opacity of the particle.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Scale;             //Current scale of the particle.

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Rotation;          //Current rotation of the particle in radians..

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Mass;              //Current mass of the particle.

        /// <summary>
        /// Applies a force to the particle.
        /// </summary>
        /// <param name="force">The force to be applied.</param>
        public void ApplyForce(ref Vector2 force)
        {
            Vector2.Add(ref this.Acceleration, ref force, out this.Acceleration);
        }

        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="elapsed">Elapsed game time in whole and fractional seconds.</param>
        internal void Update(float elapsed)
        {
            Vector2 delta;

            Vector2.Add(ref this.Momentum, ref this.Acceleration, out this.Momentum);
            Vector2.Multiply(ref this.Acceleration, 0f, out this.Acceleration);
            Vector2.Multiply(ref this.Momentum, elapsed, out delta);
            Vector2.Add(ref this.Position, ref delta, out this.Position);
        }
    }
}