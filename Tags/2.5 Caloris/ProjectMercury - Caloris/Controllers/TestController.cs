using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;

namespace ProjectMercury.Controllers
{
    [Serializable]
    public class TestController : Controller
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="frequency">Frequency at which the emitters shall be triggered, in whole and fractional seconds.</param>
        public TestController(Game game, float frequency)
        {
            if (game == null) { throw new ArgumentNullException("game"); }

            this._game = game;
            this._lastTrigger = 0;
            this._frequency = frequency;
        }

        /// <summary>
        /// Called after the particle effect has been imported.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        public override void AfterImport(Game game)
        {
            this._game = game;

            base.AfterImport(game);
        }

        [NonSerialized]
        private Game _game;         //Reference to the game.

        [NonSerialized]
        private float _lastTrigger; //Time when the emittters were last triggered.

        private float _frequency;   //Frequency to trigger the emitters.

        private Emitter _emitter;

        public Emitter Emitter
        {
            get { return this._emitter; }
            set { this._emitter = value; }
        }

        /// <summary>
        /// Updates the controller.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        public override void Update(float totalTime, float elapsedTime)
        {
            if (this._game.IsActive)
            {
                MouseState state = Mouse.GetState();

                if (state.LeftButton == ButtonState.Pressed)
                {
                    if (this._lastTrigger + this._frequency < totalTime)
                    {
                        Vector2 position = new Vector2((float)state.X, (float)state.Y);

                        this._emitter.Trigger(ref position);

                        this._lastTrigger = this._lastTrigger + this._frequency;
                    }
                }
            }
        }
    }
}
