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
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace MercuryParticleEngine
{
    public class EmitterGroup
    {
        //==============================================================[ Private Fields ]
        private string _name;
        private List<Emitter> _emitters;
        private List<Modifier> _modifiers;
        private SpriteBlendMode _blend;

        //============================================================[ Public Interface ]
        public string Name
        {
            get { return _name; }
        }
        public SpriteBlendMode BlendMode
        {
            get { return _blend; }
            set { _blend = value; }
        }
        public Vector2 Position;
        public List<Emitter> Emitters
        {
            get { return _emitters; }
        }

        //======================================================[ Constructors & Methods ]
        public EmitterGroup(string name)
        {
            _name = name;
            _emitters = new List<Emitter>();
            _modifiers = new List<Modifier>();
            _blend = SpriteBlendMode.Additive;
            Position = Vector2.Zero;
        }
        public void AddEmitter(Emitter emitter)
        {
            _emitters.Add(emitter);

            foreach (Modifier mod in _modifiers)
            {
                emitter.ApplyModifier(mod);
            }
        }
        public bool RemoveEmitter(Emitter emitter, bool mods)
        {
            if (mods)
            {
                foreach (Modifier mod in _modifiers)
                {
                    emitter.RemoveModifier(mod);
                }
            }

            return _emitters.Remove(emitter);
        }
        public void ApplyModifier(Modifier mod)
        {
            _modifiers.Add(mod);

            foreach (Emitter emitter in _emitters)
            {
                emitter.ApplyModifier(mod);
            }
        }
        public bool RemoveModifier(Modifier mod)
        {
            foreach (Emitter emitter in _emitters)
            {
                emitter.RemoveModifier(mod);
            }

            return _modifiers.Remove(mod);
        }
        public void Trigger()
        {
            foreach (Emitter emitter in _emitters)
            {
                emitter.Trigger();
            }
        }
        public override string ToString()
        {
            int num = 0;
            int budget = 0;
            foreach (Emitter emitter in _emitters)
            {
                num += emitter.ActiveParticles;
                budget += emitter.Budget;
            }

            return num.ToString() + @"/" + budget.ToString() + " in " + _emitters.Count.ToString() + " emitters";
        }
        internal void Update(GameTime time, Stack<Vector2> trans)
        {
            foreach (Modifier mod in _modifiers)
            {
                mod.UpdateEmitterGroup(time, this);
            }

            trans.Push(Position);

            foreach (Emitter emitter in _emitters)
            {
                emitter.Update(time, trans);
            }

            trans.Pop();
        }
        internal void Draw(SpriteBatch batch, Dictionary<string, ImageStrip> strips)
        {
            batch.Begin(_blend);

            foreach (Emitter emitter in _emitters)
            {
                ImageStrip strip;

                try
                {
                    strip = strips[emitter.ImageStrip];
                }
                catch
                {
                    strip = strips["Default"];
                    emitter.Frame = 2;
                }

                emitter.Draw(batch, strip);
            }

            batch.End();
        }
    }
}
