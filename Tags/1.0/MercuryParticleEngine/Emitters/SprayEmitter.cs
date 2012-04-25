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

namespace MercuryParticleEngine.Emitters
{
    public sealed class SprayEmitter : Emitter
    {
        private float _dir;
        private float _spread;

        public float Direction
        {
            get { return _dir; }
            set { _dir = value; }
        }
        public float Spread
        {
            get { return _spread; }
            set { _spread = MathHelper.Clamp(value, 0f, MathHelper.TwoPi); }
        }

        public SprayEmitter(uint budget, float dir, float spread) : base(budget)
        {
            Direction = dir;
            Spread = spread;
        }
        protected override void GetParticleParams(ref Vector2 position, ref float angle)
        {
            position.X = position.Y = 0f;
            angle = _dir + ((float)Rnd.NextDouble() * _spread) - (_spread / 2f);
        }
    }
}