using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sanford.Multimedia.Midi
{
    public sealed class PpqnTicker : IEnumerator<int>
    {
        /// <summary>
        /// The default tempo in microseconds: 120bpm.
        /// </summary>
        public const int DefaultTempo = 500000;

        /// <summary>
        /// The minimum pulses per quarter note value.
        /// </summary>
        public const int PpqnMinValue = 24;

        // The number of microseconds per millisecond.
        private const int MicrosecondsPerMillisecond = 1000;

        // The pulses per quarter note value.
        private int ppqn = PpqnMinValue;

        // The tempo in microseconds.
        private int tempo = DefaultTempo;

        private int resolution = 1;

        private int increment;

        private int accumulator;

        private int ticks;

        private bool disposed = false;

        public PpqnTicker()
        {            
            Initialize();
            Reset();
        }

        private void Initialize()
        {
            Debug.Assert(!disposed);

            increment = ppqn * resolution * MicrosecondsPerMillisecond;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the tempo in microseconds.
        /// </summary>
        public int Tempo
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                #endregion

                return tempo;
            }
            set
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                } 
                else if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Tempo");
                }

                #endregion

                tempo = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of pulses (ticks) per quarter note.
        /// </summary>
        public int Ppqn
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                #endregion

                return ppqn;
            }
            set
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                } 
                else if(value < PpqnMinValue)
                {
                    throw new ArgumentOutOfRangeException("Ppqn");
                }
                else if(value % PpqnMinValue != 0)
                {
                    throw new ArgumentException("PPQN value must be a multiple of 24");
                }

                #endregion

                ppqn = value;

                Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the resolution at which the ticks are generated.
        /// </summary>
        public int Resolution
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                #endregion

                return resolution;
            }
            set
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                } 
                else if(value < 1)
                {
                    throw new ArgumentOutOfRangeException("Resolution");
                }

                #endregion

                resolution = value;

                Initialize();
            }
        }

        #endregion

        #region IEnumerator<int> Members

        public int Current
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                #endregion

                return ticks;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            disposed = true;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
                else if(ticks < 0)
                {
                    throw new InvalidOperationException("The enumerator is positioned before the first tick.");
                }

                #endregion

                return ticks;
            }
        }

        public bool MoveNext()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            ticks = 0;

            accumulator += increment;

            while(accumulator >= tempo)
            {
                accumulator -= tempo;

                ticks++;
            }

            return true;
        }

        public void Reset()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            accumulator = 0;
            ticks = -1;
        }

        #endregion
    }
}
