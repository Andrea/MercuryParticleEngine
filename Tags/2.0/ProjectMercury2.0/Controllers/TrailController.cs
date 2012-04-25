//Mercury Particle Engine 2.0 Copyright (C) 2007 Matthew Davey

//This library is free software; you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.

//This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
//without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License along with this
//library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330,
//Boston, MA 02111-1307 USA 

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MercuryParticleEngine.Emitters;

namespace MercuryParticleEngine.Controllers
{
    public sealed class TrailController : Controller
    {
        #region [ Private Fields ]

        private Timer _timer;
        private Emitter _emitter;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="emitter">Emitter to trail</param>
        /// <param name="frequency">Frequency to trigger</param>
        public TrailController(Emitter emitter, int frequency)
        {
            AutoResetEvent autoReset = new AutoResetEvent(false);
            TimerCallback timerDelegate = new TimerCallback(Tick);

            _emitter = emitter;
            _timer = new Timer(timerDelegate, autoReset, 0, frequency);
        }

        private void Tick(object stateInfo)
        {
            LinkedListNode<Particle> node = _emitter.FirstActiveParticle;
            while (node != null)
            {
                Subscriptions.ForEach(delegate(Emitter emitter)
                {
                    emitter.Trigger(node.Value.Position);
                });

                node = node.Next;
            }
        }

        #endregion
    }
}
