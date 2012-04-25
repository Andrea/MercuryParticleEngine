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
    public sealed class CircleEmitter : Emitter
    {
        private float _radius;
        private bool _ring;
        private RadialSpread _spread;

        public float Radius
        {
            get { return _radius; }
            set { _radius = MathHelper.Max(value, 0f); }
        }
        public bool Ring
        {
            get { return _ring; }
            set { _ring = value; }
        }
        public RadialSpread Spread
        {
            get { return _spread; }
            set { _spread = value; }
        }

        public CircleEmitter(uint budget, float radius, bool ring, RadialSpread spread) : base(budget)
        {
            Radius = radius;
            Ring = ring;
            Spread = spread;
        }
        protected override void GetParticleParams(ref Vector2 position, ref float angle)
        {
            float a = (float)Rnd.NextDouble() * MathHelper.TwoPi;
            float dist;

            if (_ring) { dist = _radius; }
            else { dist = (float)Rnd.NextDouble() * _radius; }

            position.X = (float)Math.Cos(a) * dist;
            position.Y = (float)Math.Sin(a) * dist;

            angle = a;
        }

        public enum RadialSpread
        {
            FromCenter,
            Random
        }
    }
}