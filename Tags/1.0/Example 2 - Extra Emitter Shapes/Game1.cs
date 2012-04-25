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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using MercuryParticleEngine;
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace Example2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        ParticleSystem system;
        SprayEmitter spray;
        CircleEmitter circle;
        SpiralEmitter spiral;
        LineEmitter line;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            system = new ParticleSystem(this);
        }

        protected override void Initialize()
        {
            base.Initialize();

            //The SprayEmitter emits Particles in a fountain-like manner. You can adjust the
            //angle of the spray, and also the spread. The SprayEmitter constructor takes
            //both of its angle arguments in radians.
            spray = new SprayEmitter(1000, MathHelper.ToRadians(-90f), MathHelper.ToRadians(30f));
            spray.Position = new Vector2(400f, 550f);
            spray.ParticleSpeed = 250f;
            spray.ParticleSpeedRange = 100f;
            spray.DischargeQuantity = 3;
            spray.ParticleColor = Color.Red;

            //The CircleEmitter emits Particles from a random position inside a circle. It
            //can also emit Particles only from the circles edge, like a ring shape. You can
            //set the spawned Particles to face away from the circles center, or in a
            //random direction.
            circle = new CircleEmitter(1000, 50f, false, CircleEmitter.RadialSpread.FromCenter);
            circle.Position = new Vector2(150f, 350f);
            circle.ParticleSpeed = 100f;
            circle.DischargeQuantity = 3;
            circle.ParticleColor = Color.Blue;

            //The SpiralEmitter can emit Particles in a spiral shape. You can adjust the
            //number of segments in the spiral, and also the direction of the spiral.
            //In this example, we have set the DischargeQuantity to be the same as spiral
            //segments, so at each trigger we will get 1 complete ring.
            spiral = new SpiralEmitter(1000, 150f, 32, SpiralEmitter.SpiralDirection.Clockwise);
            spiral.Position = new Vector2(650f, 350f);
            spiral.ParticleSpeed = -100f;
            spiral.DischargeQuantity = 32;
            spiral.ParticleColor = Color.Green;

            //The LineEmitter spawns Particles from a random position along a line. You can
            //change the length, and rotation of the line. It could be useful for creating
            //rain or snow effects.
            line = new LineEmitter(1000, 800f);
            line.Position = new Vector2(400f, 0f);
            line.ParticleSpeed = 150f;
            line.DischargeQuantity = 3;

            //The TimeTriggerController Modifier causes the Emitter to trigger at the
            //specified interval.
            //Unfortunately there is a bug in this Modifier which causes it to only
            //affect 1 Emitter at a time. It will be fixed in next version.
            spray.ApplyModifier(new TimedTriggerController(50f));
            circle.ApplyModifier(new TimedTriggerController(50f));
            spiral.ApplyModifier(new TimedTriggerController(250f));
            line.ApplyModifier(new TimedTriggerController(50f));

            //And of course, we need to add the Emitters to the ParticleSystem.
            system.AddEmitter(spray);
            system.AddEmitter(circle);
            system.AddEmitter(spiral);
            system.AddEmitter(line);
        }

        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent == true)
            {
                content.Unload();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the default game to exit on Xbox 360 and Windows
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}