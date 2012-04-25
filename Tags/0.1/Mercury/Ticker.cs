using System;

namespace Mercury
{
    /// <summary>
    /// A simple timer that 'ticks' at a specified frequency
    /// </summary>
    class Ticker
    {
        /// <summary>Frequency of the ticker</summary>
        private uint _frequency;

        /// <summary>ParticleSystem time of the last tick</summary>
        private uint _lastTick;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="frequency">frequency of the ticker</param>
        public Ticker(uint frequency)
        {
            Frequency = frequency;

            //Mark the last tick as the current system time to avoid an
            //immediate tick
            _lastTick = (uint)Environment.TickCount;
        }

        /// <summary>
        /// Gets or sets the frequency of a ticker
        /// </summary>
        public uint Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        /// <summary>
        /// Check to see if the ticker has ticked since the last call to this method
        /// </summary>
        /// <returns>True if the ticker has 'ticked'</returns>
        public bool HasTicked()
        {
            uint time = (uint)Environment.TickCount;

            if (time > (_lastTick + _frequency))
            {   //The ticker has ticked!
                _lastTick = time;
                return true;
            }
            else
            {   //The ticker hasn't ticked yet
                return false;
            }
        }
    }
}
