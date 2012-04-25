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
using Microsoft.Xna.Framework.Graphics;
using MercuryParticleEngine.Emitters;

namespace MercuryParticleEngine.Modifiers
{
    public sealed class Color3Modifier : Modifier
    {
        private PreCurve _red;
        private PreCurve _green;
        private PreCurve _blue;

        public Color3Modifier(Vector3 initial, Vector3 mid, float sweep, Vector3 ultimate, byte samples)
        {
            _red = new PreCurve(samples);
            _green = new PreCurve(samples);
            _blue = new PreCurve(samples);

            _red.Keys.Add(new CurveKey(0f, initial.X));
            _red.Keys.Add(new CurveKey(sweep, mid.X));
            _red.Keys.Add(new CurveKey(1f, ultimate.X));

            _green.Keys.Add(new CurveKey(0f, initial.Y));
            _green.Keys.Add(new CurveKey(sweep, mid.Y));
            _green.Keys.Add(new CurveKey(1f, ultimate.Y));

            _blue.Keys.Add(new CurveKey(0f, initial.Z));
            _blue.Keys.Add(new CurveKey(sweep, mid.Z));
            _blue.Keys.Add(new CurveKey(1f, ultimate.Z));

            _red.PreCalculate();
            _green.PreCalculate();
            _blue.PreCalculate();
        }
        public override void UpdateParticle(GameTime time, Particle spawn)
        {
            spawn.Color.X = _red.Evaluate(spawn.Age);
            spawn.Color.Y = _green.Evaluate(spawn.Age);
            spawn.Color.Z = _blue.Evaluate(spawn.Age);
        }
    }
}