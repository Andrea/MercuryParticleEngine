using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace ProjectMercury.Utilities
{
    /// <summary>
    /// Represents a pre-calculated curve.
    /// </summary>
    [Serializable]
    public class PreCalcCurve
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PreCalcCurve()
        {
            this._keys = new CurveKeyCollection();
        }

        private CurveKeyCollection _keys;       //The curve keys defining the curve.
        private int _steps;                     //The number of steps which have been pre-calculated.

        [NonSerialized]
        private float[] _data;                  //The pre-calculated data.

        /// <summary>
        /// Gets the control point collection of the curve.
        /// </summary>
        public CurveKeyCollection Keys { get { return this._keys; } }

        /// <summary>
        /// Returns the number of steps that have been pre-calculated.
        /// </summary>
        public int Steps { get { return this._steps; } }

        /// <summary>
        /// Pre-calculates the curve with the specified number of steps.
        /// </summary>
        /// <param name="steps">The number of steps to pre-calculate.</param>
        public void PreCalculate(int steps)
        {
            if (steps < 2) { throw new ArgumentOutOfRangeException("steps"); }

            this._steps = steps;

            this._data = new float[this._steps];

            Curve curve = this.BuildXNACurve();

            for (int i = 0; i < this._steps; i++)
            {
                float position = (1f / (float)this._steps) * (float)i;

                this._data[i] = curve.Evaluate(position);
            }
        }

        /// <summary>
        /// Evaluates a position on the curve.
        /// </summary>
        /// <param name="position">The position on the curve.</param>
        /// <returns>The curve value at the specified position.</returns>
        public float Evaluate(float position)
        {
            int index = (int)(position * this._steps);

            return this._data[index];
        }

        /// <summary>
        /// Builds an XNA curve object from the control points.
        /// </summary>
        /// <returns></returns>
        private Curve BuildXNACurve()
        {
            Curve curve = new Curve();

            for(int i = 0; i < this._keys.Count; i++)
            {
                curve.Keys.Add(this._keys[i]);
            }

            return curve;
        }
    }
}
