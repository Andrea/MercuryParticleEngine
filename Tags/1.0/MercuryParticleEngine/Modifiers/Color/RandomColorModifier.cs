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
    public sealed class RandomColorModifier : Modifier
    {
        private Random _rnd;

        public RandomColorModifier()
        {
            _rnd = new Random();
        }
        public override void ParticleSpawned(GameTime time, Particle spawn)
        {
            spawn.Color.X = (float)_rnd.NextDouble();
            spawn.Color.Y = (float)_rnd.NextDouble();
            spawn.Color.Z = (float)_rnd.NextDouble();
        }
    }
}