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
    public sealed class LineEmitter : Emitter
    {
        private float _length;
        private float _angle;

        public Vector2 Normal;
        public float Length
        {
            get { return _length; }
            set { _length = MathHelper.Max(value, 0f); }
        }
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public LineEmitter(uint budget, float length) : base(budget)
        {
            Length = length;
            _angle = 0f;
            Normal = Vector2.UnitY;
        }
        protected override void GetParticleParams(ref Vector2 position, ref float angle)
        {
            position.X = ((float)Rnd.NextDouble() * _length) - (_length / 2f);

            Matrix trans = Matrix.CreateRotationZ(_angle);

            Vector2 actualDir;

            Vector2.Transform(ref Normal, ref trans, out actualDir);
            Vector2.Transform(ref position, ref trans, out position);

            angle = (float)Math.Atan2(actualDir.Y, actualDir.X);
        }
    }
}