using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using ProjectMercury.Controllers;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;

namespace ProjectMercury
{
    [Serializable]
    public class ParticleEffect : DrawableGameComponent, ISerializable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        public ParticleEffect(Game game) : this(game, null) { }

        /// <summary>
        /// Constructor which specifies a renderer.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        /// <param name="renderer">The renderer used to render the effect.</param>
        public ParticleEffect(Game game, Renderer renderer) : base(game)
        {
            game.Components.Add(this);
            this._renderer = renderer;
        }

        /// <summary>
        /// Deserialization constructor.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        public ParticleEffect(SerializationInfo info, StreamingContext context)
            : base(MercuryLoader.GameReference)
        {
            this._controllers = info.GetValue("_controllers", typeof(List<Controller>)) as List<Controller>;
            this._emitters = info.GetValue("_emitters", typeof(List<Emitter>)) as List<Emitter>;
            this._renderer = info.GetValue("_renderer", typeof(Renderer)) as Renderer;
        }

        /// <summary>
        /// Serialization method.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_controllers", this._controllers);
            info.AddValue("_emitters", this._emitters);
            info.AddValue("_renderer", this._renderer);
        }

        /// <summary>
        /// Called after the effect is successfully deserialized.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        public void AfterImport(Game game)
        {
            game.Components.Add(this);

            //base.Game = game;

            for (int i = 0; i < this._controllers.Count; i++)
                this._controllers[i].AfterImport(game);

            for (int i = 0; i < this._emitters.Count; i++)
                this._emitters[i].AfterImport(game);

            this._renderer.AfterImport(game);
        }

        private List<Controller> _controllers = new List<Controller>(); //Controllers which are part of the effect.
        private List<Emitter> _emitters = new List<Emitter>();          //Emitters which are part of the effect.
        private Renderer _renderer = null;                              //Renderer used to draw the effect.
        private Stopwatch _uTimer = new Stopwatch();                    //Stopwatch used to measure update time.
        private Stopwatch _rTimer = new Stopwatch();                    //Stopwatch used to measure rendering time.
        private double _updateTime;                                     //Time taken in the last update.
        private double _renderTime;                                     //Time taken to render the last frame.
        

        /// <summary>
        /// Gets the list of controllers that are part of the particle effect.
        /// </summary>
        public IList<Controller> Controllers
        {
            get { return this._controllers; }
        }

        /// <summary>
        /// Gets the list of emitters which are part of the particle effect.
        /// </summary>
        public IList<Emitter> Emitters
        {
            get { return this._emitters; }
        }

        /// <summary>
        /// Gets or sets the renderer used to render the particle effect.
        /// </summary>
        public Renderer Renderer
        {
            get { return this._renderer; }
            set { this._renderer = value; }
        }

        /// <summary>
        /// Gets the time taken to update the effect in whole and fractional seconds.
        /// </summary>
        public double UpdateTime
        {
            get { return this._updateTime; }
        }

        /// <summary>
        /// Gets the time taken to render the effect in whole and fractional seconds.
        /// </summary>
        public double RenderTime
        {
            get { return this._renderTime; }
        }

        /// <summary>
        /// Loads content required by the particle effect.
        /// </summary>
        /// <param name="loadAllContent">True to load unmanaged resources.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
                if (this._renderer != null)
                    this._renderer.LoadContent();

            base.LoadGraphicsContent(loadAllContent);
        }

        /// <summary>
        /// Unloads content required by the particle effect.
        /// </summary>
        /// <param name="unloadAllContent">True to unload unmanaged resources.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
                if (this._renderer != null)
                    this._renderer.UnloadContent();

            base.UnloadGraphicsContent(unloadAllContent);
        }

        /// <summary>
        /// Updates the particle effect.
        /// </summary>
        /// <param name="gameTime">Game timing information.</param>
        public override void Update(GameTime gameTime)
        {
            float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Start the update timer...
            this._uTimer.Start();

            //Update controllers...
            for (int i = 0; i < this._controllers.Count; i++)
                this._controllers[i].Update(totalTime, elapsedTime);

            //Update emitters...
            for (int i = 0; i < this._emitters.Count; i++)
                this._emitters[i].Update(totalTime, elapsedTime);

            //Capture the time taken to do the update...
            this._updateTime = this._uTimer.Elapsed.TotalSeconds;
            this._uTimer.Stop(); this._uTimer.Reset();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the particle effect.
        /// </summary>
        /// <param name="gameTime">Game timing information.</param>
        public override void Draw(GameTime gameTime)
        {
            float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

            //Start the render timer...
            this._rTimer.Start();

            //Render emitters...
            if (this._renderer != null)
                for (int i = 0; i < this._emitters.Count; i++)
                    this._renderer.Draw(totalTime, this._emitters[i]);

            //Capture the time taken to render the frame...
            this._renderTime = this._rTimer.Elapsed.TotalSeconds;
            this._rTimer.Stop(); this._rTimer.Reset();
        }
    }
}
