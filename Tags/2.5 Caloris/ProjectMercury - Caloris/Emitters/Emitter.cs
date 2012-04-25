using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Modifiers;

namespace ProjectMercury.Emitters
{
    [Serializable]
    public class Emitter
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Emitter()
        {
            _rnd = new Random(Environment.TickCount);
        }

        /// <summary>
        /// Static random number generator.
        /// </summary>
        static private Random _rnd;

        /// <summary>
        /// Gets the random number generator for derived classes.
        /// </summary>
        static protected Random Random
        {
            get { return _rnd; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="budget">The number of particles that will be available to the emitter.</param>
        /// <param name="term">The amount of time in whole and fractional seconds that particles shall remain
        /// active once they are released.</param>
        public Emitter(int budget, float term)
        {
            this._budget = budget;
            this._term = term;

            this._pool = new ParticlePool(budget);
        }

        /// <summary>
        /// Called after the parent effect has been imported.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        public virtual void AfterImport(Game game)
        {
            this._pool = new ParticlePool(this._budget);
            this._triggers = new Queue<Vector2>();

            for (int i = 0; i < this._modifiers.Count; i++)
                this._modifiers[i].AfterImport(game);
        }

        private int _budget;                                        //Number of particles available.
        private float _term;                                        //Lifetime of released particles.

        [NonSerialized]
        private ParticlePool _pool;                                 //Particle pool.

        [NonSerialized]
        private Queue<Vector2> _triggers = new Queue<Vector2>();    //Triggers queue.
        private int _quantity = 1;                                  //Number of particles to release per trigger.
        private List<Modifier> _modifiers = new List<Modifier>();   //Modifiers which have been applied.
        private float _particleSpeed;                               //The speed of released particles.
        private float _particleSpeedVar;                            //The speed variation of released particles.

        [NonSerialized]
        private Texture2D _texture;                                 //Reference to a texture used to draw particles.

        /// <summary>
        /// Gets the maximum number of particles available to the emitter.
        /// </summary>
        public int Budget
        {
            get { return this._budget; }
        }

        /// <summary>
        /// Gets or sets the number of particles that shall be released for every trigger.
        /// </summary>
        public int ReleaseQuantity
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        /// <summary>
        /// Gets or sets the speed of particles when they are released.
        /// </summary>
        public float ParticleSpeed
        {
            get { return this._particleSpeed; }
            set { this._particleSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the random variation of the speed of particles when they are released. This is expressed
        /// as a multiple of the baseline speed.
        /// </summary>
        public float ParticleSpeedVariation
        {
            get { return this._particleSpeedVar; }
            set { this._particleSpeedVar = value; }
        }

        /// <summary>
        /// Gets or sets the texture used to render particles from the emitter.
        /// </summary>
        public Texture2D ParticleTexture
        {
            get { return this._texture; }
            set { this._texture = value; }
        }

        /// <summary>
        /// Gets the number of currently active particles belonging to the emitter.
        /// </summary>
        public int ActiveParticlesCount
        {
            get { return this._pool.ActiveCount; }
        }

        /// <summary>
        /// Gets the list of modifiers which have been applied to the emitter.
        /// </summary>
        public IList<Modifier> Modifiers
        {
            get { return this._modifiers; }
        }

        /// <summary>
        /// Allows access to active particles by index.
        /// </summary>
        /// <param name="index">The index number of the active particle to get.</param>
        /// <returns>The active particle at the specified index.</returns>
        public Particle Get(int index)
        {
            Particle particle;

            this._pool.Get(index, out particle);

            return particle;
        }

        /// <summary>
        /// Updates the emitter.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        internal void Update(float totalTime, float elapsedTime)
        {
            //Release if necessary...
            while (this._triggers.Count > 0)
            {
                Vector2 trigger = this._triggers.Dequeue();

                for (int i = 0; i < this._quantity; i++)
                {
                    if (this._pool.AvailableCount > 0)
                    {
                        Vector2 offset, orientation, releasePos, force;

                        int index = this._pool.Fetch();
                        Particle particle;
                        this._pool.Get(index, out particle);

                        //Set the particles birth time to 'now'...
                        particle.Inception = totalTime;

                        //Do the actual releasing...
                        particle.Momentum = Vector2.Zero;

                        this.GenerateParticleOffsetAndOrientation(out offset, out orientation);

                        Vector2.Add(ref trigger, ref offset, out releasePos);
                        particle.Position = releasePos;

                        float speed = this._particleSpeed * elapsedTime;
                        speed *= (this._particleSpeed * (this._particleSpeedVar * (float)_rnd.NextDouble())) - (this._particleSpeed / 2f);
                        Vector2.Multiply(ref orientation, speed, out force);
                        particle.ApplyForce(ref force);

                        //Send the particle to the modifiers...
                        for (int ii = 0; ii < this._modifiers.Count; ii++)
                            this._modifiers[ii].ProcessReleasedParticle(totalTime, ref particle);

                        this._pool.Set(index, ref particle);
                    }
                    else
                    {
                        //Raise starving event...
                        break;
                    }
                }
            }

            //Update active particles...
            for (int i = 0; i < this._pool.ActiveCount; i++)
            {
                Particle particle;
                this._pool.Get(i, out particle);

                float age = (totalTime - particle.Inception) / this._term;

                if (age < 1.0f)
                {
                    particle.Update(elapsedTime);

                    //Send the particle to the modifiers...
                    for (int ii = 0; ii < this._modifiers.Count; ii++)
                        this._modifiers[ii].ProcessActiveParticle(totalTime, elapsedTime, ref particle,age);

                    this._pool.Set(i, ref particle);
                }
                else
                {
                    //Send the particle to the modifiers...
                    for (int ii = 0; ii < this._modifiers.Count; ii++)
                        this._modifiers[ii].ProcessRetiredParticle(totalTime, ref particle);

                    this._pool.Retire();
                }
            }
        }

        /// <summary>
        /// Generates an offset and orientation vector for a Particle as it is released.
        /// </summary>
        /// <param name="offset">The offset of the particle from the trigger position.</param>
        /// <param name="orientation">The orientation of the particle as a unit vector representing radians.</param>
        protected virtual void GenerateParticleOffsetAndOrientation(out Vector2 offset, out Vector2 orientation)
        {
            float rads = (float)_rnd.NextDouble() * MathHelper.TwoPi;

            offset = Vector2.Zero;
            orientation = new Vector2((float)Math.Sin(rads), (float)Math.Cos(rads));
        }

        /// <summary>
        /// Triggers the emitter at the specified position.
        /// </summary>
        /// <param name="position">The position at which the emitter should release particles.</param>
        public void Trigger(ref Vector2 position)
        {
            this._triggers.Enqueue(position);
        }
    }
}