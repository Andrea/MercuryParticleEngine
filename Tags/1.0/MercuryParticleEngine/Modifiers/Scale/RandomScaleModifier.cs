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
    public sealed class RandomScaleModifier : Modifier
    {
        private Vector2 _range;
        private Random _rnd;

        public RandomScaleModifier(float min, float max)
        {
            min = MathHelper.Max(0f, min);
            max = MathHelper.Max(min, max);

            _range = new Vector2(min, max);
            _rnd = new Random();
        }
        public override void ParticleSpawned(GameTime time, Particle spawn)
        {
            spawn.Scale = MathHelper.Lerp(_range.X, _range.Y, (float)_rnd.NextDouble());
        }
    }
}