using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Mercury
{
    struct Particle
    {
        //===================
        //Fields & Properties
        //===================
        #region Emitter
        /// <summary>
        /// The emitter that spawned this particle
        /// </summary>
        private Emitter _emitter;

        /// <summary>
        /// Sets the emitter that spawned this particle
        /// </summary>
        public Emitter Emitter
        {
            set { _emitter = value; }
        }
        #endregion

        #region CreationTime
        /// <summary>
        /// The system time when the particle was created
        /// </summary>
        private int _creationTime;
        #endregion
        #region Lifespan
        private uint _lifespan;
        #endregion
        #region Age
        /// <summary>
        /// Gets the age of the particle in a range of 0 - 1
        /// </summary>
        private float Age
        {
            get
            {
                //Calculate the age of the particle
                float age = (float)(Environment.TickCount - _creationTime);
                return age / (float)_lifespan;
            }
        }
        #endregion
        #region Alive
        /// <summary>
        /// Flag true if the particle is alive
        /// </summary>
        private bool _alive;

        /// <summary>
        /// Returns true if the particle is alive
        /// </summary>
        internal bool Alive
        {
            get { return _alive; }
        }
        #endregion

        #region BaseRotation
        /// <summary>
        /// The initial rotation of the particle
        /// </summary>
        private float _baseRotation;
        #endregion
        #region AdditionalRotation
        /// <summary>
        /// The additional rotation of the particle
        /// </summary>
        private float _additionalRotation;
        #endregion
        #region TotalRotation
        /// <summary>
        /// Gets the total rotation of the particle
        /// </summary>
        public float TotalRotation
        {
            get { return _baseRotation + _additionalRotation; }
        }
        #endregion

        #region Position
        /// <summary>
        /// The position of the particle
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Gets the position of the particle
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
        }
        #endregion
        #region Speed
        /// <summary>
        /// The speed at which the particle is moving
        /// </summary>
        private float _speed;

        /// <summary>
        /// Gets or sets the speed of the particle
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        #endregion
        #region Angle
        /// <summary>
        /// The angle at which the particle is moving
        /// </summary>
        private float _angle;
        #endregion
        #region Scale
        /// <summary>
        /// The current scale of the particle
        /// </summary>
        private float _scale;

        /// <summary>
        /// Gets the current scale of the particle
        /// </summary>
        public float Scale
        {
            get { return _scale; }
        }
        #endregion
        #region Color
        /// <summary>
        /// The current color of the particle as a vector4
        /// </summary>
        private Vector4 _color;

        /// <summary>
        /// Gets the color of the particle as an Xna.Graphics.Color
        /// </summary>
        public Color Color
        {
            get { return new Color(_color); }
        }
        #endregion

        //===================
        //Methods & Events
        //===================
        #region Particle()
        /// <summary>
        /// Default constructor
        /// </summary>
        public Particle( Emitter parent, ParticleInitParams init)
        {
            _emitter = parent;

            _creationTime = Environment.TickCount;
            _lifespan = _emitter.ParticleLifespanVariated;

            _baseRotation = init.Rotation;
            _additionalRotation = 0f;

            _position = init.Position;
            _speed = _emitter.ParticleSpeedVariated;
            _angle = init.Angle;
            _scale = _emitter.InitialScale;
            _color = new Vector4();
            //_color = new Vector4(
            //    _emitter.StartingAlpha, _emitter.StartingRed,
            //    _emitter.StartingGreen, _emitter.StartingBlue);

            _alive = true;
            _emitter.ActiveParticles.Add(this);
        }
        #endregion
        #region Reincarnate()
        /// <summary>
        /// Reincarnates the particle with new properties
        /// </summary>
        public void Reincarnate( ParticleInitParams init)
        {
            _creationTime = Environment.TickCount;
            _lifespan = _emitter.ParticleLifespanVariated;

            _baseRotation = init.Rotation;
            _additionalRotation = 0f;

            _position = init.Position;
            _speed = _emitter.ParticleSpeedVariated;
            _angle = init.Angle;
            _scale = _emitter.InitialScale;
            _color = new Vector4();
            //_color = new Vector4(
            //    _emitter.StartingAlpha, _emitter.StartingRed,
            //    _emitter.StartingGreen, _emitter.StartingBlue);

            _alive = true;
            _emitter.ActiveParticles.Add(this);
        }
        #endregion

        #region Update()
        /// <summary>
        /// Updates the particles
        /// </summary>
        /// <param name="elapsed">Delta time</param>
        internal void Update(float elapsed)
        {
            float age = Age;

            if (age >= 1f)
            {
                _alive = false;
                return;
            }

            _additionalRotation = MathHelper.Lerp(0f, _emitter.Revolutions, age) * MathHelper.TwoPi;

            _speed += (_emitter.ParticleSpeedDelta * elapsed);
            _speed = MathHelper.Clamp(_speed, _emitter.ParticleSpeedMin, _emitter.ParticleSpeedMax);

            _scale = _emitter.ScaleCurve.Evaluate(age);

            _color.W = _emitter.AlphaCurve.Evaluate(age);
            _color.X = _emitter.RedCurve.Evaluate(age);
            _color.Y = _emitter.GreenCurve.Evaluate(age);
            _color.Z = _emitter.BlueCurve.Evaluate(age);

            _position.X += ((float)Math.Sin(_angle) * _speed) * elapsed;
            _position.Y += ((float)Math.Cos(_angle) * _speed) * elapsed;
        }
        #endregion
        #region Render
        public void Render(SpriteBatch spriteRenderer)
        {
            spriteRenderer.Draw(
                _emitter.Graphic,
                Position,
                _emitter.GraphicRect,
                Color,
                TotalRotation,
                _emitter.GraphicOrigin,
                Scale,
                SpriteEffects.None,
                _emitter.RenderDepth);
        }
        #endregion
    }

    struct ParticleInitParams
    {
        #region Position
        /// <summary>
        /// The screen position where the particle will spawn
        /// </summary>
        public Vector2 Position;
        #endregion
        #region Angle
        /// <summary>
        /// The initial angle the particle will be moving in
        /// </summary>
        public float Angle;
        #endregion
        #region Rotation
        /// <summary>
        /// The initial rotation of the particle
        /// </summary>
        public float Rotation;
        #endregion
    }
}