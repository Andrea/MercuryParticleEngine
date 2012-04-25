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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace MercuryParticleEngine
{
    public class ParticleSystem : DrawableGameComponent
    {
        //==============================================================[ Private Fields ]
        private Dictionary<string, EmitterGroup> _groups;
        private List<Modifier> _modifiers;
        private SpriteBatch _batch;
        private ContentManager _content;
        private Dictionary<string, ImageStrip> _strips;
        private Stack<Vector2> _trans;
        private bool _paused;

        //============================================================[ Public Interface ]
        public Dictionary<string, EmitterGroup> EmitterGroups
        {
            get { return _groups; }
        }
        public Dictionary<string, ImageStrip> ImageStrips
        {
            get { return _strips; }
        }
        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }
        public SpriteBlendMode BlendMode
        {
            get { return _groups["Default"].BlendMode; }
            set { _groups["Default"].BlendMode = value; }
        }

        //======================================================[ Constructors & Methods ]
        public ParticleSystem(Game game) : base(game)
        {
            _groups = new Dictionary<string, EmitterGroup>();
            _modifiers = new List<Modifier>();
            _strips = new Dictionary<string, ImageStrip>();
            _trans = new Stack<Vector2>();

            ImageStrip strip = new ImageStrip("Default");
            strip.Asset = @"Resources\MPEDefaultParticle";
            strip.FrameWidth = strip.FrameHeight = 32;
            strip.Frames = 2;
            _strips.Add("Default", strip);

            EmitterGroup group = new EmitterGroup("Default");
            _groups.Add("Default", group);

            game.Components.Add(this);
        }
        public void AddEmitterGroup(EmitterGroup group)
        {
            _groups.Add(group.Name, group);

            foreach (Modifier mod in _modifiers)
            {
                group.ApplyModifier(mod);
            }
        }
        public void AddEmitterGroup(string name)
        {
            AddEmitterGroup(new EmitterGroup(name));
        }
        public void ApplyModifier(Modifier mod)
        {
            _modifiers.Add(mod);

            foreach(EmitterGroup group in _groups.Values)
            {
                group.ApplyModifier(mod);
            }
        }
        public bool RemoveModifier(Modifier mod)
        {
            foreach (EmitterGroup group in _groups.Values)
            {
                group.RemoveModifier(mod);
            }

            return _modifiers.Remove(mod);
        }
        public void AddEmitter(Emitter emitter)
        {
            _groups["Default"].AddEmitter(emitter);
        }
        public bool RemoveEmitter(Emitter emitter, bool mods)
        {
            return _groups["Default"].RemoveEmitter(emitter, mods);
        }
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                _batch = new SpriteBatch(GraphicsDevice);
                _content = new ContentManager(Game.Services);

                foreach (ImageStrip strip in _strips.Values)
                {
                    strip.Load(_content);
                }
            }

            base.LoadGraphicsContent(loadAllContent);
        }
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
                _batch.Dispose();
                _content.Dispose();

                foreach (ImageStrip strip in _strips.Values)
                {
                    strip.UnLoad();
                }
            }

            base.UnloadGraphicsContent(unloadAllContent);
        }
        public override void Update(GameTime time)
        {
            if (!_paused)
            {
                foreach (EmitterGroup group in _groups.Values)
                {
                    group.Update(time, _trans);
                }
            }

            base.Update(time);
        }
        public override void Draw(GameTime time)
        {
            foreach (EmitterGroup group in _groups.Values)
            {
                group.Draw(_batch, _strips);
            }

            base.Draw(time);
        }
    }
}
