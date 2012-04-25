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

namespace Example5
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        ParticleSystem system;
        Emitter emitter;

        //In this example i will show you how to use your own images & textures to render
        //your Particles. This is achieved with an object called ImageStrip.
        ImageStrip strip;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            system = new ParticleSystem(this);
            emitter = new Emitter(1000);
            emitter.ParticleSpeed = 150f;
            emitter.ApplyModifier(new EmitterMouseController(true));

            //When creating an ImageStrip, you must supply 5 pieces of information. You must
            //give the ImageStrip a name, so that Emitters can identify it. You must specify
            //the number of frames in the ImageStrip, and also the width and height of each
            //frame. Finally you must specify the asset name, as specified in the content
            //pipeline.
            strip = new ImageStrip("custom");
            strip.Asset = "CustomParticleImage";
            strip.FrameHeight = 128;
            strip.FrameWidth = 128;
            strip.Frames = 4;

            //When it is done, you will add the ImageStrip to the ParticleSystem.
            system.ImageStrips.Add("custom", strip);

            //Then you will tell the Emitter to use this ImageStrip.
            emitter.ImageStrip = "custom";
            emitter.Frame = 2;

            //When using the ImageStrip, the Emitter will automatically render the
            //specified frame at the correct size, and the origin will always be at the
            //centre of the frame.

            system.AddEmitter(emitter);

            //If you make an error, either by telling the Emitter to use an ImageStrip that
            //doesn't exist, or telling it to use a frame that doesn't exist - The Emitter
            //will instead render Particles with an exclamation mark (!), to let you know
            //that there has been an error.
        }

        protected override void Initialize()
        {
            base.Initialize();
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