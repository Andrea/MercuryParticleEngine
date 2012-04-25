//Mercury Particle Engine 2.0 Copyright (C) 2007 Matthew Davey

//This library is free software; you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.

//This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
//without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License along with this
//library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330,
//Boston, MA 02111-1307 USA 

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MercuryParticleEngine;
using MercuryParticleEngine.Controllers;
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace WindowsGame1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        ContentManager _content;

        ParticleSystem _system;
        LineEmitter _snow;
        SprayEmitter _fireworks;
        Emitter _trail;
        Emitter _explosion;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _content = new ContentManager(Services);

            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            _graphics.PreferMultiSampling = false;
            //_graphics.PreferredBackBufferFormat = SurfaceFormat.Bgr565;
            _graphics.ApplyChanges();
            //_graphics.ToggleFullScreen();

            _system = new ParticleSystem(this);

            _snow = new LineEmitter(_system, 1000, 800f, 0f);
            _snow.Position = new Vector2(400f, 0f);
            _snow.ParticleLifespan = 15000;
            _snow.ParticleSpeed = 50f;
            _snow.ParticleSpeedRange = 50f;
            _snow.ParticleColor = Color.Azure;
            _snow.ParticleScale = .25f;
            _snow.ApplyModifier(new SineForceModifier(10f, 20f));
            _snow.ApplyModifier(new RandomScaleModifier(.05f, .2f));
            _snow.ApplyController(new TimedTriggerController(10));
            _snow.ApplyModifier(new OpacityModifier(1f, 0f));

            _fireworks = new SprayEmitter(_system, 500, MathHelper.Pi, MathHelper.PiOver4);
            _fireworks.Position = new Vector2(400f, 600f);
            _fireworks.ParticleSpeed = 500f;
            _fireworks.ParticleSpeedRange = 100f;
            _fireworks.ParticleColor = Color.BurlyWood;
            _fireworks.ParticleLifespan = 2000;
            _fireworks.ParticleScale = .5f;
            _fireworks.ParticleOpacity = .25f;
            _fireworks.ApplyModifier(new GravityModifier(0f, 250f));
            _fireworks.ApplyController(new TimedTriggerController(40));

            _trail = new Emitter(_system, 5000);
            _trail.ParticleLifespan = 250;
            _trail.ParticleSpeed = 25f;
            _trail.ApplyModifier(new ColorModifier(Color.Goldenrod, Color.Honeydew));
            _trail.ApplyModifier(new ScaleModifier(.2f, 0f));
            _trail.ApplyController(new TrailController(_fireworks, 10));

            _explosion = new Emitter(_system, 5000);
            _explosion.DischargeQuantity = 250;
            _explosion.ParticleScale = .5f;
            _explosion.ParticleSpeed = 155f;
            _explosion.ParticleSpeedRange = 300f;
            _explosion.ApplyModifier(new ColorModifier(Color.PowderBlue, Color.Red));
            _explosion.ApplyModifier(new OpacityModifier(1f, 0f));
            _explosion.ApplyModifier(new AtmosphereModifier(0.2f));
            _explosion.ApplyController(new ExplosionController(_fireworks));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
            }
        }

        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent == true)
            {
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the default game to exit on Xbox 360 and Windows
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Window.Title = _system.Status;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}