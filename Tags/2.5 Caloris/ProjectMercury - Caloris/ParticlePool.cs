using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ProjectMercury
{
    /// <summary>
    /// Maintains a pool of Particles, allowing the engine to fetch and retire them,
    /// and iterate through active particles.
    /// </summary>
    /// <remarks>Assumes that the lifetime of active particles will be the same.</remarks>
    public class ParticlePool
    {
        private int _budget;        //The number of particles in the pool.
        private Particle[] _pool;   //The pool array.
        private int _idle;          //The array index of the next idle particle.
        private int _active;        //The array index of the first active particle.

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="budget">The number of objects that will be available in the pool.</param>
        /// <param name="builder">Method which will instantiate a pool object.</param>
        public ParticlePool(int budget)
        {
            this._budget = budget;
            this._pool = new Particle[budget];

            for (int i = 0; i < budget; i++)
                this._pool[i] = new Particle();

            this._idle = 0;
            this._active = 0;
        }

        /// <summary>
        /// Gets the number of objects which are active in the pool.
        /// </summary>
        public int ActiveCount
        {
            get { return this._idle - this._active; }
        }

        /// <summary>
        /// Gets the number of objects which are idle in the pool.
        /// </summary>
        public int AvailableCount
        {
            get { return this._budget - this.ActiveCount; }
        }

        /// <summary>
        /// Takes the next idle object, and activates it.
        /// </summary>
        /// <returns>The index* of the object which was activated.</returns>
        /// <remarks>*Index in relation to the indexer, not the array.</remarks>
        public int Fetch()
        {
            this._idle++;

            return this.ActiveCount -1;
        }

        /// <summary>
        /// Retires the oldest active object.
        /// </summary>
        public void Retire()
        {
            this._active++;

            if (this._active >= this._budget)
            {
                this._idle -= this._budget;
                this._active -= this._budget;
            }
        }

        /// <summary>
        /// Gets an active particle by index.
        /// </summary>
        /// <param name="index">The index number of the particle to get.</param>
        /// <param name="particle">The retrieved particle.</param>
        public void Get(int index, out Particle particle)
        {
            particle = this._pool[(this._active + index < this._budget
                ? this._active + index : (this._active + index) - this._budget)];
        }

        /// <summary>
        /// Sets an active particle by index.
        /// </summary>
        /// <param name="index">The index number of the particle to set.</param>
        /// <param name="particle">The particle to set.</param>
        public void Set(int index, ref Particle particle)
        {
            this._pool[(this._active + index < this._budget ? this._active + index
                : (this._active + index) - this._budget)] = particle;
        }
    }
}