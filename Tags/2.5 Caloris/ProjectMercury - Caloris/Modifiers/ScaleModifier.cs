using System;
using Microsoft.Xna.Framework;
using ProjectMercury.Utilities;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents a modifier which interpolates the scale of a particle throughout its
    /// lifetime.
    /// </summary>
    [Serializable]
    public class ScaleModifier : Modifier
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="initial">The initial scale of the particle in pixels upon its release.</param>
        /// <param name="final">The final scale of the particle in pixels upon its expiry.</param>
        public ScaleModifier(float initial, float final)
            : this(new Vector2[] { new Vector2(0f, initial), new Vector2(1f, final) }) { }

        /// <summary>
        /// Constructor specifying a middle scale value.
        /// </summary>
        /// <param name="initial">The initial scale of the particle in pixels upon its release.</param>
        /// <param name="mid">The middle scale of the particle.</param>
        /// <param name="midSweep">The age at which the particle shall reach middle scale.</param>
        /// <param name="final">The final scale of the particle in pixels upon its expiry.</param>
        public ScaleModifier(float initial, float mid, float midSweep, float final)
            : this(new Vector2[] { new Vector2(0f, initial), new Vector2(midSweep, mid), new Vector2(1f, final) }) { }

        /// <summary>
        /// Constructor specifying an array of keys defining the scale curve.
        /// </summary>
        /// <param name="keys">An array of Vector2 objects defining the curve. The X component of the
        /// vector specifies the position on the curve, the Y component specifies the value at that
        /// position.</param>
        public ScaleModifier(Vector2[] keys)
        {
            this._sCurve = new PreCalcCurve();

            for (int i = 0; i < keys.Length; i++)
            {
                Vector2 key = keys[i];

                this._sCurve.Keys.Add(new CurveKey(key.X, key.Y));
            }

            this._sCurve.PreCalculate(512);
        }

        /// <summary>
        /// Called after the particle effect has been deserialized.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        internal override void AfterImport(Game game)
        {
            this._sCurve.PreCalculate(512);
        }

        private PreCalcCurve _sCurve;   //Scale curve.

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be processed.</param>
        /// <param name="age">The age of the particle in the range 0 - 1.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, ref Particle particle, float age)
        {
            particle.Scale = this._sCurve.Evaluate(age);
        }
    }
}