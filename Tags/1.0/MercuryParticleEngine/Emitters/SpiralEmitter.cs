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
    public sealed class SpiralEmitter : Emitter
    {
        private float _radius;
        private float[] _angles;
        private SpiralDirection _dir;
        private int _segments;
        private int _segment;

        public float Radius
        {
            get { return _radius; }
            set { _radius = MathHelper.Max(value, 0f); }
        }

        public SpiralEmitter(uint budget, float radius, byte segments, SpiralDirection dir) : base(budget)
        {
            Radius = radius;
            _dir = dir;
            _segments = (int)segments;

            if (segments < 3) { segments = 3; }

            _angles = new float[segments + 1];

            for (int i = 0; i <= segments; i++)
            {
                _angles[i] = (MathHelper.TwoPi / segments) * i;
            }

            _segment = 0;
        }
        protected override void GetParticleParams(ref Vector2 position, ref float angle)
        {
            if (_dir == SpiralDirection.AntiClockwise)
            {
                _segment++;
                if (_segment > _segments) { _segment = 1; }
            }
            else
            {
                _segment--;
                if (_segment < 0) { _segment = _segments - 1; }
            }

            angle = _angles[_segment];

            position.X = (float)Math.Cos(_angles[_segment]) * _radius;
            position.Y = (float)Math.Sin(_angles[_segment]) * _radius;
        }

        public enum SpiralDirection
        {
            Clockwise,
            AntiClockwise
        }
    }
}