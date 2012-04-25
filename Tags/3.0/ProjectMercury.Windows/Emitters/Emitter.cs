namespace ProjectMercury.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ProjectMercury.Modifiers;

    /// <summary>
    /// Defines the base class for a Particle Emitter. The basic implementation releases Particles from a single point.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Emitters.EmitterTypeConverter, ProjectMercury.Design")]
#endif
    public class Emitter
    {
#if DEBUG
        /// <summary>
        /// True if the Emitter has been initialised.
        /// </summary>
        [ContentSerializerIgnore]
        public bool Initialised { get; private set; }
#else
        [ContentSerializerIgnore]
        public bool Initialised;
#endif

#if DEBUG
        private int _budget;

        /// <summary>
        /// Gets or sets the number of Particles which are available to the Emitter.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if trying to set this property after the Emitter has been initialised.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the specified value is less than 1.</exception>
        public int Budget
        {
            get { return this._budget; }
            set
            {
                if (this.Initialised)
                    throw new InvalidOperationException();

                if (value < 1)
                    throw new ArgumentOutOfRangeException();

                this._budget = value;
            }
        }
#else
        public int Budget;
#endif

#if DEBUG
        private float _term;

        /// <summary>
        /// Gets or sets the length of time that released Particles will remain active, in whole and fractional seconds.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if trying to set this property after the Emitter has been initialised.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the supplied value is less than or equal to 0.</exception>
        public float Term
        {
            get { return this._term; }
            set
            {
                if (this.Initialised)
                    throw new InvalidOperationException();

                if (value < float.Epsilon)
                    throw new ArgumentOutOfRangeException();

                this._term = value;
            }
        }
#else
        public float Term;
#endif

        [ContentSerializerIgnore]
        public Particle[] Particles;
        
        [ContentSerializerIgnore]
        private int Idle;
        
        [ContentSerializerIgnore]
        protected Queue<Vector2> Triggers;
        
#if DEBUG
        private int _releaseQuantity;

        /// <summary>
        /// Gets or sets the number of Particles which will be released on each trigger.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the specified value is less than 1.</exception>
        public int ReleaseQuantity
        {
            get { return this._releaseQuantity; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException();

                this._releaseQuantity = value;
            }
        }
#else
        public int ReleaseQuantity;
#endif
        
        /// <summary>
        /// Gets or sets the speed at which Particles travel when they are released.
        /// </summary>
        public VariableFloat ReleaseSpeed;
        
        /// <summary>
        /// Gets or sets the colour of released Particles.
        /// </summary>
        public Vector3 ReleaseColour;
        
        /// <summary>
        /// Gets or sets the opacity of released Particles.
        /// </summary>
        public VariableFloat ReleaseOpacity;
        
        /// <summary>
        /// Gets or sets the scale of released particles.
        /// </summary>
        public VariableFloat ReleaseScale;
        
        /// <summary>
        /// Gets or sets the rotation of released Particles.
        /// </summary>
        public VariableFloat ReleaseRotation;

        /// <summary>
        /// Gets the asset name of the texture to use when rendering Particles.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public string ParticleTextureAssetName;

        /// <summary>
        /// Gets or sets the Texture2D used to display the Particles.
        /// </summary>
        [ContentSerializerIgnore]
        public Texture2D ParticleTexture;
        
        /// <summary>
        /// Gets the collection of Modifiers which are acting upon the Emitter.
        /// </summary>
        public ModifierCollection Modifiers;

        /// <summary>
        /// Gets or sets the TagBinder implementation used to attach custom data to Particles.
        /// </summary>
        [ContentSerializerIgnore]
        public ITagBinder TagBinder;

        /// <summary>
        /// The array of custom tags which are currently attached to the active Particles.
        /// </summary>
        [ContentSerializerIgnore]
        private object[] Tags;

        /// <summary>
        /// Instantiates a new instance of the Emitter class.
        /// </summary>
        public Emitter()
        {
            this.Triggers = new Queue<Vector2>();
            this.Modifiers = new ModifierCollection();
        }

        /// <summary>
        /// Gets the number of Particles which are currently active.
        /// </summary>
        public int ActiveParticlesCount
        {
            get { return this.Idle; }
        }

        /// <summary>
        /// Initialises the Emitter.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if the Term and/or Budget properties have not been set.</exception>
        public void Initialize()
        {
#if DEBUG
            if (this.Term <= 0 || this.Budget <= 0)
                throw new InvalidOperationException();
#endif
            this.Particles = new Particle[this.Budget];
            this.Tags = new object[this.Budget];
            this.Idle = 0;

            this.Initialised = true;
        }

        /// <summary>
        /// Loads resources required by the Emitter via a ContentManager.
        /// </summary>
        /// <param name="content">The ContentManager used to load resources.</param>
        public void LoadContent(ContentManager content)
        {
            if (this.ParticleTextureAssetName != null)
                this.ParticleTexture = content.Load<Texture2D>(this.ParticleTextureAssetName);
        }

        /// <summary>
        /// Releases the specified number of Particles, at the specified trigger position.
        /// </summary>
        /// <param name="totalSeconds">Total game time in whole and fractional seconds.</param>
        /// <param name="count">The number of particles to release.</param>
        /// <param name="triggerPosition">The position of the trigger.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if the Emitter has not been initialised.</exception>
        private void ReleaseParticles(float totalSeconds, int count, ref Vector2 triggerPosition)
        {
#if DEBUG
            if (!this.Initialised)
                throw new InvalidOperationException();
#endif
            for (int i = 0; i < count; i++)
            {
                // Check to see that there is an idle Particle available...
                if (this.Idle < this.Budget)
                {
                    // Get the next available idle Particle...
                    Particle particle = this.Particles[this.Idle];

                    Vector2 offset, force;

                    // Generate and offset and force vector for the particle...
                    this.GenerateParticleOffset(totalSeconds, ref triggerPosition, out offset);
                    this.GenerateParticleForce(ref offset, out force);

                    // Add the trigger position and offset vector to get the particles release position...
                    Vector2.Add(ref triggerPosition, ref offset, out particle.Position);

                    // Calculate the velocity of the particle using the force vector and the release velocity...
                    Vector2.Multiply(ref force, this.ReleaseSpeed, out particle.Velocity);

                    particle.Momentum   = Vector2.Zero;
                    particle.Inception  = totalSeconds;
                    particle.Age        = 0f;
                    particle.Colour     = new Vector4(this.ReleaseColour, this.ReleaseOpacity);
                    particle.Scale      = this.ReleaseScale;
                    particle.Rotation   = this.ReleaseRotation;

                    this.Particles[this.Idle] = particle;

                    // Instruct the TagBinder to supply a tag for this Particle...
                    if (this.TagBinder != null)
                        this.Tags[this.Idle] = this.TagBinder.GetTag(ref particle);

                    // Increment the idle marker...
                    this.Idle++;
                }
            }
        }

        /// <summary>
        /// Retires the specified number of Particles.
        /// </summary>
        private void RetireParticles(int count)
        {
            // Instruct the TagBinder to remove tags for the retired Particles...
            if (this.TagBinder != null)
                for (int i = 0; i < count; i++)
                    this.TagBinder.DisposeTag(this.Tags[i]);

            // Move the remaining particles and tags to the front of their respective arrays...
            for (int i = count; i < this.Idle; i++)
            {
                this.Particles[i - count] = this.Particles[i];
                this.Tags[i - count] = this.Tags[i];
            }

            // Decrement the idle marker accordingly...
            this.Idle -= count;
        }

        /// <summary>
        /// Updates the Emitter and all Particles within.
        /// </summary>
        /// <param name="totalSeconds">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedSeconds">Elapsed frame time in whole and fractional seconds.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if the Emitter has not been initialised.</exception>
        public void Update(float totalSeconds, float elapsedSeconds)
        {
#if DEBUG
            if (!this.Initialised)
                throw new InvalidOperationException();
#endif
            // Check to see if the Emitter has been triggered...
            while (this.Triggers.Count > 0)
            {
                Vector2 triggerPosition = this.Triggers.Dequeue();

                // Release some particles...
                this.ReleaseParticles(totalSeconds, this.ReleaseQuantity, ref triggerPosition);
            }

            // Track the number of Particles which have expired...
            int expiredCount = 0;

            // Begin iterating through all active Particles...
            for (int i = 0; i < this.Idle; i++)
            {
                Particle particle = this.Particles[i];

                // Calculate the age of the Particle...
                particle.Age = (totalSeconds - particle.Inception) / this.Term;

                // If its age is >= 1, we need not do any further processing on it, as it will be retired...
                if (particle.Age >= 1.0f)
                {
                    expiredCount++;

                    continue;
                }

                // Otherwise, send the Particle to any Modifiers acting upon the Emitter...
                if (this.TagBinder != null)
                    this.Modifiers.RunProcessors(totalSeconds, elapsedSeconds, ref particle, this.Tags[i]);
                else
                    this.Modifiers.RunProcessors(totalSeconds, elapsedSeconds, ref particle, null);

                // Update the Particle...
                particle.Update(elapsedSeconds);

                this.Particles[i] = particle;
            }

            // If there were Particles which expired, retire them now...
            if (expiredCount > 0) { this.RetireParticles(expiredCount); }
        }

        /// <summary>
        ///  Triggers the Emitter at the specified position...
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if the Emitter has not been initialised.</exception>
        public void Trigger(Vector2 position)
        {
#if DEBUG
            if (!this.Initialised)
                throw new InvalidOperationException();
#endif
            this.Triggers.Enqueue(position);
        }

        /// <summary>
        /// Generates an offset vector for a Particle as it is released.
        /// </summary>
        protected virtual void GenerateParticleOffset(float totalSeconds, ref Vector2 triggerPosition, out Vector2 offset)
        {
            offset = Vector2.Zero;
        }

        /// <summary>
        /// Generates a normalised force vector for a Particle as it is released.
        /// </summary>
        protected virtual void GenerateParticleForce(ref Vector2 offset, out Vector2 force)
        {
            float radians = RandomHelper.NextFloat(MathHelper.TwoPi);

            force = new Vector2
            {
                X = (float)Math.Sin(radians),
                Y = (float)Math.Cos(radians)
            };
        }
    }
}