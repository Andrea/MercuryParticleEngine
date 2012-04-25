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

//Usually, you will need to include all 3 of these using statements.
using MercuryParticleEngine;
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace Example1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        //Here we declare our ParticleSystem, and 1 Emitter.
        ParticleSystem system;
        Emitter emitter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            //When creating a ParticleSystem, we pass a Game object. The ParticleSystem is 
            //a DrawableGameComponent, so it will update and render automatically.
            system = new ParticleSystem(this);
        }

        protected override void Initialize()
        {
            base.Initialize();

            //When creating an Emitter, you will always need to specify a budget. The budget
            //is the number of Particles the Emittter has, and if the Emitter tries to
            //emit more than this number, it will raise a 'starving' event. (Emitter events
            //will be demonstrated in another example)
            emitter = new Emitter(1000);

            //Setting the DischargeQuantity controls how many Particles the Emitter spawns
            //each time it is triggered.
            emitter.DischargeQuantity = 3;

            //The speed of Particles is controlled with two values. The first value is the
            //base speed of all Particles, and the second value is the range either side of
            //the base speed. Each particle is assigned a random speed value between
            //Speed - (Range /2), and Speed + (Range /2).
            emitter.ParticleSpeed = 50f;
            emitter.ParticleSpeedRange = 50f;
            //So, each Particle will have a speed between 25, and 75.

            //Applying Modififiers to an Emitter is the way to advanced Emitter effects, and
            //can also help integrate the engine into your game. In this example we are
            //applying 3 simple Modifiers. The first is a MouseController, which moves the
            //position of the Emitter to match the Mouse coordinates. The MouseController
            //modifier can also trigger the Emitter when the mouse button is pressed. The
            //second is a RandomColorModifier, which causes each spawned Particle to be a
            //random color. The third is a RandomScaleModifier, which causes each Particle
            //to be a random scale between the minimum and maximum values specified.
            Modifier mouseControl = new EmitterMouseController(true);
            Modifier randomColor = new RandomColorModifier();
            Modifier randomScale = new RandomScaleModifier(.1f, 2f);

            //We must apply each of these Modifiers to the Emitter.
            emitter.ApplyModifier(mouseControl);
            emitter.ApplyModifier(randomColor);
            emitter.ApplyModifier(randomScale);

            //And finally, we must add the Emitter to the ParticleSystem.
            system.AddEmitter(emitter);

            //This demonstration is now ready to run!
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