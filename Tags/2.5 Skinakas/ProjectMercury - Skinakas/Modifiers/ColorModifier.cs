using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Utilities;

namespace ProjectMercury.Modifiers
{
    /// <summary>
    /// Represents a modifier that interpolates the color of a particle over its lifetime.
    /// </summary>
    [Serializable]
    public class ColorModifier : Modifier
    {
        /// <summary>
        /// Constructor specifying an initial color and a final color.
        /// </summary>
        /// <param name="initial">The color of the particle upon release.</param>
        /// <param name="final">The color of the particle upon expiry.</param>
        public ColorModifier(Color initial, Color final)
            : this(new Vector4[] { new Vector4(initial.ToVector3(), 0f), new Vector4(final.ToVector3(), 1f) }) { }

        /// <summary>
        /// Constructor specifying an initial color, final color, and sweepable mid color.
        /// </summary>
        /// <param name="initial">The color of the particle upon release.</param>
        /// <param name="mid">The color of the particle upon hitting the mid sweep.</param>
        /// <param name="midSweep">The position of the mid sweep on the curve.</param>
        /// <param name="final">The color of the particle upon expiry.</param>
        public ColorModifier(Color initial, Color mid, float midSweep, Color final)
            : this(new Vector4[] { new Vector4(initial.ToVector3(), 0f), new Vector4(mid.ToVector3(), midSweep), new Vector4(final.ToVector3(), 1f) }) {}

        /// <summary>
        /// Constructor specifying an array of color keys.
        /// </summary>
        /// <param name="keys">The keys of the color curve.</param>
        public ColorModifier(Vector4[] keys)
        {
            this._rCurve = new PreCalcCurve();
            this._gCurve = new PreCalcCurve();
            this._bCurve = new PreCalcCurve();

            for (int i = 0; i < keys.Length; i++)
            {
                Vector4 key = keys[i];

                this._rCurve.Keys.Add(new CurveKey(key.W, key.X));
                this._gCurve.Keys.Add(new CurveKey(key.W, key.Y));
                this._bCurve.Keys.Add(new CurveKey(key.W, key.Z));
            }

            this._rCurve.PreCalculate(512);
            this._gCurve.PreCalculate(512);
            this._bCurve.PreCalculate(512);
        }

        /// <summary>
        /// Called after the particle effect has been deserialized.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        internal override void AfterImport(Game game)
        {
            this._rCurve.PreCalculate(512);
            this._gCurve.PreCalculate(512);
            this._bCurve.PreCalculate(512);
        }

        private PreCalcCurve _rCurve;   //The curve for the red color component.
        private PreCalcCurve _gCurve;   //The curve for the green color component.
        private PreCalcCurve _bCurve;   //The curve for the blue color component.

        /// <summary>
        /// Processes an active particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="elapsedTime">Elapsed game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be processed.</param>
        /// <param name="age">The age of the particle in the range 0 to 1.</param>
        public override void ProcessActiveParticle(float totalTime, float elapsedTime, IParticle particle, float age)
        {
            particle.Color = new Vector3(this._rCurve.Evaluate(age), this._gCurve.Evaluate(age), this._bCurve.Evaluate(age));
        }
    }
}