using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using System.ComponentModel;

namespace Mercury
{
    public class ParticleSystem : GameComponent
    {
        //===================
        //Fields & Properties
        //===================
        #region GraphicsService
        private IGraphicsDeviceService _graphicsService;
        #endregion
        #region SpriteRenderer
        private SpriteBatch _spriteRenderer;
        #endregion

        #region BlendMode
        private SpriteBlendMode _blend = SpriteBlendMode.AlphaBlend;

        [DefaultValue(SpriteBlendMode.AlphaBlend)]
        public SpriteBlendMode BlendMode
        {
            get { return _blend; }
            set { _blend = value; }
        }
        #endregion
        #region DefaultGraphic
        /// <summary>
        /// Default image used to render particles
        /// </summary>
        private Texture2D _defaultGraphic;

        /// <summary>
        /// Origin of the default particle graphic
        /// </summary>
        private Vector2 _defaultOrigin;

        /// <summary>
        /// Clipping rectangle of the default particle graphic
        /// </summary>
        private Rectangle _defaultRect;

        /// <summary>
        /// Gets the default image used to render particles
        /// </summary>
        internal Texture2D DefaultGraphic
        {
            get { return _defaultGraphic; }
        }

        /// <summary>
        /// Gets the origin of the default particle image
        /// </summary>
        internal Vector2 DefaultGraphicOrigin
        {
            get { return _defaultOrigin; }
        }

        /// <summary>
        /// Gets the clipping rectangle of the default particle image
        /// </summary>
        internal Rectangle DefaultGraphicRect
        {
            get { return _defaultRect; }
        }
        #endregion

        #region Emitters
        private List<Emitter> _emitters;

        internal List<Emitter> Emitters
        {
            get { return _emitters; }
        }
        #endregion

        //===================
        //Methods & Events
        //===================
        #region ParticleSystem()
        /// <summary>
        /// Default constructor
        /// </summary>
        public ParticleSystem()
        {
        }
        #endregion

        #region Start()
        /// <summary>
        /// Initialises the particle system
        /// </summary>
        public override void Start()
        {
            //Get the games graphics service
            _graphicsService = Game.GameServices.GetService<IGraphicsDeviceService>();

            //Create our sprite renderer
            _spriteRenderer = new SpriteBatch(_graphicsService.GraphicsDevice);

            //Load the default particle graphic
            _defaultGraphic = Texture2D.FromFile(_graphicsService.GraphicsDevice, @"Resources\DefaultParticle.dds");

            //Set the origin & clipping rectangle of the default particle graphic
            _defaultOrigin = new Vector2(_defaultGraphic.Width / 2f, _defaultGraphic.Height / 2f);
            _defaultRect = new Rectangle(0, 0, _defaultGraphic.Width, _defaultGraphic.Height);

            //Create the list of emitters
            _emitters = new List<Emitter>();
        }
        #endregion
        #region Update()
        /// <summary>
        /// Updates the particle system
        /// </summary>
        public override void Update()
        {
            base.Update();
        }
        #endregion
        #region Draw()
        /// <summary>
        /// Renders the particle system
        /// </summary>
        public override void Draw()
        {
            _spriteRenderer.Begin(_blend);

            foreach (Emitter current in _emitters)
            {
                current.Render(_spriteRenderer);
            }

            _spriteRenderer.End();
        }
        #endregion
    }
}
