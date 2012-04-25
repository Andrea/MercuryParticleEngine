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
    public abstract class Modifier
    {
        //======================================================[ Constructors & Methods ]
        public virtual void UpdateEmitterGroup(GameTime time, EmitterGroup group) { }
        public virtual void UpdateEmitter(GameTime time, Emitter emitter) { }
        public virtual void UpdateParticle(GameTime time, Particle spawn) { }
        public virtual void ParticleExpired(GameTime time, Particle spawn) { }
        public virtual void ParticleSpawned(GameTime time, Particle spawn) { }

        protected class PreCurve : Curve
        {
            private byte _samples;
            private float[] _data;

            public PreCurve(byte samples) : base()
            {
                _samples = samples;
                _data = new float[samples + 1];

                Flush();
            }
            public new float Evaluate(float position)
            {
                position = MathHelper.Clamp(position, 0f, 1f);
                byte sample = (byte)(position * _samples);

                if (_data[sample] != -1)
                { 
                    return _data[sample];
                }
                else
                {
                    float value = base.Evaluate(position);
                    _data[sample] = value;
                    return value;
                }
            }
            public new CurveKeyCollection Keys
            {
                get
                {
                    Flush();
                    return base.Keys;
                }
            }
            public void PreCalculate()
            {
                float position;

                for (byte sample = 0; sample < _samples; sample++)
                {
                    position = (float)sample / (float)_samples;
                    _data[sample] = base.Evaluate(position);
                }
            }
            private void Flush()
            {
                for (ushort i = 0; i < _samples; i++)
                {
                    _data[i] = -1;
                }
            }
        }
    }
}
