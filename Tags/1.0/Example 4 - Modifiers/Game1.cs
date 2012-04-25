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

namespace Example4
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        ParticleSystem system;
        Emitter emitter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            system = new ParticleSystem(this);
        }

        protected override void Initialize()
        {
            base.Initialize();

            emitter = new Emitter(1000);
            emitter.ParticleLifespan = 2000;
            emitter.ParticleSpeed = 150f;
            emitter.ParticleSpeedRange = 50f;

            //We have seen some examples of Modifiers in the previous examples. Modifiers
            //are always necessary to bring Emitters to life, but there is no limit to the
            //amount, and variety of Modifiers you can apply.
            emitter.ApplyModifier(new EmitterMouseController(false));
            emitter.ApplyModifier(new TimedTriggerController(10f));

            //There are many Modifiers to choose from to get the effect you desire, and it
            //is easy to create you own (there will be another example of this). Here i will
            //demonstrate some of the most useful Modifiers.

            //The GravityModifier applys a linear gravity force to all active Particles. You
            //can adjust the direction, and the strength of gravity.
            emitter.ApplyModifier(new GravityModifier(MathHelper.PiOver2, 350f));

            //The Color3Modifier fades the color of Particles between 3 colors. You can
            //adjust the sweep rate of the middle color. The interpolation is pre-calculated,
            //and you can change the smoothness of the pre-calculation (higher is better).
            emitter.ApplyModifier(new Color3Modifier(Color.White.ToVector3(),
                Color.SteelBlue.ToVector3(), 0.25f, Color.Purple.ToVector3(), 128));

            //Also available is a Color2Modifier, which fades between 2 colors - this is not
            //pre-calculated and so you can change the colors in realtime.
            //Plus, there is a RandomColorModifier, which gives each spawned Particle a
            //random color.

            //As well as color, there are similar Modifiers for adjusting Particles opacity
            //and scale. They work in the same way. There is the addition of MultiOpacity
            //and MultiScale Modifiers, which interpolate between and infinate number of
            //values...
            emitter.ApplyModifier(new Scale2Modifier(0f, 3f));
            emitter.ApplyModifier(new Opacity3Modifier(1f, 1f, 0.75f, 0f, 64));

            //The AtmosphereModifier allows you to simulate dense atmosphere of water. It
            //works by adjusting the speed of the Particles as they move... A density of 1
            //will have no effect on the Particles. A density >1 has the effect of slowing
            //Particles.
            emitter.ApplyModifier(new AtmosphereModifier(1.2f));

            //Finally, the WindowBounceModifier causes Particles to bounce off the edge of
            //game window. You can adjust the bounce coefficient.
            emitter.ApplyModifier(new WindowBounceModifier(this.Window, .75f));

            //As always, we need to add the Emitter to the ParticleSystem.
            system.AddEmitter(emitter);
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