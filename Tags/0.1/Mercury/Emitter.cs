using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Mercury
{
    public abstract class Emitter : GameComponent
    {
        //===================
        //Fields & Properties
        //===================
        #region Rnd
        /// <summary>
        /// Static random number generator
        /// </summary>
        protected static Random _rnd;

        /// <summary>
        /// Gets the random number generator
        /// </summary>
        protected Random Rnd
        {
            get { return _rnd; }
        }
        #endregion
        #region Initialised
        /// <summary>
        /// Flag true if the emitter has been initialised
        /// </summary>
        private bool _inited = false;
        #endregion
        #region ParticleSystem
        /// <summary>
        /// Particle system that this emitter is managed by
        /// </summary>
        private ParticleSystem _particleSystem;

        /// <summary>
        /// Gets or sets the particle system that this emitter is managed by
        /// </summary>
        public ParticleSystem ParticleSystem
        {
            get { return _particleSystem; }
            set { _particleSystem = value; }
        }
        #endregion
        #region Enabled
        /// <summary>
        /// Flag true if the emitter is enabled
        /// </summary>
        private bool _enabled = true;

        /// <summary>
        /// Gets or sets wether or not the emitter is enabled
        /// </summary>
        [Category("Misc")]
        [Description("Flag true to enable the emitter")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        #endregion

        #region ActiveParticles
        /// <summary>
        /// A list of current active particles spawned by this emitter
        /// </summary>
        private List<Particle> _activeParticles;

        /// <summary>
        /// Gets the list of curret active particles spawned by this emitter
        /// </summary>
        internal List<Particle> ActiveParticles
        {
            get { return _activeParticles; }
        }
        #endregion
        #region Limbo
        /// <summary>
        /// A Queue of expired particles waiting to be reincarnated
        /// </summary>
        private Queue<Particle> _limbo;

        /// <summary>
        /// Gets the queue of expired particles
        /// </summary>
        internal Queue<Particle> Limbo
        {
            get { return _limbo; }
        }
        #endregion

        #region Position
        /// <summary>
        /// The screen position of the emitter
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The initial X axis position
        /// </summary>
        private int _x = 0;

        /// <summary>
        /// Gets or sets the initial X axis position
        /// </summary>
        [Category("Misc")]
        [Description("The initial X position of the emitter")]
        [DefaultValue(0)]
        public int X
        {
            get { return _x; }
            set
            {
                if (!_inited)
                    _x = value;
            }
        }

        /// <summary>
        /// The initial Y axis position
        /// </summary>
        private int _y = 0;

        /// <summary>
        /// Gets or sets the initial Y axis position
        /// </summary>
        [Category("Misc")]
        [Description("The initial Y position of the emitetr")]
        [DefaultValue(0)]
        public int Y
        {
            get { return _y; }
            set
            {
                if (!_inited)
                    _y = value;
            }
        }
        #endregion

        #region AutoTrigger
        /// <summary>
        /// Flag true to enable emitter auto-trigger
        /// </summary>
        private bool _autoTrigger = false;

        /// <summary>
        /// Gets or sets wether or not the emitter should auto-trigger
        /// </summary>
        [Category("Triggering")]
        [Description("Flag true to enable auto-trigger")]
        [DefaultValue(false)]
        public bool AutoTrigger
        {
            get { return _autoTrigger; }
            set { _autoTrigger = value; }
        }
        #endregion
        #region AutoTriggerFrequency
        /// <summary>
        /// The frequency the emitter auto-triggers
        /// </summary>
        private uint _autoTriggerFreq = 1000;

        /// <summary>
        /// Gets or sets the frequency at which the emitter should auto-trigger
        /// </summary>
        [Category("Triggering")]
        [Description("The frequency at which the emitter will auto-trigger")]
        [DefaultValue(1000)]
        public uint AutoTriggerFrequency
        {
            get { return _autoTriggerFreq; }
            set
            {
                _autoTriggerFreq = value;

                if (_inited)
                {
                    _autoTriggerTicker.Frequency = value;
                }
            }
        }
        #endregion
        #region AutoTriggerTicker
        private Ticker _autoTriggerTicker;
        #endregion

        #region EmitQuantity
        /// <summary>
        /// The number of particles to spawn per trigger
        /// </summary>
        private byte _emitQuantity = 1;

        /// <summary>
        /// Gets or sets the number of particles spawned per trigger
        /// </summary>
        [Category("Triggering")]
        [Description("The number of particles spawned per trigger")]
        [DefaultValue(1)]
        public byte EmitQuantity
        {
            get { return _emitQuantity; }
            set { _emitQuantity = value; }
        }
        #endregion

        #region Scale

        #region ScaleCurve

        /// <summary>
        /// The curve used to interpolate particle scale
        /// </summary>
        private PreCurve _scaleCurve;

        /// <summary>
        /// Gets the curve used to interpolate particle scale
        /// </summary>
        internal PreCurve ScaleCurve
        {
            get { return _scaleCurve; }
        }

        #endregion
        #region InitialScale

        /// <summary>
        /// Initial scale of spawned particles
        /// </summary>
        private float _initialScale = 1f;

        /// <summary>
        /// Gets or sets the initial scale of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleScale")]
        [Description("Initial scale of spawned particles")]
        [DefaultValue(1f)]
        public float InitialScale
        {
            get { return _initialScale; }
            set
            {
                if (!_inited)
                    _initialScale = MathHelper.Clamp(value, 0f, 100f);
            }
        }

        #endregion
        #region MidScale

        /// <summary>
        /// Mid scale of spawned particles
        /// </summary>
        private float _midScale = 1f;

        /// <summary>
        /// Gets or sets the mid scale of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in the design view</remarks>
        [Category("ParticleScale")]
        [Description("Mid scale of spawned particles")]
        [DefaultValue("1f")]
        public float MidScale
        {
            get { return _midScale; }
            set
            {
                if (!_inited)
                    _midScale = MathHelper.Clamp(value, 0f, 100f);
            }
        }

        /// <summary>
        /// Mid scale sweep of spawned particles
        /// </summary>
        private float _midScaleSweep = .5f;

        /// <summary>
        /// Gets or sets the mid scale sweep of spawned particles
        /// </summary>
        [Category("ParticleScale")]
        [Description("Mid scale sweep of spawned particles")]
        [DefaultValue(0.5f)]
        public float MidScaleSweep
        {
            get { return _midScaleSweep; }
            set
            {
                _midScaleSweep = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        #endregion
        #region FinalScale

        /// <summary>
        /// Final scale of spawned particles
        /// </summary>
        private float _finalScale = 1f;

        /// <summary>
        /// Gets or sets the final scale of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleScale")]
        [Description("Final scale of spawned particles")]
        [DefaultValue(1f)]
        public float FinalScale
        {
            get { return _finalScale; }
            set
            {
                if (!_inited)
                    _finalScale = MathHelper.Clamp(value, 0f, 100f);
            }
        }

        #endregion

        #endregion
        #region Rotation

        /// <summary>
        /// Number of revolutions the particle turns
        /// </summary>
        private float _revolutions = 0f;

        /// <summary>
        /// Gets or sets the number of revolutions of spawned particles
        /// </summary>
        /// <remarks>This property is *always* editable</remarks>
        [DefaultValue(0f)]
        [Description("Number of revolutions the particle turns")]
        [Category("ParticleRotation")]
        public float Revolutions
        {
            get { return _revolutions; }
            set { _revolutions = MathHelper.Clamp(value, 0f, 100f); }
        }

        #endregion

        #region Alpha

        /// <summary>
        /// Curve used to interpolate particle alpha
        /// </summary>
        private PreCurve _alphaCurve;

        /// <summary>
        /// Gets the curve used to interpolate particle alpha
        /// </summary>
        internal PreCurve AlphaCurve
        {
            get { return _alphaCurve; }
        }

        /// <summary>
        /// Initial alpha of spawned particles
        /// </summary>
        private float _initialAlpha = 1f;

        /// <summary>
        /// Gets or sets the initial alpha of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleAlpha")]
        [Description("Initial alpha of spawned particles")]
        [DefaultValue(1f)]
        public float InitialAlpha
        {
            get { return _initialAlpha; }
            set
            {
                if (!_inited)
                    _initialAlpha = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        /// <summary>
        /// Mid alpha of spawned particles
        /// </summary>
        private float _midAlpha = 1f;

        /// <summary>
        /// Gets or sets the mid alpha of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleAlpha")]
        [Description("Mid alpha of spawned particles")]
        [DefaultValue(1f)]
        public float MidAlpha
        {
            get { return _midAlpha; }
            set
            {
                if (!_inited)
                    _midAlpha = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        /// <summary>
        /// Mid alpha sweep of spawned particles
        /// </summary>
        private float _midAlphaSweep = .5f;

        /// <summary>
        /// Gets or sets the mid alpha sweep of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleAlpha")]
        [Description("Mid alpha sweep of spawned particles")]
        [DefaultValue(.5f)]
        public float MidAlphaSweep
        {
            get { return _midAlphaSweep; }
            set
            {
                if (!_inited)
                    _midAlphaSweep = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        /// <summary>
        /// Final alpha of spawned particles
        /// </summary>
        private float _finalAlpha = 1f;

        /// <summary>
        /// Gets or sets the final alpha of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleAlpha")]
        [Description("Final alpha of spawned particles")]
        [DefaultValue(1f)]
        public float FinalAlpha
        {
            get { return _finalAlpha; }
            set
            {
                if (!_inited)
                    _finalAlpha = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        #endregion

        #region Color

        /// <summary>
        /// Curve used to interpolate particle color red component
        /// </summary>
        private PreCurve _redCurve;

        /// <summary>
        /// Gets the curve used to interpolate particle color red component
        /// </summary>
        internal PreCurve RedCurve
        {
            get { return _redCurve; }
        }

        /// <summary>
        /// Curve used to interpolate particle color green component
        /// </summary>
        private PreCurve _greeenCurve;

        /// <summary>
        /// Gets the curve used to interpolate particle color green component
        /// </summary>
        internal PreCurve GreenCurve
        {
            get { return _greeenCurve; }
        }

        /// <summary>
        /// Curve used to interpolate particle color blue component
        /// </summary>
        private PreCurve _blueCurve;

        /// <summary>
        /// Gets the curve used to interpolate particle color blue component
        /// </summary>
        internal PreCurve BlueCurve
        {
            get { return _blueCurve; }
        }

        #endregion
        #region InitialColor

        /// <summary>
        /// Initial color of spawned particles
        /// </summary>
        private System.Drawing.Color _initialColor = System.Drawing.Color.White;

        /// <summary>
        /// Gets or sets the initial color of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleColor")]
        [Description("Initial color of spawned particles")]
        public System.Drawing.Color InitialColor
        {
            get { return _initialColor; }
            set
            {
                if (!_inited)
                    _initialColor = value;
            }
        }

        #endregion
        #region MidColor

        /// <summary>
        /// The mid color of spawned particles
        /// </summary>
        private System.Drawing.Color _midColor = System.Drawing.Color.White;

        /// <summary>
        /// Gets or sets the mid color of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleColor")]
        [Description("The mid color of spawned particles")]
        public System.Drawing.Color MidColor
        {
            get { return _midColor; }
            set
            {
                if (!_inited)
                    _midColor = value;
            }
        }

        /// <summary>
        /// The mid color sweep of spawned particles
        /// </summary>
        private float _midColorSweep = .5f;

        /// <summary>
        /// Gets or sets the mid color sweep of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleColor")]
        [Description("The mid color sweep of spawned particles")]
        [DefaultValue(.5f)]
        public float MidColorSweep
        {
            get { return _midColorSweep; }
            set
            {
                if (!_inited)
                    _midColorSweep = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        #endregion
        #region FinalColor

        /// <summary>
        /// The final color of spawned particles
        /// </summary>
        private System.Drawing.Color _finalColor = System.Drawing.Color.White;

        /// <summary>
        /// Gets or sets the final color of spawned particles
        /// </summary>
        /// <remarks>This property can only be altered in design view</remarks>
        [Category("ParticleColor")]
        [Description("The final color of spawned particles")]
        public System.Drawing.Color FinalColor
        {
            get { return _finalColor; }
            set
            {
                if (!_inited)
                    _finalColor = value;
            }
        }

        #endregion

        #region ParticleLifespan
        /// <summary>
        /// The initial lifespan of spawned particles
        /// </summary>
        private uint _baseLifespan = 1000;

        /// <summary>
        /// Gets or sets the initial lifespan of spawned particles
        /// </summary>
        [Category("ParticleLifespan")]
        [Description("The initial lifespan of spawned particles")]
        [DefaultValue(1000)]
        public uint ParticleLifespan
        {
            get { return _baseLifespan; }
            set { _baseLifespan = value; }
        }
        #endregion
        #region ParticleLifespanVariation
        /// <summary>
        /// The variation of base particle lifespan
        /// </summary>
        private float _lifespanVariation = 0f;

        /// <summary>
        /// Gets or sets the variation of base particle lifespan
        /// </summary>
        [Category("ParticleLifespan")]
        [Description("The variation of base particle lifespan")]
        [DefaultValue(0f)]
        public float ParticleLifespanVariation
        {
            get { return _lifespanVariation; }
            set { _lifespanVariation = MathHelper.Clamp(value, 0f, 1f); }
        }
        #endregion
        #region ParticleLifespanVariated
        /// <summary>
        /// Calculates a lifespan value with random variation
        /// </summary>
        internal uint ParticleLifespanVariated
        {
            get
            {
                float variation = ((float)_rnd.NextDouble() * _lifespanVariation) - (_lifespanVariation / 2f);
                return (uint)(_baseLifespan + (_baseLifespan * variation));
            }
        }
        #endregion

        #region ParticleSpeed
        /// <summary>
        /// The base speed of spawned particles
        /// </summary>
        private float _baseSpeed = 0f;

        /// <summary>
        /// Gets or sets the base speed of spawned particles
        /// </summary>
        [Category("ParticleSpeed")]
        [Description("The base speed of spawned particles")]
        [DefaultValue(0f)]
        public float ParticleSpeed
        {
            get { return _baseSpeed; }
            set { _baseSpeed = value; }
        }
        #endregion
        #region ParticleSpeedVariation
        /// <summary>
        /// The variation of base particle speed
        /// </summary>
        private float _speedVariation = 0f;

        /// <summary>
        /// Gets or sets the variation of base particle speed
        /// </summary>
        [Category("ParticleSpeed")]
        [Description("The variation of base particle speed")]
        [DefaultValue(0f)]
        public float ParticleSpeedVariation
        {
            get { return _speedVariation; }
            set { _speedVariation = MathHelper.Clamp(value, 0f, 1f); }
        }
        #endregion
        #region ParticleSpeedDelta
        /// <summary>
        /// The acceleration or deceleration of spawned particles
        /// </summary>
        private float _speedDelta = 0f;

        /// <summary>
        /// Gets or sets the acceleration | deceleration of spawned particles
        /// </summary>
        [Category("ParticleSpeed")]
        [Description("The acceleration | deceleration of spawned particles")]
        [DefaultValue(0f)]
        public float ParticleSpeedDelta
        {
            get { return _speedDelta; }
            set { _speedDelta = value; }
        }
        #endregion
        #region ParticleSpeedMin
        /// <summary>
        /// The minimum speed of spawned particles
        /// </summary>
        private float _speedMin = -1000f;

        /// <summary>
        /// Gets or sets the minimum speed of spawned particles
        /// </summary>
        [Category("ParticleSpeed")]
        [Description("The minimum speed of spawned particles")]
        [DefaultValue(-1000f)]
        public float ParticleSpeedMin
        {
            get { return _speedMin; }
            set { _speedMin = MathHelper.Clamp(value, -1000f, 1000f); }
        }
        #endregion
        #region ParticleSpeedMax
        /// <summary>
        /// The maximum speed of spawned particles
        /// </summary>
        private float _speedMax = 1000f;

        /// <summary>
        /// Gets or sets the maximum speed of spawned particles
        /// </summary>
        [Category("ParticleSpeed")]
        [Description("The maximum speed of spawned particles")]
        [DefaultValue(1000f)]
        public float ParticleSpeedMax
        {
            get { return _speedMax; }
            set { _speedMax = MathHelper.Clamp(value, -1000f, 1000f); }
        }
        #endregion
        #region ParticleSpeedVariated
        /// <summary>
        /// Calculates a particle speed with random variation
        /// </summary>
        protected internal float ParticleSpeedVariated
        {
            get
            {
                float variation = ((float)_rnd.NextDouble() * _speedVariation) - (_speedVariation / 2f);
                return _baseSpeed + (_baseSpeed * variation);
            }
        }
        #endregion

        #region GraphicLoaded
        /// <summary>
        /// Flag true if a graphic has been assigned to the emitter
        /// </summary>
        private bool _graphicLoaded = false;
        #endregion
        #region Graphic
        /// <summary>
        /// The image used to render particles spawned from this emitter
        /// </summary>
        private Texture2D _graphic;

        /// <summary>
        /// Gets or sets the image used to render particles from this emitter
        /// </summary>
        internal Texture2D Graphic
        {
            get
            {
                if (!_graphicLoaded)
                { return _particleSystem.DefaultGraphic; }
                else
                { return _graphic; }
            }
            set
            {
                _graphic = value;
                _graphicLoaded = true;
            }
        }
        #endregion
        #region GraphicRect
        /// <summary>
        /// The clipping rectangle for the particle image
        /// </summary>
        private Microsoft.Xna.Framework.Rectangle _graphicRect;

        /// <summary>
        /// Gets or sets the particle image clipping rectangle
        /// </summary>
        internal Microsoft.Xna.Framework.Rectangle GraphicRect
        {
            get
            {
                if (!_graphicLoaded)
                { return _particleSystem.DefaultGraphicRect; }
                else
                { return _graphicRect; }
            }
            set { _graphicRect = value; }
        }
        #endregion
        #region GraphicOrigin
        /// <summary>
        /// The origin of the emitters particle graphic
        /// </summary>
        private Vector2 _graphicOrigin;

        /// <summary>
        /// Gets or sets the origin of the emitters particle graphic
        /// </summary>
        internal Vector2 GraphicOrigin
        {
            get
            {
                if (!_graphicLoaded)
                { return _particleSystem.DefaultGraphicOrigin; }
                else
                { return _graphicOrigin; }
            }
        }
        #endregion

        #region RenderDepth
        /// <summary>
        /// The rendering depth of the emitter
        /// </summary>
        private float _renderDepth = 0f;

        /// <summary>
        /// Gets or sets the rendering depth of the emitter
        /// </summary>
        [DefaultValue(0f)]
        [Description("The the rendering depth of the emitter")]
        public float RenderDepth
        {
            get { return _renderDepth; }
            set { _renderDepth = value; }
        }
        #endregion

        //===================
        //Methods & Events
        //===================
        #region static Emitter()
        /// <summary>
        /// Static constructor
        /// </summary>
        static Emitter()
        {
            //Seed the random number generator
            _rnd = new Random(Environment.TickCount);
        }
        #endregion
        #region Emitter()
        /// <summary>
        /// default constructor
        /// </summary>
        protected Emitter()
        {
            _activeParticles = new List<Particle>();
            _limbo = new Queue<Particle>();

            _graphicRect = new Microsoft.Xna.Framework.Rectangle(0, 0, 1, 1);
            _graphicOrigin = new Vector2(0, 0);
        }
        #endregion

        #region Initialise
        /// <summary>
        /// Initialises the emitter
        /// </summary>
        public void Initialise()
        {
            _particleSystem.Emitters.Add(this);

            _autoTriggerTicker = new Ticker(_autoTriggerFreq);

            #region Build curves...

            _scaleCurve = new PreCurve(64);
            _scaleCurve.Keys.Add(new CurveKey(0f, _initialScale));
            _scaleCurve.Keys.Add(new CurveKey(_midScaleSweep, _midScale));
            _scaleCurve.Keys.Add(new CurveKey(1f, _finalScale));

            _alphaCurve = new PreCurve(64);
            _alphaCurve.Keys.Add(new CurveKey(0f, _initialAlpha));
            _alphaCurve.Keys.Add(new CurveKey(_midAlphaSweep, _midAlpha));
            _alphaCurve.Keys.Add(new CurveKey(1f, _finalAlpha));

            _redCurve = new PreCurve(64);
            _redCurve.Keys.Add(new CurveKey(0f, (float)_initialColor.R / 255f));
            _redCurve.Keys.Add(new CurveKey(_midColorSweep, (float)_midColor.R / 255f));
            _redCurve.Keys.Add(new CurveKey(1f, (float)_finalColor.R / 255f));

            _greeenCurve = new PreCurve(64);
            _greeenCurve.Keys.Add(new CurveKey(0f, (float)_initialColor.G / 255f));
            _greeenCurve.Keys.Add(new CurveKey(_midColorSweep, (float)_midColor.G / 255f));
            _greeenCurve.Keys.Add(new CurveKey(1f, (float)_finalColor.G / 255f));

            _blueCurve = new PreCurve(64);
            _blueCurve.Keys.Add(new CurveKey(0f, (float)_initialColor.B / 255f));
            _blueCurve.Keys.Add(new CurveKey(_midColorSweep, (float)_midColor.B / 255f));
            _blueCurve.Keys.Add(new CurveKey(1f, (float)_finalColor.B / 255f));

            #endregion

            Position = new Vector2((float)_x, (float)_y);

            _inited = true;
        }
        #endregion

        #region Trigger()
        /// <summary>
        /// Triggers the particles emitter
        /// </summary>
        public void Trigger()
        {
            ParticleInitParams init = new ParticleInitParams();

            for (int i = 0; i < _emitQuantity; i++)
            {
                Emit(ref init);

                if (_limbo.Count > 0)
                {
                    _limbo.Dequeue().Reincarnate(init);
                }
                else
                {
                    _activeParticles.Add(new Particle(this, init));
                }
            }
        }
        #endregion
        #region Emit()
        /// <summary>
        /// Emits a single particle
        /// </summary>
        internal abstract void Emit(ref ParticleInitParams init);
        #endregion

        #region Start()
        /// <summary>
        /// This method is called before the first game loop
        /// </summary>
        public override void Start()
        {
            Initialise();
        }
        #endregion
        #region Update()
        /// <summary>
        /// Updates the emitter
        /// </summary>
        public override void Update()
        {
            float elapsed = (float)Game.ElapsedTime.TotalSeconds;

            for (int i = 0; i < _activeParticles.Count; i++)
            {
                Particle current = _activeParticles[i];
                current.Update(elapsed);
                _activeParticles[i] = current;
            }

            for (int i = 0; i < _activeParticles.Count; i++)
            {
                if (!_activeParticles[i].Alive)
                {
                    _limbo.Enqueue(_activeParticles[i]);
                    _activeParticles.RemoveAt(i);
                    i--;
                }
            }

            if (!_enabled) { return; }

            if (_autoTrigger == true)
            {
                if (_autoTriggerTicker.HasTicked())
                {
                    Trigger();
                }
            }
        }
        #endregion
        #region Render()
        /// <summary>
        /// Renders each of the particle emitters active particles
        /// </summary>
        /// <param name="spriteRenderer">Spritebatch object to render with</param>
        internal void Render(SpriteBatch spriteRenderer)
        {
            foreach (Particle current in _activeParticles)
            {
                current.Render(spriteRenderer);
            }
        }
        #endregion
    }
}