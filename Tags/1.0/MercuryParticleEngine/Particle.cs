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
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MercuryParticleEngine
{
    public class Particle
    {
        //==============================================================[ Private Fields ]
        private int _lifespan;
        private int _creation;
        private bool _expired;
        private float _age;
        private float _scale;
        private float _rotation;
        private float _speed;
        private float _orientation;
        private Vector2 _momentum;
        private Dictionary<Type, object> _user;

        //============================================================[ Public Interface ]
        public Vector2 Position;
        public Vector2 Momentum
        {
            get { return _momentum; }
        }
        public Vector4 Color;
        internal bool Expired
        {
            get { return _expired; }
            set { _expired = value; }
        }
        public float Age
        {
            get { return _age; }
        }
        public float Opacity
        {
            get { return Color.W; }
            set { Color.W = value; }
        }
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public float Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;

                _momentum.X = (float)Math.Cos(value);
                _momentum.Y = (float)Math.Sin(value);

                _momentum.Normalize();
            }
        }
        public Dictionary<Type, object> UserData
        {
            get { return _user; }
        }

        //======================================================[ Constructors & Methods ]
        public Particle()
        {
            _scale = 1f;
            _momentum = Vector2.Zero;
            _user = new Dictionary<Type, object>(8);
            Position = Vector2.Zero;
            Color = Vector4.One;
        }
        internal void Activate(GameTime time, uint lifespan)
        {
            _lifespan = (int)lifespan;
            _creation = (int)time.TotalGameTime.TotalMilliseconds;
            _expired = false;
            _age = 0f;
        }
        public void Rotate(float angle)
        {
            _rotation += angle;
        }
        internal void Update(GameTime time)
        {
            _age = ((float)time.TotalGameTime.TotalMilliseconds - (float)_creation) / (float)_lifespan;

            if (_age > 1f)
            {
                _expired = true;
                return;
            }

            Vector2 actualMomentum;
            Vector2.Multiply(ref _momentum, _speed, out actualMomentum);
            Vector2.Multiply(ref actualMomentum, (float)time.ElapsedGameTime.TotalSeconds, out actualMomentum);
            Vector2.Add(ref Position, ref actualMomentum, out Position);
        }
        internal void Draw(SpriteBatch batch, Texture2D texture, ref Rectangle rect, ref Vector2 origin)
        {
            batch.Draw(texture, Position, rect, new Color(Color), _rotation, origin, _scale, SpriteEffects.None, 0f);
        }
    }
}
