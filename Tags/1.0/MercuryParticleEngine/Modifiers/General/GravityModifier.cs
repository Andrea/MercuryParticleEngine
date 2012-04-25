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
    public sealed class GravityModifier : Modifier
    {
        private Vector2 _grav;

        public GravityModifier(float direction, float strength)
        {
            _grav = new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
            _grav.Normalize();
            _grav *= strength;
        }
        public override void UpdateParticle(GameTime time, Particle spawn)
        {
            Vector2 momentum = new Vector2();
            momentum.X = (float)Math.Cos(spawn.Orientation);
            momentum.Y = (float)Math.Sin(spawn.Orientation);
            Vector2.Multiply(ref momentum, spawn.Speed, out momentum);

            Vector2 actualGrav;
            Vector2.Multiply(ref _grav, (float)time.ElapsedGameTime.TotalSeconds, out actualGrav);

            Vector2.Add(ref momentum, ref actualGrav, out momentum);

            spawn.Orientation = (float)Math.Atan2((double)momentum.Y, (double)momentum.X);
            spawn.Speed = momentum.Length();
        }
    }
}