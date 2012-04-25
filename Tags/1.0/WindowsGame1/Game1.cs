
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion
using MercuryParticleEngine;
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace WindowsGame1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        ParticleSystem system;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            system = new ParticleSystem(this);

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferMultiSampling = false;
            this.IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ToggleFullScreen();
        }

        protected override void Initialize()
        {
            Emitter example = new CircleEmitter(150, 0f, false, CircleEmitter.RadialSpread.FromCenter);
            example.DischargeQuantity = 3;
            example.ParticleLifespan = 6000;
            example.ParticleSpeed = 40f;
            example.ParticleSpeedRange = 40f;
            example.Position = new Vector2(320f, 240f);

            example.ApplyModifier(new TimedTriggerController(200f));
            example.ApplyModifier(new Opacity3Modifier(0f, .5f, .25f, 0f, 128));
            example.ApplyModifier(new Color3Modifier(Color.Firebrick.ToVector3(), Color.Goldenrod.ToVector3(), 0.75f, Color.WhiteSmoke.ToVector3(), 128));
            example.ApplyModifier(new Scale2Modifier(1f, 3f));
            example.ApplyModifier(new ParticleRotationModifier(.05f));

            ImageStrip xtra = new ImageStrip("xtra");
            xtra.Asset = @"Resources\xtra1";
            xtra.FrameHeight = 128;
            xtra.Frames = 4;
            xtra.FrameWidth = 128;

            system.ImageStrips.Add("xtra", xtra);

            example.ImageStrip = "xtra";
            example.Frame = 3;
            
            system.AddEmitter(example);

            base.Initialize();
        }


        /// <summary>
        /// Load your graphics content.  If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                // TODO: Load any ResourceManagementMode.Automatic content
            }

            // TODO: Load any ResourceManagementMode.Manual content
        }


        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent == true)
            {
                content.Unload();
            }
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the default game to exit on Xbox 360 and Windows
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { this.Exit(); }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}