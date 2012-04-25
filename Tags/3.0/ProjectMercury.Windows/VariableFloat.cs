namespace ProjectMercury
{
    using System.ComponentModel;

#if WINDOWS
    [TypeConverter("ProjectMercury.Design.VariableFloatTypeConverter, ProjectMercury.Design")]
#endif
    public struct VariableFloat
    {
        public float Anchor;
        public float Variation;

        /// <summary>
        /// Gets the randomised value if the VariableFloat.
        /// </summary>
        public float Value
        {
            get
            {
                if (this.Variation <= float.Epsilon) { return this.Anchor; }

                return RandomHelper.Variation(this.Anchor, this.Variation);
            }
        }

        /// <summary>
        /// Implicit cast operator from float to VariableFloat.
        /// </summary>
        static public implicit operator VariableFloat(float value)
        {
            return new VariableFloat
            {
                Anchor    = value,
                Variation = 0f
            };
        }

        /// <summary>
        /// Implicit cast operation from VariableFloat to float.
        /// </summary>
        static public implicit operator float(VariableFloat value)
        {
            return value.Value;
        }
    }
}