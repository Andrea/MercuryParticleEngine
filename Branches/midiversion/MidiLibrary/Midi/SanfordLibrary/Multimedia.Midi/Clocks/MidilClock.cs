#region License

/* Copyright (c) 2006 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Sanford.Multimedia.Timers;

namespace Sanford.Multimedia.Midi
{
	/// <summary>
	/// Generates clock events internally.
	/// </summary>
	public sealed class MidiClock : IClock, IComponent
    {
        #region MidiClock Members

        #region Fields

        // Used for generating tick events.
        private Timer timer = new Timer();

        // Parses meta message tempo change messages.
        private TempoChangeBuilder builder = new TempoChangeBuilder();

        private PpqnTicker ticker = new PpqnTicker();

        private bool running = false;

        // Indicates whether the clock has been disposed.
        private bool disposed = false;

        private ISite site = null;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the MidiInternalClock class.
        /// </summary>
		public MidiClock()
		{
            timer.Period = (10 > Timer.Capabilities.periodMin ? 10 : Timer.Capabilities.periodMin);
            timer.Tick += new EventHandler(HandleTick);
            ticker.Resolution = timer.Period;
        }

        public MidiClock(int timerPeriod)
        {
            timer.Period = timerPeriod;
            timer.Tick += new EventHandler(HandleTick);
            ticker.Resolution = timer.Period;
        }

        /// <summary>
        /// Initializes a new instance of the MidiInternalClock class with the 
        /// specified IContainer.
        /// </summary>
        /// <param name="container">
        /// The IContainer to which the MidiInternalClock will add itself.
        /// </param>
        public MidiClock(IContainer container)
        {
            ///
            /// Required for Windows.Forms Class Composition Designer support
            ///
            container.Add(this);

            timer.Period = (10 > Timer.Capabilities.periodMin ? 10 : Timer.Capabilities.periodMin);
            timer.Tick += new EventHandler(HandleTick);
            ticker.Resolution = timer.Period;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the MidiInternalClock.
        /// </summary>
        public void Start()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException("MidiInternalClock");
            }

            #endregion

            #region Guard

            if(running)
            {
                return;
            }

            #endregion

            ticker.Reset();

            OnStarted(EventArgs.Empty);

            // Start the multimedia timer in order to start generating ticks.
            timer.Start();

            // Indicate that the clock is now running.
            running = true;           
            
        }

        /// <summary>
        /// Resumes tick generation from the current position.
        /// </summary>
        public void Continue()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException("MidiInternalClock");
            }

            #endregion

            #region Guard

            if(running)
            {
                return;
            }

            #endregion

            // Raise Continued event.
            OnContinued(EventArgs.Empty);

            // Start multimedia timer in order to start generating ticks.
            timer.Start();

            // Indicate that the clock is now running.
            running = true;            
        }

        /// <summary>
        /// Stops the MidiInternalClock.
        /// </summary>
        public void Stop()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException("MidiInternalClock");
            }

            #endregion

            #region Guard

            if(!running)
            {
                return;
            }

            #endregion

            // Stop the multimedia timer.
            timer.Stop();

            // Indicate that the clock is not running.
            running = false;

            OnStopped(EventArgs.Empty);
        }

        public void Process(MetaMessage message)
        {
            #region Require

            if(message == null)
            {
                throw new ArgumentNullException("message");
            }

            #endregion

            #region Guard

            if(message.MetaType != MetaType.Tempo)
            {
                return;
            }

            #endregion

            TempoChangeBuilder builder = new TempoChangeBuilder(message);

            // Set the new tempo.
            Tempo = builder.Tempo;
        }

        #region Event Raiser Methods

        private void OnDisposed(EventArgs e)
        {
            EventHandler handler = Disposed;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        private void OnTick(EventArgs e)
        {
            EventHandler handler = Tick;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnStarted(EventArgs e)
        {
            EventHandler handler = Started;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        private void OnStopped(EventArgs e)
        {
            EventHandler handler = Stopped;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        private void OnContinued(EventArgs e)
        {
            EventHandler handler = Continued;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Event Handler Methods

        // Handles Tick events generated by the multimedia timer.
        private void HandleTick(object sender, EventArgs e)
        {
            ticker.MoveNext();

            for(int i = 0; i < ticker.Current; i++)
            {
                OnTick(EventArgs.Empty);
            }            
        }        

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tempo in microseconds per beat.
        /// </summary>
        public int Tempo
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException("MidiInternalClock");
                }

                #endregion

                return ticker.Tempo;
            }
            set
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException("MidiInternalClock");
                }

                #endregion

                ticker.Tempo = value;
            }
        }

        public int Ppqn
        {
            get
            {
                return ticker.Ppqn;
            }
            set
            {
                ticker.Ppqn = value;
            }
        }

        #endregion

        #endregion

        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            #region Guard

            if(disposed)
            {
                return;
            }

            #endregion            

            if(running)
            {
                // Stop the multimedia timer.
                timer.Stop();
            }            

            disposed = true;             

            timer.Dispose();
            ticker.Dispose();

            GC.SuppressFinalize(this);

            OnDisposed(EventArgs.Empty);
        }

        #endregion

        #region IClock Members

        public event EventHandler Tick;

        public event EventHandler Started;

        public event EventHandler Continued;

        public event EventHandler Stopped;

        public bool IsRunning
        {
            get 
            {
                return running;
            }
        }

        #endregion
    }
}
