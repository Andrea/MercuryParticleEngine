using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Emitters;

namespace ProjectMercury.Renderers
{
    /// <summary>
    /// Represents the abstract base class for particle effect renderers.
    /// </summary>
    [Serializable]
    abstract public class Renderer
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="game">Game reference.</param>
        /// <param name="graphics">The games graphics device service.</param>
        public Renderer(Game game, IGraphicsDeviceService graphics)
            : this(game, graphics, new ContentManager(game.Services)) { }

        /// <summary>
        /// Constructor passing a reference to a ContentManager.
        /// </summary>
        /// <param name="game">Game reference.</param>
        /// <param name="graphics">The games graphics device service.</param>
        /// <param name="content">A ContentManager for loading textures & effects.</param>
        public Renderer(Game game, IGraphicsDeviceService graphics, ContentManager content)
        {
            this._game = game;
            this._graphics = graphics;
            this._content = content;
        }

        /// <summary>
        /// Called after the particle effect has been imported.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        public virtual void AfterImport(Game game)
        {
            this._game = game;
            this._graphics = game.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            this._content = new ContentManager(game.Services);
        }

        [NonSerialized]
        private Game _game;                         //Reference to the game.

        [NonSerialized]
        private IGraphicsDeviceService _graphics;   //Reference to the graphics device manager.

        [NonSerialized]
        private ContentManager _content;            //Reference to an internal or external content manager.

        [NonSerialized]
        private Texture2D _default;                 //Default texture used to render particles.

        /// <summary>
        /// Gets the games graphics device service.
        /// </summary>
        protected IGraphicsDeviceService Graphics
        {
            get { return this._graphics; }
        }

        /// <summary>
        /// Gets a reference to the content manager.
        /// </summary>
        protected ContentManager Content
        {
            get { return this._content; }
        }

        /// <summary>
        /// Gets a reference to the default texture for rendering particles.
        /// </summary>
        protected Texture2D DefaultTexture
        {
            get { return this._default; }
        }

        /// <summary>
        /// Draws an emitter.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="emitter">The emitter to be drawn.</param>
        internal void Draw(float totalTime, Emitter<Particle> emitter)
        {
            this.Prepare(totalTime, emitter);

            for (int i = 0; i < emitter.ActiveParticlesCount; i++)
            {
                IParticle particle = emitter.Get(i);

                this.RenderParticle(totalTime, particle);
            }

            this.PostRender(totalTime);
        }

        /// <summary>
        /// Loads content required by the renderer.
        /// </summary>
        public virtual void LoadContent()
        {
            this._default = this._content.Load<Texture2D>("particle");
        }

        /// <summary>
        /// Unloads content required by the renderer.
        /// </summary>
        public virtual void UnloadContent()
        {
            this._content.Unload();
        }

        /// <summary>
        /// Begins rendering of an emitter, called before any particles are drawn.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="emitter">The emitter which is about to be rendered.</param>
        protected virtual void Prepare(float totalTime, Emitter<Particle> emitter) { }

        /// <summary>
        /// Draws a single particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be drawn.</param>
        protected virtual void RenderParticle(float totalTime, IParticle particle) { }

        /// <summary>
        /// Finishes rendering of an emitter, called after all the particles are drawn.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        protected virtual void PostRender(float totalTime) { }
    }
}