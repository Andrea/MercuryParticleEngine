//Copyright (C) 2006 Matt Davey

//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or any later version.

//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

using System;
using System.Timers;
using Microsoft.Xna.Framework;
using MercuryParticleEngine.Emitters;

namespace MercuryParticleEngine.Modifiers
{
    public sealed class TimedTriggerController : Modifier
    {
        private bool _primed;
        private Timer _timer;

        public float Interval
        {
            get { return (float)_timer.Interval; }
            set { _timer.Interval = (double)value; }
        }

        public TimedTriggerController(float frequency)
        {
            _timer = new Timer((double)frequency);
            _timer.AutoReset = true;
            _timer.Elapsed += new ElapsedEventHandler(Trigger);
            _timer.Start();
        }
        public override void UpdateEmitter(GameTime time, Emitter emitter)
        {
            if (_primed)
            {
                emitter.Trigger();
                _primed = false;
            }
        }
        public void Trigger(object sender, ElapsedEventArgs e)
        {
            _primed = true;
        }
    }
}
