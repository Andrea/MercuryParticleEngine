using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;

namespace ProjectMercury.Controllers
{
    [Serializable]
    abstract public class Controller
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Controller() { }

        /// <summary>
        /// Called after the particle effect has been imported.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        public virtual void AfterImport(Game game) { }

        /// <summary>
        /// Updates the controller.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        public virtual void Update(float totalTime, float elapsedTime) { }
    }
}
