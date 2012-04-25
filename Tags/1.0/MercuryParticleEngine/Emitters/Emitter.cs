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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MercuryParticleEngine.Modifiers;

namespace MercuryParticleEngine.Emitters
{
    public class Emitter
    {
        //==============================================================[ Static Members ]
        private static Random _rnd = new Random();
        public static Random Rnd
        {
            get { return _rnd; }
        }

        //==============================================================[ Private Fields ]
        private List<Modifier> _modifiers;
        private uint _budget;
        protected Queue<Particle> _spawns;
        protected Queue<Particle> _limbo;
        private string _strip;
        private byte _frame;
        private bool _primed;
        private uint _quantity;
        private uint _lifespan;
        private Color _color;
        private float _opacity;
        private float _scale;
        private Vector2 _speed;
        private bool _active;
        private Rectangle _rect;
        private Vector2 _origin;

        //============================================================[ Public Interface ]
        public Vector2 Position;
        public string ImageStrip
        {
            get { return _strip; }
            set { _strip = value; }
        }
        public byte Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }
        public uint DischargeQuantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        public uint ParticleLifespan
        {
            get { return _lifespan; }
            set { _lifespan = value; }
        }
        public Color ParticleColor
        {
            get { return _color; }
            set { _color = value; }
        }
        public float ParticleOpacity
        {
            get { return _opacity; }
            set { _opacity = MathHelper.Clamp(value, 0f, 1f); }
        }
        public float ParticleScale
        {
            get { return _scale; }
            set { _scale = MathHelper.Max(value, 0f); }
        }
        public float ParticleSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float ParticleSpeedRange
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }
        public bool Active
        {
            get { return _active; }
        }
        public int ActiveParticles
        {
            get { return _spawns.Count; }
        }
        public int Budget
        {
            get { return (int)_budget; }
        }

        //======================================================[ Constructors & Methods ]
        public Emitter(uint budget)
        {
            _modifiers = new List<Modifier>();
            _budget = budget;
            _spawns = new Queue<Particle>((int)budget);
            _limbo = new Queue<Particle>((int)budget);
            _strip = "Default";
            _frame = 1;
            _quantity = 1;
            _lifespan = 1000;
            _color = Color.White;
            _opacity = 1f;
            _scale = 1f;
            _speed = Vector2.Zero;
            _rect = new Rectangle();
            _origin = new Vector2();
            Position = Vector2.Zero;

            for (int i = 0; i < budget; i++)
            {
                Particle spawn = new Particle();
                _limbo.Enqueue(spawn);
            }
        }
        public void ApplyModifier(Modifier mod)
        {
            _modifiers.Add(mod);
        }
        public bool RemoveModifier(Modifier mod)
        {
            return _modifiers.Remove(mod);
        }
        public void Trigger()
        {
            _primed = true;
        }
        public void Trigger(object sender, EventArgs e)
        {
            Trigger();
        }
        public override string ToString()
        {
            return _spawns.Count.ToString() + @"\" + _budget.ToString();
        }
        internal void Update(GameTime time, Stack<Vector2> trans)
        {
            foreach (Modifier mod in _modifiers)
            {
                mod.UpdateEmitter(time, this);
            }

            trans.Push(Position);

            if (_primed)
            {
                Discharge(time, trans);
                _primed = false;
            }

            trans.Pop();

            foreach (Particle spawn in _spawns)
            {
                spawn.Update(time);

                if (!spawn.Expired)
                {
                    foreach (Modifier mod in _modifiers)
                    {
                        mod.UpdateParticle(time, spawn);
                    }
                }
            }

            for (int i = 0; i < _spawns.Count; i++)
            {
                Particle spawn = _spawns.Peek();

                if (spawn.Expired)
                {
                    foreach (Modifier mod in _modifiers)
                    {
                        mod.ParticleExpired(time, spawn);
                    }

                    _limbo.Enqueue(_spawns.Dequeue());

                    if (_spawns.Count == 0 & _active)
                    {
                        _active = false;
                        RaiseSleeping();
                    }
                }
                else
                {
                    break;
                }
            }
        }
        internal void Draw(SpriteBatch batch, ImageStrip strip)
        {
            strip.Query(_frame, ref _rect, ref _origin);

            foreach (Particle spawn in _spawns)
            {
                spawn.Draw(batch, strip.Texture, ref _rect, ref _origin);
            }
        }
        private void Discharge(GameTime time, Stack<Vector2> trans)
        {
            bool spawned = false;

            for (int i = 0; i < (int)_quantity; i++)
            {
                if (_limbo.Count > 0)
                {
                    if (!_active)
                    {
                        _active = true;
                        RaiseWaking();
                    }

                    Particle spawn = _limbo.Dequeue();

                    Vector2 position = new Vector2();
                    float rotation = 0f;
                    GetParticleParams(ref position, ref rotation);

                    spawn.Color = new Vector4(_color.ToVector3(), _opacity);
                    spawn.Scale = _scale;
                    spawn.Rotation = rotation;

                    spawn.Position = position;
                    foreach (Vector2 vec in trans)
                    {
                        spawn.Position += vec;
                    }

                    spawn.Orientation = rotation;
                    spawn.Speed = MathHelper.Lerp(_speed.X - (_speed.Y / 2f), _speed.X + (_speed.Y / 2f), (float)_rnd.NextDouble());

                    spawn.Activate(time, _lifespan);

                    foreach (Modifier mod in _modifiers)
                    {
                        mod.ParticleSpawned(time, spawn);
                    }

                    _spawns.Enqueue(spawn);

                    spawned = true;
                }
                else
                {
                    RaiseStarving();
                    break;
                }

                if (spawned) { RaiseDischarging(); }
            }
        }
        protected virtual void GetParticleParams(ref Vector2 position, ref float angle)
        {
            position.X = position.Y = 0f;
            angle = (float)_rnd.NextDouble() * MathHelper.TwoPi;
        }

        //======================================================================[ Events ]
        public event EventHandler Waking;
        private void RaiseWaking()
        {
            if (Waking != null) { Waking(this, EventArgs.Empty); }
        }
        public event EventHandler Sleeping;
        private void RaiseSleeping()
        {
            if (Sleeping != null) { Sleeping(this, EventArgs.Empty); }
        }
        public event EventHandler Starving;
        private void RaiseStarving()
        {
            if (Starving != null)
            {
                Starving(this, EventArgs.Empty);
            }
        }
        public event EventHandler Discharging;
        private void RaiseDischarging()
        {
            if (Discharging != null)
            {
                Discharging(this, EventArgs.Empty);
            }
        }
    }
}
