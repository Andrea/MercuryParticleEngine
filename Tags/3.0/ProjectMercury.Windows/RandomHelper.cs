namespace ProjectMercury
{
    using System;

    static internal class RandomHelper
    {
        static RandomHelper()
        {
            Random = new Random(Environment.TickCount);
        }

        static private Random Random;

        /// <summary>
        /// Returns a non-negetive random whole number.
        /// </summary>
        static public int NextInt()
        {
            return Random.Next();
        }

        /// <summary>
        /// Returns a non-negetive random whole number less than the specified maximum.
        /// </summary>
        /// <param name="max">The exclusive upper bound the random number to be generated.</param>
        static public int NextInt(int max)
        {
            return Random.Next(max);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The exclusive upper bound of the random number returned.</param>
        static public int NextInt(int min, int max)
        {
            return Random.Next(min, max);
        }

        /// <summary>
        /// Returns a random float between 0.0 and 1.0.
        /// </summary>
        static public float NextFloat()
        {
            return (float)Random.NextDouble();
        }

        /// <summary>
        /// Returns a random float betwen 0.0 and the specified upper bound.
        /// </summary>
        /// <param name="max">The inclusive upper bound of the random number returned.</param>
        static public float NextFloat(float max)
        {
            return max * NextFloat();
        }

        /// <summary>
        /// Returns a random float within the specified range.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The inclusive upper bound of the random number returned.</param>
        static public float NextFloat(float min, float max)
        {
            return ((max - min) * NextFloat()) + min;
        }

        /// <summary>
        /// Returns a random boolean value.
        /// </summary>
        static public bool NextBool()
        {
            return NextInt(2) == 1;
        }

        /// <summary>
        /// Returns a random variation of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variation">The variation multiple of the value.</param>
        /// <example>a value of 10 with a variation of 0.5 will result in a random number between 5.0 and 15.</example>
        static public float Variation(float value, float variation)
        {
            float min = value - (variation * value),
                  max = value + (variation * value);

            return NextFloat(min, max);
        }

        /// <summary>
        /// Chooses a random item from the specified parameters and returns it.
        /// </summary>
        static public int Choose(params int[] values)
        {
            int index = RandomHelper.NextInt(values.Length);

            return values[index];
        }

        /// <summary>
        /// Chooses a random item from the specified parameters and returns it.
        /// </summary>
        static public float Choose(params float[] values)
        {
            int index = RandomHelper.NextInt(values.Length);

            return values[index];
        }

        /// <summary>
        /// Chooses a random item from the specified parameters and returns it.
        /// </summary>
        static public T Choose<T>(params T[] values)
        {
            int index = RandomHelper.NextInt(values.Length);

            return values[index];
        }
    }
}