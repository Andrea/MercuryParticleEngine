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
using Microsoft.Xna.Framework;
using MercuryParticleEngine.Emitters;

namespace MercuryParticleEngine.Modifiers
{
    public sealed class OpacityMultiModifier : Modifier
    {
        private PreCurve _curve;

        public OpacityMultiModifier(CurveKeyCollection keys, byte samples)
        {
            _curve = new PreCurve(samples);

            foreach (CurveKey key in keys)
            {
                _curve.Keys.Add(key);
            }

            _curve.PreCalculate();
        }
        public override void UpdateParticle(GameTime time, Particle spawn)
        {
            spawn.Opacity = _curve.Evaluate(spawn.Age);
        }
    }
}