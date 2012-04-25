using System;
using Microsoft.Xna.Framework;
using ProjectMercury.Utilities;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents a modifier that interpolates the opacity of a particle over its
    /// lifetime.
    /// </summary>
    [Serializable]
    public class OpacityModifier : Modifier
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="initial">The initial opacity of the particle upon its release.</param>
        /// <param name="final">The final opacity of the particle upon its expiry.</param>
        public OpacityModifier(float initial, float final)
            : this(new Vector2[] { new Vector2(0f, initial), new Vector2(1f, final) }) { }

        /// <summary>
        /// Constructor speciying a middle sweep value.
        /// </summary>
        /// <param name="initial">The initial opacity of the particle upon its release.</param>
        /// <param name="mid">The opacity of the particle upon reaching the mid sweep age.</param>
        /// <param name="midSweep">The age at which the particle will reach mid opacity.</param>
        /// <param name="final">The final opacity of the particle upon its expiry.</param>
        public OpacityModifier(float initial, float mid, float midSweep, float final)
            : this(new Vector2[] { new Vector2(0f, initial), new Vector2(midSweep, mid), new Vector2(1f, final) }) { }

        /// <summary>
        /// Constructor specifying an array of opacity keys. The X component of the vector specifies
        /// the position on the curve, the Y component specifies the value at that position.
        /// </summary>
        /// <param name="keys">An array of keys defining the curve.</param>
        public OpacityModifier(Vector2[] keys)
        {
            this._aCurve = new PreCalcCurve();

            for (int i = 0; i < keys.Length; i++)
            {
                Vector2 key = keys[i];

                this._aCurve.Keys.Add(new CurveKey(key.X, key.Y));
            }

            this._aCurve.PreCalculate(512);
        }

        /// <summary>
        /// Called after the particle effect has been deserialized.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        internal override void AfterImport(Game game)
        {
            this._aCurve.PreCalculate(512);
        }

        private PreCalcCurve _aCurve;   //Opacity curve.

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be procesed.</param>
        /// <param name="age">The age of the particle in the range 0 to 1.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, ref Particle particle, float age)
        {
            particle.Opacity = this._aCurve.Evaluate(age);
        }
    }
}