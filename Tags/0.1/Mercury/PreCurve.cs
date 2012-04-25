using Microsoft.Xna.Framework;

namespace Mercury
{
    /// <summary>
    /// Extends the Xna.Framework.Curve class and adds functionality to pre-calculate
    /// the curve with a variable number of samples
    /// </summary>
    public class PreCurve : Curve
    {
        //===================
        //Fields & Properties
        //===================
        #region Samples
        /// <summary>
        /// Number of samples in the curve
        /// </summary>
        private byte _samples;
        #endregion
        #region Data
        /// <summary>
        /// Pre-calculated data
        /// </summary>
        private float[] _data;
        #endregion
        #region Keys
        /// <summary>
        /// Gets the CurveKeyCollection for this PreCurve
        /// </summary>
        public new CurveKeyCollection Keys
        {
            get
            {   //Looks like the curve is about to be adjusted, flush the pre-calc data!
                Flush();
                return base.Keys;
            }
        }
        #endregion

        //===================
        //Methods & Events
        //===================
        #region PreCurve()
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="samples">Number of curve samples</param>
        public PreCurve(byte samples)
            : base()
        {
            _samples = samples;
            _data = new float[samples +1];

            Flush();
        }
        #endregion

        #region Evaluate()
        /// <summary>
        /// Evaluates a position on the curve
        /// </summary>
        /// <param name="position">The position on the curve</param>
        /// <returns>
        /// The curve value at the sample closest to the specified position
        /// </returns>
        public new float Evaluate(float position)
        {
            //Simulate CurveLoopType.Constant Pre & Post loop
            if (position < 0f) { position = 0f; }
            if (position > 1f) { position = 1f; }

            //Get the closest sample to the specified curve position
            byte sample = (byte)(position * _samples);

            if (_data[sample] != -1)
            {   //This sample has already been calculated, so use that data
                return _data[sample];
            }
            else
            {   //This sample has not been calculated, calculate it now
                float value = base.Evaluate(position);

                //Add the result to the data array
                _data[sample] = value;

                return value;
            }
        }
        #endregion
        #region PreCalculate()
        /// <summary>
        /// Pre-calculates the curve
        /// </summary>
        /// <remarks>Not intended for real-time use</remarks>
        public void PreCalculate()
        {
            float position;

            for (byte sample = 0; sample < _samples; sample++)
            {
                //Gets this samples position on the curve
                position = (float)sample / (float)_samples;

                //Calculate this sample & add it to the data array
                _data[sample] = base.Evaluate(position);
            }
        }
        #endregion
        #region Flush()
        /// <summary>
        /// Flushes all pre-calculated data from the curve
        /// </summary>
        private void Flush()
        {
            for (ushort i = 0; i < _samples; i++)
            {
                _data[i] = -1;
            }
        }
        #endregion
    }
}