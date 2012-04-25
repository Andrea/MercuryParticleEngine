using System;
using Microsoft.Xna.Framework;

namespace ProjectMercury
{
    public class Particle : IParticle
    {
        private float _inception;        //Time at which the particle was last released.
        private Vector2 _position;       //Position of the particle.
        private Vector2 _momentum;       //Momentum of the particle.
        private Vector2 _acceleration;   //Acceleration of the particle (forces applied this update).
        private Vector3 _color;          //Current color of the particle.
        private float _opacity;          //Current opacity of the particle.
        private float _scale;            //Current scale of the particle.
        private float _rotation;         //Current rotation of the particle in radians..

        /// <summary>
        /// Gets or sets the game time at which the particle was released, in whole and fractional seconds.
        /// </summary>
        public float Inception
        {
            get { return this._inception; }
            set { this._inception = value; }
        }

        /// <summary>
        /// Gets or sets the current position of the particle.
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        /// <summary>
        /// Gets or sets the current momentum of the particle.
        /// </summary>
        public Vector2 Momentum
        {
            get { return this._momentum; }
            set { this._momentum = value; }
        }

        /// <summary>
        /// Gets or sets the current acceleration of the particle.
        /// </summary>
        public Vector2 Acceleration
        {
            get { return this._acceleration; }
            set { this._acceleration = value; }
        }

        /// <summary>
        /// Gets or sets the current color of the particle.
        /// </summary>
        public Vector3 Color
        {
            get { return this._color; }
            set { this._color = value; }
        }

        /// <summary>
        /// Gets or sets the opacity of the particle.
        /// </summary>
        public float Opacity
        {
            get { return this._opacity; }
            set { this._opacity = value; }
        }

        /// <summary>
        /// gets or sets the current scale of the particle.
        /// </summary>
        public float Scale
        {
            get { return this._scale; }
            set { this._scale = value; }
        }

        /// <summary>
        /// gets or sets the current rotation of the particle.
        /// </summary>
        public float Rotation
        {
            get { return this._rotation; }
            set { this._rotation = value; }
        }

        /// <summary>
        /// Applies a force to the particle.
        /// </summary>
        /// <param name="force">The force to be applied.</param>
        public virtual void ApplyForce(ref Vector2 force)
        {
            Vector2.Add(ref this._acceleration, ref force, out this._acceleration);
        }

        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="elapsed">Elapsed game time in whole and fractional seconds.</param>
        public virtual void Update(float elapsed)
        {
            Vector2 delta;

            Vector2.Add(ref this._momentum, ref this._acceleration, out this._momentum);
            this._acceleration = Vector2.Zero;
            Vector2.Multiply(ref this._momentum, elapsed, out delta);
            Vector2.Add(ref this._position, ref delta, out this._position);
        }
    }
}